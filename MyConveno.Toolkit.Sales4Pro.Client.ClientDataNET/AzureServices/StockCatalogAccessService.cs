using System.Net.Http.Json;

//https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class StockCatalogAccessService : IStockCatalogAccessService
{
    private readonly string azureURL = string.Empty;

    private HttpClient httpClient { get; set; }

    public StockCatalogAccessService(string url)
    {
        azureURL = url;
    }

    public void Initialize()
    {
        if (httpClient != null)
            return;

        httpClient = new HttpClient()
        {
            BaseAddress = new Uri(azureURL)
        };
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<List<StockCatalog>> GetAllStockCatalogsAsync()
    {
        Initialize();
        string parameter = string.Format("StockCatalogs/GetAll");
        IList<StockCatalog> stockCatalogs = await httpClient.GetFromJsonAsync<IList<StockCatalog>>(parameter);
        return (List<StockCatalog>)stockCatalogs;
    }

    public async Task<bool> AddStockCatalog(StockCatalog StockCatalog)
    {
        Initialize();

        string parameter = "StockCatalogs";
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(parameter, StockCatalog);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> UpdateStockCatalog(StockCatalog StockCatalog)
    {
        Initialize();

        string parameter = "StockCatalogs";
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(parameter, StockCatalog);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> DeleteStockCatalog(string StockCatalogid)
    {
        Initialize();

        string parameter = string.Format("StockCatalogs/{0}", StockCatalogid);
        HttpResponseMessage response = await httpClient.DeleteAsync(parameter);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

}
