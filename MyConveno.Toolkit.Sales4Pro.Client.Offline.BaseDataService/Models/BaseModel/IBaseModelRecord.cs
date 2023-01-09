namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService
{
    public interface IBaseModelRecord
    {
        bool IsDeleted { get; init; }
        long SyncDateTimeTicks { get; init; }
    }
}