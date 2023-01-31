using System.Net.Http.Json;

//https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class ClientAccessService : IClientAccessService
{
    string azureURL = string.Empty;
    private HttpClient httpClient { get; set; }

    public ClientAccessService(string url)
    {
        azureURL = url;
    }

    protected void Initialize()
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

    public async Task<Client> GetClientByIdAsync(string clientid)
    {
        Initialize();
        string parameter = string.Format("Clients/GetById?clientid={0}", clientid);
        Client client = await httpClient.GetFromJsonAsync<Client>(parameter);
        return client;
    }

    public async Task<bool> UpdateClient(Client agent)
    {
        Initialize();

        string parameter = "Clients";
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(parameter, agent);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

}
