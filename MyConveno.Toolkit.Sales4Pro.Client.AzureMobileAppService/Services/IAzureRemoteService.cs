namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService
{
    public interface IAzureRemoteService
    {
        string AzureURL { get; init; }

        Task InitializeAsync();
        Task<List<SyncShoppingCart>> GetOrdersAsync();
        Task<SyncShoppingCart> GetOrderAsync(string id);
        Task<int> GetOrdersCountAsync();
        Task<string> SaveOrderAsync(SyncShoppingCart upsertItem);
        Task<bool> DeleteOrderAsync(string id);
    }
}