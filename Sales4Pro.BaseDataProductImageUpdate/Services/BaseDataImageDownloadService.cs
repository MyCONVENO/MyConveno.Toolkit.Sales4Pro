using Azure.Storage.Blobs;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
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
    // Lösche alle lokale Bilder und setze den Wert der letzten Aktualisierung zurück
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
    public async Task<bool> DownloadImagesAsync(string currentLoginUserName, string imagefolderPath, BlobContainerClient container)
    {
        bool success;

        UpdatedImagePaths.Clear();

        try
        {
            cancellationTokenSourceDownloadProductImagesTask = new CancellationTokenSource();

            if (imageUpdateIsRunning)
                success = false;
            else
                success = await Task.Run(() => UpdateProductImagesAsync(currentLoginUserName, imagefolderPath, container, cancellationTokenSourceDownloadProductImagesTask.Token));
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
    public async Task<bool> UpdateProductImagesAsync(string currentLoginUserName, string imagefolderPath, BlobContainerClient container, CancellationToken ct)
    {
        int changedItemsCount = -1;
        int result = 0;

        HttpClient httpClient = new() { Timeout = TimeSpan.FromMinutes(25) };

        UriBuilder createanduploadBuilder = new(baseDataWebServiceHost + "/CreateAndUploadZippedCSVPackage")
        {
            Port = -1
        };
        System.Collections.Specialized.NameValueCollection createanduploadQuery = HttpUtility.ParseQueryString(createanduploadBuilder.Query);

        createanduploadQuery["tableName"] = "ProductImage";
        createanduploadQuery["syncdatetimeticks"] = InjectedPlugIn.GetProductImageUpdateDateTimeTicks().ToString();
        createanduploadBuilder.Query = createanduploadQuery.ToString();
        string createanduploadURL = createanduploadBuilder.ToString();

        HttpResponseMessage data = await httpClient.GetAsync(createanduploadURL);
        string filename = await data.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(filename))
            return false;

        DateTime lastRecordUpdateDateTime = new(2000, 1, 1);

        // Ein Container für Results
        List<KeyValuePair<int, DateTime>> results = new();

        using (BaseDataAzureBlobStorageServices azureService = new(containerName))
        {
            List<ProductImage> productImages = azureService.DownloadFileAndExtractRecords<ProductImage>(filename);
            results.Add(ProcessProductImageListsAsync(productImages, imagefolderPath, container, ct));
        }

        KeyValuePair<int, DateTime> tempPair = new(results.Sum(r => r.Key), results.Max(r => r.Value));
        result += tempPair.Key;

        if (lastRecordUpdateDateTime < tempPair.Value)
            lastRecordUpdateDateTime = tempPair.Value;

        //********************************************************************
        // Schreibe den Tabellenname (ProductImage) und die zugehörige
        // Updatedatum (in Ticks) in die AppSettings (für die nächste Downloadanfrage)
        //********************************************************************
        InjectedPlugIn.SetProductImageUpdateDateTimeTicks(lastRecordUpdateDateTime.Ticks);

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

        HttpResponseMessage deleteData = await httpClient.GetAsync(deleteURL);
        string deleteJSONResponse = await deleteData.Content.ReadAsStringAsync();

        changedItemsCount = result;

        //*********************************************************************************

        imageUpdateIsRunning = false;

        httpClient.Dispose();
        httpClient = null;

        return changedItemsCount > 0;
    }

    private KeyValuePair<int, DateTime> ProcessProductImageListsAsync(List<ProductImage> productImages, string imagefolderPath, BlobContainerClient container, CancellationToken ct)
    {
        DateTime lastRecordUpdateDateTime = new(2000, 1, 1);
        bool downloadOK = true;

        if (productImages == null)
            return new KeyValuePair<int, DateTime>(0, lastRecordUpdateDateTime);

        foreach (ProductImage productImage in productImages)
        {
            BaseDataImageUpdateProgressItem imageSyncItem = new(typeof(ProductImage))
            { ImagePath = new List<string>() };

            Task<string> localImagePath = InjectedPlugIn.WriteOneProductImage(productImage, imagefolderPath, container);
            imageSyncItem.ImagePath.Add(localImagePath.Result);
            UpdateProgressChanged?.Invoke(this, new List<BaseDataImageUpdateProgressItem>() { imageSyncItem });

            if (ct.IsCancellationRequested)
            {
                downloadOK = false;
                break;
            }
        }

        if (downloadOK)
        {
            if (productImages.First().SyncDateTimeTicks > 0)
                lastRecordUpdateDateTime = new DateTime(productImages.Max(i => i.SyncDateTimeTicks));
            else
                lastRecordUpdateDateTime = productImages.Max(i => i.SyncDateTime);
        }

        return new KeyValuePair<int, DateTime>(productImages.Count, lastRecordUpdateDateTime);
    }

    #endregion

}