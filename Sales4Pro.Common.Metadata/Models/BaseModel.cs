using Sales4Pro.Common.Metadata.Interfaces;

namespace Sales4Pro.Common.Metadata.Models
{
    public class BaseModel : IBaseModel
    {
        public bool IsDeleted { get; set; }
        public long SyncDateTimeTicks { get; set; }
    }
}
