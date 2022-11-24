using Sales4Pro.Common.Metadata.Interfaces;
using SQLite;

namespace Sales4Pro.Common.Metadata.Models
{
    public class BaseModel : IBaseModel
    {
        [Ignore]
        public bool IsDeleted { get; set; }
        [Ignore]
        public long SyncDateTimeTicks { get; set; }
    }
}
