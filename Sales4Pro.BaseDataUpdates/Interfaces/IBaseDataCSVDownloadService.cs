using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public interface IBaseDataCSVDownloadService
{
    IBaseDataDownloadPlugIn InjectedPlugIn { get; set; }
    
    void FillInitialProgressItemVMs();
    Task ResetUpdateAsync(bool raiseBaseDataDeleted);

    Task<List<ProgressItem>> CheckForUpdateAsync();

    Task<bool> UpdateAsync(List<ProgressItem> syncTables);

}