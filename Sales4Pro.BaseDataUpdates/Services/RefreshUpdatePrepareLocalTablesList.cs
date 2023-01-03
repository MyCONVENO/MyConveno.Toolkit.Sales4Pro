using Newtonsoft.Json;
using System.Web;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

internal static class RefreshUpdatePrepareLocalTablesList
{
    //**********************************************************************************
    // Lade eine temporäre Liste mit Items vom Typ [ProgressItem], die die Anzahl
    // der geänderten Datensätze enthält
    // Wichtig ist hier nur der Eintrag [TotalChanges] in den einzelnen Items (Tabellen)
    //**********************************************************************************

    internal static async Task<List<ProgressItem>> UpdateListOnline(IBaseDataDownloadPlugIn plugIn, string baseDataWebServiceHost)
    {
        // *************************************************************************
        // Hole die Änderungen aller Tabellen, die dem aktuellen Stand entsprechen
        // aus den AppSettings
        // *************************************************************************
        Dictionary<string, long> syncDateTimes = plugIn.GetAllUpdateDateTimeTicks();

        // *************************************************************************
        // Wir übergeben dem http-Client diese Liste (als JSON-File) und teilen
        // ihm somit mit, was der aktuelle Stand am Client ist
        // Wir bekommen dann eine Liste mit den AKTUELLEN online Werten, für welche
        // Server-Tabellen es Änderungen gibt.
        //
        // Der Response der REST-Anfrage
        // Achtung!!! dies dauert ggf. sehr lange!!!
        // *************************************************************************
        List<ProgressItem> serjson;

        using (HttpClient client = new HttpClient())
        {
            string jsonSyncDateTimes = JsonConvert.SerializeObject(syncDateTimes, Formatting.Indented);
            UriBuilder builder = new UriBuilder(baseDataWebServiceHost + "/GetChangedBaseDataTablesAsJSONList")
            {
                Port = -1
            };
            System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
            query["jsontablelist"] = jsonSyncDateTimes;
            builder.Query = query.ToString();
            string url = builder.ToString();

            HttpResponseMessage data = await client.GetAsync(url);
            string jsonResponse = await data.Content.ReadAsStringAsync();

            serjson = JsonConvert.DeserializeObject<List<ProgressItem>>(jsonResponse);
            return serjson;
        }
    }
}