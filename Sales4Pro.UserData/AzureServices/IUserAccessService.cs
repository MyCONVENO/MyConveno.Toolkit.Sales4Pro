namespace MyConveno.Toolkit.Sales4Pro.Client.UserData;

public interface IUserAccessService
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByUserNameAsync(string username);
    Task<User> GetUserByCredentialsAsync(string username, string password);
    Task<bool> AddUser(User user);
    Task<bool> UpdateUser(User user);
    Task<bool> DeleteUser(string userid);
    void Initialize();
}