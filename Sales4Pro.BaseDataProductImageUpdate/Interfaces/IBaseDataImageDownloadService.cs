using Azure.Storage.Blobs;
using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public interface IBaseDataImageDownloadService
{
    event EventHandler<bool> ProductImageUpdatesAvailable;

    event EventHandler<List<BaseDataImageUpdateProgressItem>> UpdateProgressChanged;
    IBaseDataImageDownloadPlugIn InjectedPlugIn { get; }
    ObservableCollection<string> UpdatedImagePaths { get; set; }
    Task CheckIfUpdatedImagesAvailableAsync(long lastUpdateProductImageTicks, string currentLoginUserName);
    void CancelUpdateImages();
    Task<bool> DownloadImagesAsync(string currentLoginUserName, string imagefolderPath, BlobContainerClient container);
    Task ResetUpdateAsync();
    Task<bool> UpdateProductImagesAsync(string currentLoginUserName, string imagefolderPath, BlobContainerClient container, CancellationToken ct);
}