namespace Sales4Pro.Common.Metadata.Models
{
    public class ProductImage : BaseModel
    {
        public string ProductImageID { get; set; }
        public string ImageName { get; set; }
        //public byte[] ImageBlob { get; set; }
        //public string ProductID { get; set; }
        public System.DateTime SyncDateTime { get; set; }

        //public bool IsModified { get; set; }
        public string DownloadUri { get; set; }
    }
}
