using System.Net.Http.Json;

//https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class SpecialDiscountAccessService : ISpecialDiscountAccessService
{
    private readonly string azureURL = string.Empty;
    private HttpClient httpClient { get; set; }

    public SpecialDiscountAccessService(string url)
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

    public async Task<List<SpecialDiscount>> GetAllSpecialDiscountsAsync()
    {
        Initialize();
        string parameter = string.Format("SpecialDiscounts/GetAll");
        IList<SpecialDiscount> specialDiscounts = await httpClient.GetFromJsonAsync<IList<SpecialDiscount>>(parameter);
        return (List<SpecialDiscount>)specialDiscounts;
    }

    public async Task<bool> AddSpecialDiscount(SpecialDiscount specialDiscount)
    {
        Initialize();

        string parameter = "SpecialDiscounts";
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(parameter, specialDiscount);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> UpdateSpecialDiscount(SpecialDiscount specialDiscount)
    {
        Initialize();

        string parameter = "SpecialDiscounts";
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(parameter, specialDiscount);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> DeleteSpecialDiscount(string specialDiscountid)
    {
        Initialize();

        string parameter = string.Format("SpecialDiscounts/{0}", specialDiscountid);
        HttpResponseMessage response = await httpClient.DeleteAsync(parameter);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

}
