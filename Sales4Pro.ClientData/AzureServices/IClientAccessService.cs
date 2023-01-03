namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IClientAccessService
{
    Task<Client> GetClientByIdAsync(string clientid);
}