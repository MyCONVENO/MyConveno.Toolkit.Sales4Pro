using System.Collections.Generic;
using System.Linq;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

internal static class RefreshUpdateRefreshLocalTablesList
{
    //**********************************************************************************
    // Lade eine temporäre Liste mit Items vom Typ [ProgressItem], die die Anzahl
    // der geänderten Datensätze enthält
    // Wichtig ist hier nur der Eintrag [TotalChanges] in den einzelnen Items (Tabellen)
    //**********************************************************************************

    internal static List<ProgressItem> RefreshList(IBaseDataDownloadPlugIn plugIn, 
                                                   List<ProgressItem> itemswithchanges, 
                                                   RefreshProgressItem refreshProgressItem)
    {
        //*****************************************************************************************************
        // Gehe durch jedes Objekt (UpdateProgressItem einer Tabelle), wenn die Tabelle Einträge enthält 
        //*****************************************************************************************************

        List<ProgressItem> syncTables;


        // Erzeuge bei jedem Update-Aufruf eine neue Instanz der syncTables mit leeren Objekten (UpdateProgressItem)
        // Diese Tabelle wird von dieser Funktion zurückgegeben
        List<ProgressItem> syncProgressItems = plugIn.GetAllProgressItemsWithLocalUpdateDateTime();

        if (itemswithchanges != null && itemswithchanges.Count > 0)
        {
            // Gehe durch alle UpdateProgressItem der Tabelle [changes]
            foreach (ProgressItem syncProgressItem in itemswithchanges)
            {
                // Hole die Instanz vom Typ [UpdateProgressItem] aus syncTables mit Hilfe einer Suche gleicher [TableName]
                ProgressItem item = syncProgressItems.FirstOrDefault(s => s.TableName == syncProgressItem.TableName);

                // Wenn ein Eintrag besteht
                if (item != null)
                {
                    // Ersetze TotalChanges mit dem Wert aus [syncTables]
                    item.TotalChanges = syncProgressItem.TotalChanges;

                    // Setzte den lokalisierten Text "UpdatesAvailable"
                    item.ProgressText = "Updates verfügbar";

                    // Setze CurrentBaseDataUpdateState auf UpdatesAvailable
                    item.CurrentBaseDataUpdateState = ProgressItemViewModel.CurrentBaseDataUpdateStatesEnum.UpdatesAvailable;

                }
            }
        }
        else
        {
            // Die Tabelle itemswithchanges war leer, also gib ein neues List Objekt zurück
            syncTables = new List<ProgressItem>();
        }
        //*****************************************************************************************************

        // Die Tabelle changes war gefüllt, also mache hier weiter
        foreach (ProgressItem spi in syncProgressItems)
        {
            if (spi.CurrentBaseDataUpdateState == ProgressItemViewModel.CurrentBaseDataUpdateStatesEnum.Pending)
            {
                spi.CurrentBaseDataUpdateState = ProgressItemViewModel.CurrentBaseDataUpdateStatesEnum.Unchanged;
            }
        }

        // Gebe die ursprüngliche, aus dem REST-ApplicationId geladene List zurück
        syncTables = syncProgressItems;
        // ************************************************************************

        // Melde, dass sich hier etwas geändert hat

        foreach (ProgressItem syncItem in syncTables)
        {
            refreshProgressItem.RefreshSingleUpdateProgressItem(syncItem);
        }

        return syncTables;
    }
}