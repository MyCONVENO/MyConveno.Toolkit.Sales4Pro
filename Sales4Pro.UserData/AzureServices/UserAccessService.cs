using System.Net.Http.Json;

//https://www.stevejgordon.co.uk/sending-and-receiving-json-using-httpclient-with-system-net-http-json

namespace MyConveno.Toolkit.Sales4Pro.Client.UserData;

public class UserAccessService : IUserAccessService
{
    private readonly string azureURL = string.Empty;
    private HttpClient httpClient { get; set; }

    public UserAccessService(string url)
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

    public async Task<List<User>> GetAllUsersAsync()
    {
        Initialize();
        string parameter = string.Format("Users/GetAll");
        IList<User> users = await httpClient.GetFromJsonAsync<IList<User>>(parameter);
        return (List<User>)users;
    }

    public async Task<User> GetUserByCredentialsAsync(string username, string password)
    {
        Initialize();
        string parameter = string.Format("Users/GetByCredentials?username={0}&password={1}", username, password);

        try
        {
            User user = await httpClient.GetFromJsonAsync<User>(parameter);
            return user;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<User> GetUserByUserNameAsync(string username)
    {
        Initialize();
        string parameter = string.Format("Users/GetByUserName?username={0}", username);
        User user = await httpClient.GetFromJsonAsync<User>(parameter);
        return user;
    }


    public async Task<bool> AddUser(User user)
    {
        Initialize();

        string parameter = "Users";
        HttpResponseMessage response = await httpClient.PostAsJsonAsync(parameter, user);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> UpdateUser(User user)
    {
        Initialize();

        string parameter = "Users";
        HttpResponseMessage response = await httpClient.PutAsJsonAsync(parameter, user);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

    public async Task<bool> DeleteUser(string userid)
    {
        Initialize();

        string parameter = string.Format("Users/{0}", userid);
        HttpResponseMessage response = await httpClient.DeleteAsync(parameter);

        if (response.IsSuccessStatusCode)
            return true;
        else
            return false;
    }

}
