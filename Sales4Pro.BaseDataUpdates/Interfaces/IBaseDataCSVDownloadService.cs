namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public interface IBaseDataCSVDownloadService
{
    IBaseDataDownloadPlugIn InjectedPlugIn { get; set; }
    void FillInitialProgressItemVMs();
    void ResetUpdate(bool raiseBaseDataDeleted);
    Task<List<ProgressItem>> CheckForUpdateAsync();
    Task<bool> UpdateAsync(List<ProgressItem> syncTables);

}