using System.Threading;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public interface IBaseDataImageDownloadService
{
    IBaseDataImageDownloadPlugIn InjectedPlugIn { get; }

    void CancelUpdateImages();
    Task<bool> DownloadImagesAsync(string currentLoginUserName);
    Task ResetUpdateAsync();
    Task<bool> UpdateProductImagesAsync(string currentLoginUserName, CancellationToken ct);
}