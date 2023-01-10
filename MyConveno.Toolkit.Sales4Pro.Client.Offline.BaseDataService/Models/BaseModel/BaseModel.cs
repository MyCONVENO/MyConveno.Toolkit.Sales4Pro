namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public class BaseModel : IBaseModel
{
    public bool IsDeleted { get; set; }
    public long SyncDateTimeTicks { get; set; }
}

