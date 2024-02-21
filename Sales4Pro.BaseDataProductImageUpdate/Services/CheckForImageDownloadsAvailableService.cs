using Newtonsoft.Json;
using System.Web;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public class CheckForImageDownloadsAvailableService : ICheckForImageDownloadsAvailableService
{
    public event EventHandler<bool> ProductImageUpdatesAvailable;

    private string baseDataImageWebServiceHost = string.Empty;

    public CheckForImageDownloadsAvailableService(string webServiceHost, string remoteUpdateDataFolder)
    {
        baseDataImageWebServiceHost = webServiceHost;
    }

    #region Tasks

    public async Task CheckIfUpdatedImagesAvailableAsync(long lastUpdateProductImageTicks, string currentLoginUserName)
    {
        bool updatesAvailable = false;

        // Lade eine temporäre Liste mit Items vom Typ BaseDataImageUpdateProgressItem,
        // die die Anzahl der geänderten Datensätze enthält
        // Wichtig ist hier nur der Eintrag [TotalChanges]
        List<BaseDataImageUpdateProgressItem> allTablesWithChanges;

        try
        {
            Dictionary<string, long> syncDateTimes = new()
            {
                // (Default UpdateDateTimeTicks 630822816000000000 ist der 01.01.2000)
                { "ProductImage", lastUpdateProductImageTicks }
            };

            using (HttpClient client = new())
            {
                string jsonSyncDateTimes = JsonConvert.SerializeObject(syncDateTimes, Formatting.Indented);


                UriBuilder builder = new(baseDataImageWebServiceHost + "/GetChangedBaseDataTablesAsJSONList")
                {
                    Port = -1
                };

                System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
                query["jsontablelist"] = jsonSyncDateTimes;
                //query["userID"] = currentLoginUserName;
                builder.Query = query.ToString();
                string url = builder.ToString();

                HttpResponseMessage data = await client.GetAsync(url);
                string jsonResponse = await data.Content.ReadAsStringAsync();

                allTablesWithChanges = JsonConvert.DeserializeObject<List<BaseDataImageUpdateProgressItem>>(jsonResponse);
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            allTablesWithChanges = null;
        }

        if (allTablesWithChanges != null && allTablesWithChanges.Any())
        {
            // Hole aus der Liste nur den Eintrag für die ProductImage-Tabelle
            BaseDataImageUpdateProgressItem baseDataImageUpdateProgressItem = allTablesWithChanges.FirstOrDefault(s => s.TableName == "ProductImage");

            // .. und prüfe den Eintrag TotalChanges
            if (baseDataImageUpdateProgressItem != null && baseDataImageUpdateProgressItem.TotalChanges > 0)
                updatesAvailable = true;
            else
                updatesAvailable = false;
        }

        ProductImageUpdatesAvailable?.Invoke(this, updatesAvailable);

        return;
    }

    #endregion

}