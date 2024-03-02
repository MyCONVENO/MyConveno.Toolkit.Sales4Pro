namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService
{
    public interface IAzureRemoteService
    {
        string AzureURL { get; init; }

        Task InitializeAsync();
        Task<List<SyncShoppingCart>> GetOrdersAsync(string userName, DateTime fromDate);
        Task<List<SyncShoppingCart>> GetPendingOrdersAsync();
        Task<int> GetOrdersCountAsync(string userName, DateTime fromDate);
        Task<SyncShoppingCart> GetOrderAsync(string id);
        Task<string> SaveOrderAsync(SyncShoppingCart upsertItem);
        Task<bool> DeleteOrderAsync(string id);

    }
}