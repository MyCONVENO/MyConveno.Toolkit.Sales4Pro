using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public partial class BaseDataImageDownloadService : ObservableObject, IBaseDataImageDownloadService
{
    public event EventHandler<List<BaseDataImageUpdateProgressItem>> UpdateProgressChanged;

    static bool imageUpdateIsRunning;
    private CancellationTokenSource cancellationTokenSourceDownloadProductImagesTask;
    private readonly string baseDataWebServiceHost = string.Empty;
    private readonly string containerName = string.Empty;

    public BaseDataImageDownloadService(string webServiceHost, string container, IBaseDataImageDownloadPlugIn plugIn)
    {
        baseDataWebServiceHost = webServiceHost;
        containerName = container;
        InjectedPlugIn = plugIn;
    }

    #region Public Properties

    public IBaseDataImageDownloadPlugIn InjectedPlugIn { get; set; }

    [ObservableProperty]
    private ObservableCollection<string> updatedImagePaths = new();

    #endregion

    #region Methods, Functions and Tasks

    // ***********************************************************************************************
    // Lösche alle lokale Bilder und setze den Wert der letztren aktualisierung zurück
    // ***********************************************************************************************
    public async Task ResetUpdateAsync()
    {
        await InjectedPlugIn.ResetUpdateAsync();
    }
    // ***********************************************************************************************

    public void CancelUpdateImages()
    {
        cancellationTokenSourceDownloadProductImagesTask?.Cancel();
    }

    // ***********************************************************************************************
    // 1. Lade Bilder ins lokale Dateisystem
    // ***********************************************************************************************
    public async Task<bool> DownloadImagesAsync(string currentLoginUserName)
    {
        bool success;

        UpdatedImagePaths.Clear();

        try
        {
            cancellationTokenSourceDownloadProductImagesTask = new CancellationTokenSource();

            if (imageUpdateIsRunning)
                success = false;
            else
                success = await Task.Run(() => UpdateProductImagesAsync(currentLoginUserName, cancellationTokenSourceDownloadProductImagesTask.Token));
        }
        catch (Exception)
        {
            BaseDataImageUpdateProgressItem updateresult = new();
            updateresult.TotalChanges = updateresult.TotalChanges > 0 ? updateresult.TotalChanges : 1;
            UpdateProgressChanged?.Invoke(this, new List<BaseDataImageUpdateProgressItem>() { updateresult });
            success = false;
        }
        return success;
    }

    // ***********************************************************************************************
    // 2. Der eigentliche Updatevorgang
    // ***********************************************************************************************
    public async Task<bool> UpdateProductImagesAsync(string currentLoginUserName, CancellationToken ct)
    {
        int changedItemsCount = -1;
        int result = 0;

        using (HttpClient client = new() { Timeout = TimeSpan.FromMinutes(25) })
        {
            UriBuilder createanduploadBuilder = new(baseDataWebServiceHost + "/CreateAndUploadZippedCSVPackage")
            {
                Port = -1
            };
            System.Collections.Specialized.NameValueCollection createanduploadQuery = HttpUtility.ParseQueryString(createanduploadBuilder.Query);

            createanduploadQuery["tableName"] = "ProductImage";
            createanduploadQuery["syncdatetimeticks"] = InjectedPlugIn.GetProductImageUpdateDateTimeTicks().ToString();
            createanduploadBuilder.Query = createanduploadQuery.ToString();
            string createanduploadURL = createanduploadBuilder.ToString();

            HttpResponseMessage data = await client.GetAsync(createanduploadURL);
            string filename = await data.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(filename))
                return false;


            DateTime updateDateTime = new (2000, 1, 1);

            // Ein Container für Results
            List<KeyValuePair<int, DateTime>> results = new ();

            using (AzureBlobStorageServices azureService = new(containerName))
            {
                List<ProductImage> productImages = azureService.DownloadFileAndExtractRecords<ProductImage>(filename);
                results.Add(ProcessProductImageListsAsync(productImages, ct));
            }

            KeyValuePair<int, DateTime> tempPair = new (results.Sum(r => r.Key), results.Max(r => r.Value));
            result += tempPair.Key;

            if (updateDateTime < tempPair.Value)
                updateDateTime = tempPair.Value;

            //********************************************************************
            // Schreibe den Tabellenname (ProductImage) und die zugehörige
            // Updatedatum (in Ticks) in die AppSettings (für die nächste Downloadanfrage)
            //********************************************************************
            InjectedPlugIn.SetProductImageUpdateDateTimeTicks(updateDateTime.Ticks);

            //*********************************************************************************
            // Houskeeping :-)
            // Lösche verarbeitete, bereitgestellte und somit nicht mehr benötigte Datenpakete
            // am Server
            //*********************************************************************************

            UriBuilder deleteBuilder = new(baseDataWebServiceHost + "/DeleteZIPFileInBlob")
            {
                Port = -1
            };
            System.Collections.Specialized.NameValueCollection deleteQuery = HttpUtility.ParseQueryString(deleteBuilder.Query);
            deleteQuery["filename"] = filename;
            deleteBuilder.Query = deleteQuery.ToString();
            string deleteURL = deleteBuilder.ToString();

            HttpResponseMessage deleteData = await client.GetAsync(deleteURL);
            string deleteJSONResponse = await deleteData.Content.ReadAsStringAsync();

            changedItemsCount = result;
        }
        //*********************************************************************************


        imageUpdateIsRunning = false;

        return changedItemsCount > 0;
    }

    private KeyValuePair<int, DateTime> ProcessProductImageListsAsync(List<ProductImage> productImages, CancellationToken ct)
    {
        DateTime syncDateTime = new(2000, 1, 1);
        bool downloadOK = true;

        if (productImages == null)
            return new KeyValuePair<int, DateTime>(0, syncDateTime);

        List<ProductImage> alldownloads = new();
        alldownloads.AddRange(productImages.OrderBy(s => s.ImageName));

        using (HttpClient client = new())
        {
            while (alldownloads.Any())
            {
                if (ct != null)
                    ct.ThrowIfCancellationRequested();

                BaseDataImageUpdateProgressItem imageSyncItem = new(typeof(ProductImage))
                {
                    ImagePath = new List<string>()
                };
                imageSyncItem.ImagePath.Clear();

                int downloadCount = 10;
                if (alldownloads.Count >= downloadCount)
                {
                    IEnumerable<ProductImage> downloadImgs = alldownloads.Take(downloadCount).ToList();

                    Task<string>[] downloads = new Task<string>[downloadCount];
                    int dCounter = 0;
                    foreach (ProductImage productImage in downloadImgs)
                    {
                        downloads[dCounter] = DownloadOneProductImageAsync(productImage);
                        alldownloads.Remove(productImage);
                        dCounter++;
                    }

                    Task.WaitAll(downloads);

                    foreach (Task<string> t in downloads)
                    {
                        imageSyncItem.ImagePath.Add(t.Result);
                    }

                    UpdateProgressChanged?.Invoke(this, new List<BaseDataImageUpdateProgressItem>() { imageSyncItem });
                }
                else
                {
                    IEnumerable<ProductImage> downloadImgs = alldownloads.ToList();

                    Task[] downloads = new Task[downloadImgs.Count()];
                    int dCounter = 0;
                    foreach (ProductImage pi in downloadImgs)
                    {
                        downloads[dCounter] = DownloadOneProductImageAsync(pi);
                        alldownloads.Remove(pi);
                        dCounter++;
                    }
                    Task.WaitAll(downloads);
                }
            }
        }

        if (downloadOK)
        {
            if (productImages.First().SyncDateTimeTicks > 0)
                syncDateTime = new DateTime(productImages.Max(i => i.SyncDateTimeTicks));
            else
                syncDateTime = productImages.Max(i => i.SyncDateTime);
        }

        return new KeyValuePair<int, DateTime>(productImages.Count, syncDateTime);
    }

    private async Task<string> DownloadOneProductImageAsync(ProductImage productImage)
    {
        using (HttpClient client = new())
        {
            using (MemoryStream destination = new())
            {
                string webURL = productImage.DownloadUri + "?timestamp='" + DateTime.Now.Ticks.ToString() + "'";
                Stream imageStream = await client.GetStreamAsync(webURL);
                await imageStream.CopyToAsync(destination);
                string imgFilePath = await InjectedPlugIn.WriteOneProductImage(productImage.ImageName, destination);
                return imgFilePath;
            }
        }
    }

    #endregion

}