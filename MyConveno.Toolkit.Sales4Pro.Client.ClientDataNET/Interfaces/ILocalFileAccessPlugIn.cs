namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface ILocalFileAccessPlugIn
{
    Task SaveObjectToJSONFileAsync(string jsonfilename, object content);

    Task<T> GetObjectFromJSONFileAsync<T>(string jsonfilename);
}