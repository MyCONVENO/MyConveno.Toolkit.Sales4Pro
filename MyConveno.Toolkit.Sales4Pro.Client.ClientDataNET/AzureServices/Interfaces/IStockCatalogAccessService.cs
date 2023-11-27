namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IStockCatalogAccessService
{
    Task<bool> AddStockCatalog(StockCatalog association);
    Task<bool> DeleteStockCatalog(string associationid);
    Task<List<StockCatalog>> GetAllStockCatalogsAsync();
    void Initialize();
    Task<bool> UpdateStockCatalog(StockCatalog association);
}