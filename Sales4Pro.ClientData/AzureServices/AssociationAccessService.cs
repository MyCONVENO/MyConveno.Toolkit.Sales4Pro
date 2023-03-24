using System.Net.Http.Json;

//https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class AssociationAccessService : IAssociationAccessService
{
    private readonly string azureURL = string.Empty;
    private HttpClient httpClient { get; set; }

    public AssociationAccessService(string url)
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

    public async Task<List<Association>> GetAllAssociationsAsync()
    {
        Initialize();
        string parameter = string.Format("Associations/GetAll");
        IList<Association> associations = await httpClient.GetFromJsonAsync<IList<Association>>(parameter);
        return (List<Association>)associations;
    }

    public async Task<bool> AddAssociation(Association association)
    {
        Initialize();

        string parameter = "Associations";
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(parameter, association);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> UpdateAssociation(Association association)
    {
        Initialize();

        string parameter = "Associations";
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(parameter, association);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> DeleteAssociation(string associationid)
    {
        Initialize();

        string parameter = string.Format("Associations/{0}", associationid);
        HttpResponseMessage response = await httpClient.DeleteAsync(parameter);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

}
