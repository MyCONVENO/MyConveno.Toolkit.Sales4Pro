namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public interface IBaseDataImageDownloadPlugIn
{
    void SetProductImageUpdateDateTimeTicks(long ticks);
    long GetProductImageUpdateDateTimeTicks();
    Task ResetUpdateAsync();
    Task<string> WriteOneProductImage(string fileName, MemoryStream stream);
}