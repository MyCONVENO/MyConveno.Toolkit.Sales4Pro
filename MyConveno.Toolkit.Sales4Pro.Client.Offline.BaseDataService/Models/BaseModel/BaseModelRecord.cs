namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService;

public record BaseModelRecord : IBaseModelRecord
{
    public bool IsDeleted { get; init; }
    public long SyncDateTimeTicks { get; init; }
}

