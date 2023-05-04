namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IAgentAccessService
{
    Task<bool> AddAgent(Agent agent);
    Task<bool> DeleteAgent(string agentid);
    Task<bool> DeleteAllAgents();
    Task<List<Agent>> GetAllAgentsAsync();
    void Initialize();
    Task<bool> UpdateAgent(Agent agent);
}