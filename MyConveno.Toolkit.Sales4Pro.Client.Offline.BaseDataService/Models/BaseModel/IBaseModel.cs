namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService
{
    public interface IBaseModel
    {
        bool IsDeleted { get; set; }
        long SyncDateTimeTicks { get; set; }
    }
}