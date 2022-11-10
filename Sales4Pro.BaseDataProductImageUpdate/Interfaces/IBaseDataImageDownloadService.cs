using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public interface IBaseDataImageDownloadService
{
    event EventHandler<List<BaseDataImageUpdateProgressItem>> UpdateProgressChanged;
    IBaseDataImageDownloadPlugIn InjectedPlugIn { get; }
    ObservableCollection<string> UpdatedImagePaths { get; set; }
    void CancelUpdateImages();
    Task<bool> DownloadImagesAsync(string currentLoginUserName);
    Task ResetUpdateAsync();
    Task<bool> UpdateProductImagesAsync(string currentLoginUserName, CancellationToken ct);
}