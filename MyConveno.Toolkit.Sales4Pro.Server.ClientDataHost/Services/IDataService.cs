namespace MyConveno.Toolkit.Sales4Pro.Server.ClientDataHost
{
    public interface IDataService
    {
        int AddUser(User user, string connectionId = "Default");
        int DeleteUser(string userid, string connectionId = "Default");
        IEnumerable<User> GetAllUsers(string connectionId = "Default");
        Client GetClientById(string clientid = "", string connectionId = "Default");
        User GetUserByCredentials(string username = "", string password = "", string connectionId = "Default");
        User GetUserByUserName(string username = "", string connectionId = "Default");
        int UpdateUser(User user, string connectionId = "Default");
    }
}