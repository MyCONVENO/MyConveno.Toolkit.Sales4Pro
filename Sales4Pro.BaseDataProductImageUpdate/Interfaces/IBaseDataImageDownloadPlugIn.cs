using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public interface IBaseDataImageDownloadPlugIn
{
    ObservableCollection<string> UpdatedImagePaths { get; set; }

    long GetImageUpdateDateTimeTicks();
    void RaiseUpdateProgressChanged(List<BaseDataImageUpdateProgressItem> Result);
    Task ResetUpdateAsync();
    void SetProductImageUpdateDateTimeTicks(long ticks);
    Task<string> WriteOneProductImage(string fileName, MemoryStream stream);
}