namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IClientAccessService
{
    Task<List<Client>> GetAllClientsAsync();
    Task<Client> GetClientByIdAsync(string clientid);
    Task<bool> UpdateClient(Client client);
}