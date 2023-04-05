using System.Net.Http.Json;

//https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class AgentAccessService : IAgentAccessService
{
    private readonly string azureURL = string.Empty;

    private HttpClient httpClient { get; set; }

    public AgentAccessService(string url)
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

    public async Task<bool> AddAgent(Agent agent)
    {
        Initialize();

        string parameter = "Agents";
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(parameter, agent);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> UpdateAgent(Agent agent)
    {
        Initialize();

        string parameter = "Agents";
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(parameter, agent);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> DeleteAgent(string agentid)
    {
        Initialize();

        string parameter = string.Format("Agents/{0}", agentid);
        HttpResponseMessage response = await httpClient.DeleteAsync(parameter);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<List<Agent>> GetAllAgentsAsync()
    {
        Initialize();
        string parameter = string.Format("Agents/GetAll");
        IList<Agent> agents = await httpClient.GetFromJsonAsync<IList<Agent>>(parameter);
        return (List<Agent>)agents;
    }

}
