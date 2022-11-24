namespace Sales4Pro.Common.Metadata.Interfaces
{
    public interface IBaseModel
    {
        bool IsDeleted { get; set; }
        long SyncDateTimeTicks { get; set; }
    }
}
