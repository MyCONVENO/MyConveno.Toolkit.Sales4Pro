using System.Net.Http.Json;

//https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class PaymentTermAccessService : IPaymentTermAccessService
{
    private readonly string azureURL = string.Empty;
    private HttpClient httpClient { get; set; }

    public PaymentTermAccessService(string url)
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

    public async Task<List<PaymentTerm>> GetAllPaymentTermsAsync()
    {
        Initialize();
        string parameter = string.Format("PaymentTerms/GetAll");
        IList<PaymentTerm> paymentTerms = await httpClient.GetFromJsonAsync<IList<PaymentTerm>>(parameter);
        return (List<PaymentTerm>)paymentTerms;
    }

    public async Task<bool> AddPaymentTerm(PaymentTerm paymentTerm)
    {
        Initialize();

        string parameter = "PaymentTerms";
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(parameter, paymentTerm);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> UpdatePaymentTerm(PaymentTerm paymentTerm)
    {
        Initialize();

        string parameter = "PaymentTerms";
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(parameter, paymentTerm);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> DeletePaymentTerm(string paymentTermid)
    {
        Initialize();

        string parameter = string.Format("PaymentTerms/{0}", paymentTermid);
        HttpResponseMessage response = await httpClient.DeleteAsync(parameter);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> DeleteAllPaymentTerms()
    {
        Initialize();

        string parameter = string.Format("PaymentTerms/DeleteAll");
        HttpResponseMessage response = await httpClient.DeleteAsync(parameter);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

}