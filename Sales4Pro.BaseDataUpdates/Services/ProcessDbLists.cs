using Dapper;
using System.Reflection;
using Z.Dapper.Plus;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

internal static class ProcessDbLists
{
    /// <summary>
    /// Der Parameter 'list' enthält alle seit dem letzten Update geänderte Datensätze
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="sales4ProDatabaseConnection"></param>
    /// <returns></returns>

    internal static async Task<KeyValuePair<int, DateTime>> ProcessDbListsAsync<T>(List<T> list, Microsoft.Data.Sqlite.SqliteConnection connection)
    {
        // *********************************************************
        // Hole den Tabellenname (z.B. Color)
        // *********************************************************
        string tableName = typeof(T).Name;

        //DateTime lastSyncDateTime = new(2000, 1, 1);

        // *********************************************************
        // Die übergebene Liste ist leer. Gehe zurück
        // *********************************************************
        if (list == null)
            return new KeyValuePair<int, DateTime>(0, new DateTime(2000, 1, 1));

        // ****************************************************************************
        // Gehe durch alle Listeneinträge
        // ****************************************************************************
        List<T> itemsToBeDeletedList = new();
        List<T> insertList = new();

        long latestsyncticks = new DateTime(2000, 1, 1).Ticks;

        foreach (T item in list)
        {
            // *********************************************************
            // Wir löschen ALLE Datensätze, die übergeben wurden,
            // da ja nur geänderte oder gelöschte Datensätze vorliegen.
            // Datensätze, die nicht das Flag "IsDeleted" gesetzt haben
            // sind ja geänderte Datensätze.
            // Auch die werden gelöscht (entsprechend der ID (z.B. ColorID) und
            // weiter unten neu hinzugefügt.
            // *********************************************************

            // Jeder Datensatz wird um die Platzhalter 'IsDeleted' und 'SyncDateTimeTicks'
            // erweitert und mit den übergebenen Werten gefüllt
            //IBaseModel bitem = item as IBaseModel;

            //Type t = item.GetType();
            PropertyInfo prop = item.GetType().GetProperty("SyncDateTimeTicks");
            long syncticks = (long)prop.GetValue(item);
            latestsyncticks = syncticks > latestsyncticks ? syncticks : latestsyncticks;


            // Füge ALLE Datensätze zur Löschliste hinzu
            itemsToBeDeletedList.Add(item);

            // *********************************************************
            // Wenn der Datensatz NICHT als IsDeleted markiert ist,
            // wird er zur Insert-Liste hinzugefügt
            //if (bitem.IsDeleted == false)
            insertList.Add(item);
        }
        // ****************************************************************************


        // ****************************************************************************
        // Delete records
        // Wir löschen jetzt alle Datensätze deren IDs den übergebenen Datensätzen
        // entsprechen
        // ****************************************************************************
        PropertyInfo property = typeof(T).GetRuntimeProperty(tableName + "ID");
        List<string> iDsToBeDeletedList = new();
        foreach (T di in itemsToBeDeletedList)
        {
            iDsToBeDeletedList.Add(property.GetValue(di).ToString());
        }

        string prodIDCommaString = string.Join(",", iDsToBeDeletedList.Select(p => "'" + p.ToString() + "'"));
        string x = "Delete FROM " + tableName + " WHERE " + tableName + "ID IN (" + prodIDCommaString + ")";
        await connection.ExecuteAsync(x);
        // ****************************************************************************


        // ****************************************************************************
        // Insert records
        // Jetzt fügen wir die Datensätze ein, die entweder neu sind oder geändert wurden
        // (Geänderte Datensätze wurden ja oben bereits gelöscht
        // ****************************************************************************
        if (insertList.Any())
            connection.BulkInsert<T>(insertList);
        // ****************************************************************************


        // ****************************************************************************
        // Wir suchen in der übergebenen Liste nach dem neuesten DateTime Tick
        // und geben diesen als aktuellen Zeitstand der aktualisierung zurück.
        // Das int-Feld enthält die Anzahl der verarbeiteten Datensätze (neu und gelöscht)
        // ****************************************************************************
        //if (itemsToBeDeletedList.Any())
        //{
        //    //lastSyncDateTime = DateTime.Now; //   new DateTime(itemsToBeDeletedList.Max(i => i.SyncDateTimeTicks));


        //    //lastSyncDateTime = new DateTime(itemsToBeDeletedList.Max(i => i.SyncDateTimeTicks));
        //}

        // ****************************************************************************
        // Wir geben ein KeyValuePair mit Anzahl der verarbeiteten Datensätzen und 
        // des neuesten Zeitstempel aller Datensätze zurück
        // ****************************************************************************
        return new KeyValuePair<int, DateTime>(list.Count, new DateTime(latestsyncticks));
    }
}