using static MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates.ProgressItemViewModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public class ProgressItem
{
    public delegate void UpdateProgressChangedEventHandler(IEnumerable<ProgressItem> Result);

    public ProgressItem()
    { }

    public ProgressItem(Type t, string localizedTableName, DateTime lastUpdate)
    {
        ProgressItemType = t;
        TableName = t.Name;
        LocalizedTableName = localizedTableName;
        LastUpdate = lastUpdate;
    }

    public Type ProgressItemType { get; set; }
    public string TableName { get; set; }
    public int TotalChanges { get; set; }
    public int Changed { get; set; }
    public string ProgressText { get; set; }
    public List<string> ImagePath { get; set; }
    public DateTime LastUpdate { get; set; }
    public CurrentBaseDataUpdateStatesEnum CurrentBaseDataUpdateState { get; set; }
    public string LocalizedTableName { get; set; }

}
