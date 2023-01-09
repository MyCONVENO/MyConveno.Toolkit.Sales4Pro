namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public record BaseModel : IBaseModel
{
    public bool IsDeleted { get; set; }
    public long SyncDateTimeTicks { get; set; }
}

