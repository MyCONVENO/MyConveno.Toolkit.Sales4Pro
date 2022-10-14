using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public static class UpdateOneTable
{
    // *********************************************************
    // Diese Funktion aktualisiert den Inhalt einer Tabelle mit
    // evtl. verfügbaren Änderungen (Löschungen, neue und
    // geänderte Datenätze 
    // *********************************************************
    public static async Task<int> UpdateOneTableAsync<T>(Sales4ProDatabaseConnection sales4ProDatabaseConnection,
                                                         ProgressItem progressItem,
                                                         IBaseDataDownloadPlugIn plugIn,
                                                         string baseDataWebServiceHost,
                                                         RefreshProgressItem refreshProgressItem)
    {
        try
        {
            KeyValuePair<int, DateTime> processDbListsResult;
            //int result = 0; // die Anzahl der geänderten Datensätze
            //DateTime updateDateTime = new DateTime(2000, 1, 1);

            // *********************************************************
            // Hole eine Liste von komprimierten Dateien, die Datensätze enthalten, die 
            // seit dem letzten Update in dieser Tabelle (z.B. Agent) verändert wurden  (z.B. 6h4h39wht8433tzh43zth8437z.data)
            // *********************************************************
            progressItem.ProgressText = "Erfrage Aktualisierungen";
            foreach (ProgressItem syncItem in new List<ProgressItem>() { progressItem })
            {
                refreshProgressItem.RefreshSingleUpdateProgressItem(syncItem);
            }
            //await Task.Delay(1000);

            //********************************************************************
            // GetChangedDataFromWebServiceAsync
            // Hole die Tabellen-Datensätze die seit dem letzten Update geändert,
            // hinzugefügt oder gelöscht (IsDeleted) wurden
            //********************************************************************
            string filename = string.Empty;
            string tableName = progressItem.TableName; // z.B. "Agent"

            //********************************************************************
            // Übertrage
            // - den Tabellenname (tablename),
            // - die letzte gespeicherte SyncDateTime der Tabelle (...)
            // - und die Login E-Mail (...)
            // Hole eine Liste von komprimierten CSV-Dateien, die Datensätze enthalten,
            // die seit dem letzten Update (syncDateTime) in dieser Tabelle (z.B. Agent)
            // verändert wurden (z.B. 6h4h39wht8433tzh43zth8437z.zip)
            //********************************************************************
            List<string> serjson = new();

            using (HttpClient client = new() { Timeout = TimeSpan.FromMinutes(25) })
            {
                UriBuilder builder = new(baseDataWebServiceHost + "/CreateAndUploadZippedCSVPackage")
                {
                    Port = -1
                };
                System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);

                //Der Tabellenname
                query["tableName"] = tableName;
                //Der Zeitstempel der letzten Aktualisierung
                query["syncdatetimeticks"] = plugIn.GetTableUpdateDateTimeTicks(tableName).ToString();
                builder.Query = query.ToString();
                string url = builder.ToString();

                HttpResponseMessage data = await client.GetAsync(url);
                filename = await data.Content.ReadAsStringAsync();
            }
            //********************************************************************
            // Zurückgegeben wird ein Dateiname (z.B. h3645h875fh74f43fh34768.zip), 
            // Diese Datei enthält die geänderten Datensätze der Tabelle
            //********************************************************************

            progressItem.ProgressText = "Aktualisierungen vorhanden";
            foreach (ProgressItem syncItem in new List<ProgressItem>() { progressItem })
            {
                refreshProgressItem.RefreshSingleUpdateProgressItem(syncItem);
            }
            //await Task.Delay(1000);

            //********************************************************************
            // Erstelle einen Container für Results
            //********************************************************************
            //List<KeyValuePair<int, DateTime>> results = new List<KeyValuePair<int, DateTime>>();

            //********************************************************************
            // Jetzt laden wir die erzeugte Datei mit den aktuellen Datensätzen
            //********************************************************************
            progressItem.ProgressText = "Lade Aktualisierungen";
            foreach (ProgressItem syncItem in new List<ProgressItem>() { progressItem })
            {
                refreshProgressItem.RefreshSingleUpdateProgressItem(syncItem);
            }
            //await Task.Delay(1000);

            using (AzureBlobStorageServices azureService = new())
            {
                List<T> rawDataList = azureService.DownloadFileAndExtractRecords<T>(filename);

                //********************************************************************
                // Aktualisiere die Datenbanktabelle und gebe ein Key-Value Pair
                // - Anzahl der aktualisierten Datensätze
                // - Datum der Aktualisierung
                // zurück
                //********************************************************************
                progressItem.ProgressText = "Wende Aktualisierungen an";
                foreach (ProgressItem syncItem in new List<ProgressItem>() { progressItem })
                {
                    refreshProgressItem.RefreshSingleUpdateProgressItem(syncItem);
                }
                await Task.Delay(1000);

                processDbListsResult = await ProcessDbLists.ProcessDbListsAsync<T>(rawDataList, sales4ProDatabaseConnection);
            }

            progressItem.Changed = processDbListsResult.Key;
            foreach (ProgressItem syncItem in new List<ProgressItem>() { progressItem })
            {
                refreshProgressItem.RefreshSingleUpdateProgressItem(syncItem);
            }

            //********************************************************************
            // Schreibe den Tabellenname (z.B. Color) und die zugehörige
            // Updatedatum (in Ticks) in die AppSettings (für die nächste Downloadanfrage)
            //********************************************************************
            plugIn.SetTableUpdateDateTimeTicks(tableName, processDbListsResult.Value);

            //*********************************************************************************
            // Houskeeping :-)
            // Lösche verarbeitete, bereitgestellte und somit nicht mehr benötigte Datenpakete
            // am Server
            //*********************************************************************************
            using (HttpClient client = new())
            {
                UriBuilder builder = new(baseDataWebServiceHost + "/DeleteZIPFileInBlob")
                {
                    Port = -1
                };
                System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(builder.Query);
                query["filename"] = filename;
                builder.Query = query.ToString();
                string url = builder.ToString();

                HttpResponseMessage data = await client.GetAsync(url);
                string jsonResponse = await data.Content.ReadAsStringAsync();
            }
            //*********************************************************************************

            //*********************************************************************************
            // Gebe die geschriebenen Änderungen (Datensätze) zurück
            //*********************************************************************************
            return processDbListsResult.Key;
        }
        catch (Exception ex)
        {
            return -1;
        }
    }
}