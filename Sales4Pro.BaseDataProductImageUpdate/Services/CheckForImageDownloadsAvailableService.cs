using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

    public async Task CheckIfUpdatedImagesAvailableAsync(long updateDateTimeProductImage, string currentLoginUserName)
    {
        bool updatesAvailable = await CheckIfUpdatesAvailableAsync(updateDateTimeProductImage, currentLoginUserName);

        ProductImageUpdatesAvailable?.Invoke(this, updatesAvailable);

        return;
    }

    private async Task<bool> CheckIfUpdatesAvailableAsync(long updateDateTimeProductImage, string currentLoginUserName)
    {
        // Lade eine temporäre Liste mit Items vom Typ BaseDataImageUpdateProgressItem, die die Anzahl der geänderten Datensätze enthält
        // Wichtig ist hier nur der Eintrag [TotalChanges]
        List<BaseDataImageUpdateProgressItem> allTablesWithChanges;

        try
        {
            Dictionary<string, long> syncDateTimes = new Dictionary<string, long>
            {
                // (Default UpdateDateTimeTicks 630822816000000000 ist der 01.01.2000)
                { "ProductImage", updateDateTimeProductImage }
            };

            //List<BaseDataImageUpdateProgressItem> serjson;
            using (HttpClient client = new HttpClient())
            {
                string jsonSyncDateTimes = JsonConvert.SerializeObject(syncDateTimes, Formatting.Indented);


                UriBuilder builder = new UriBuilder(baseDataImageWebServiceHost + "/GetChangedBaseDataTablesAsJSONList")
                {
                    Port = -1
                };

                //UriBuilder builder = new UriBuilder(baseDataImageWebServiceHost + "/GetAvailableChangesBaseData");
                //UriBuilder builder = new UriBuilder(baseDataImageWebServiceHost + "/GetAvailableChangesBaseData");
                builder.Port = -1;
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
            if (baseDataImageUpdateProgressItem != null &&
                baseDataImageUpdateProgressItem.TotalChanges > 0)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

}