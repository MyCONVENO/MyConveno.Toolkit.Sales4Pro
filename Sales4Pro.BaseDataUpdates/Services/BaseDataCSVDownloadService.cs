﻿using Z.Dapper.Plus;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public partial class BaseDataCSVDownloadService : IBaseDataCSVDownloadService
{
    private readonly string baseDataWebServiceHost = string.Empty;
    private bool isInternalUpdateIsRunning = false;

    public BaseDataCSVDownloadService(string webServiceHost, IBaseDataDownloadPlugIn plugIn)
    {
        baseDataWebServiceHost = webServiceHost;
        InjectedPlugIn = plugIn;

        // *****************************************************************
        // Dapper Plus licensing
        string licenseName = "5529;700-myconveno.de"; // für SQLite !!!!!
        string licenseKey = "27c20de2-a412-b5bf-7d1a-d086088c0759"; // für SQLite !!!!!

        //string licenseKey = "0228402f-b580-4cfe-68ec-87bebc7f0efd"; // für SQL Server !!!!!
        // ACHTUNG!! Diese Lizenz gilt nur für Dapper.Plus bis Version 4.0.26 !!!!!!
        DapperPlusManager.AddLicense(licenseName, licenseKey);

        string licenseErrorMessage;
        if (!DapperPlusManager.ValidateLicense(out licenseErrorMessage))
        {
            throw new Exception(licenseErrorMessage);
        }
        // *****************************************************************

    }

    #region Properties

    public IBaseDataDownloadPlugIn InjectedPlugIn { get; set; }

    #endregion

    #region Methods, Functions and Tasks

    /// <summary>
    /// Dieser Task wird aufgerufen, wenn Im Client auf die Schaltfläche Aktualisierungen in der Symbolleiste oben rechts gedrückt wird.
    /// Das Flyout wird geöffnet und die Liste gefüllt
    /// Baue die ObservableCollection ProgressItemVMs auf (alles lokal)
    /// Der Status der einzelnen Tabellen wird aus den AppSettings geholt (LastSuccessfulUpdateDateTime" + TableName)
    /// </summary>
    public void FillInitialProgressItemVMs()
    {
        foreach (ProgressItem progressItem in InjectedPlugIn.GetAllProgressItemsWithLocalUpdateDateTime())
        {
            InjectedPlugIn.RefreshProgressItem.RefreshSingleUpdateProgressItem(progressItem);
        }
    }

    public async Task<List<ProgressItem>> CheckForUpdateAsync()
    {
        List<ProgressItem> downloadTableProgressItems = new();

        try
        {
            // breche hier ab, wenn bereits ein Update läuft
            if (isInternalUpdateIsRunning == false)
            {
                // Blockiere eventuell neu gestartete Updates
                isInternalUpdateIsRunning = true;

                // Melde zurück, dass nach Updates gesucht wird (InfoText "... Dauert wenige Sekunden ...")

                //**********************************************************************************
                //  XXX      Hole eine Liste mit Änderungen vom WebService
                //    X      
                //  XXX      Lade eine temporäre Liste mit Items vom Typ [ProgressItem], die die Anzahl
                //  X        der geänderten Datensätze enthält
                //  XXX X    Wichtig ist hier nur der Eintrag [TotalChanges] in den einzelnen Items (Tabellen)
                //**********************************************************************************
                List<ProgressItem> itemswithchanges = await RefreshUpdatePrepareLocalTablesList.UpdateListOnline(InjectedPlugIn,
                                                                                                                 baseDataWebServiceHost);
                //**********************************************************************************

                //**********************************************************************************
                // Gehe durch jedes Objekt (UpdateProgressItem einer Tabelle), wenn die Liste
                // (itemswithchanges) Einträge enthält 
                //**********************************************************************************
                downloadTableProgressItems = RefreshUpdateRefreshLocalTablesList.RefreshList(InjectedPlugIn,
                                                                                             itemswithchanges,
                                                                                             InjectedPlugIn.RefreshProgressItem);


                // ************************************************************************
                // ************************************************************************
                // Die Liste mit den Tabellen ist jetzt vorbereitet und zeigt die Anzahl
                // der zu ändernden oder neuen Datensätze an.
                // Jetzt können wir basierend auf dieser Liste die Updates durchführen
                // ************************************************************************
                // ************************************************************************

            }
            return downloadTableProgressItems;
        }
        catch (Exception)
        {
            return new List<ProgressItem>();
        }
        finally
        {
            // Es dürfen wieder Aktualisierungen gestartet werden
            isInternalUpdateIsRunning = false;
        }
    }


    /// <summary>
    /// Hier wird das eigentliche Update ausgeführt.
    /// Das LoginToken (z.B. michael.coelsch@outlook.de [Die UserID aus User] wird lokal in den AppSettings gespeichert
    /// Es wird die lokale Datenbank erstellt und die Tabellen erstellt, wenn nicht schon da.
    /// </summary>
    /// <param name="loginUserName">Gibt bei Erfolg true zurück</param>
    /// <returns></returns>
    public async Task<bool> UpdateAsync(List<ProgressItem> syncTables)
    {
        bool updateOK = false;
        bool success = false;

        try
        {
            // breche hier ab, wenn bereits ein Update läuft
            if (isInternalUpdateIsRunning == false || !syncTables.Any())
            {
                // Blockiere eventuell neu gestartete Updates
                isInternalUpdateIsRunning = true;

                success = await InjectedPlugIn.DownloadTablesAsync(syncTables);

                // iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
                // Alles OK oder es wurden keine zu ändernde Datensätze gefunden
                // -> Beende die Aktualisierung
                // iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii
                if (success && syncTables.Count > 0)
                {
                    updateOK = true;

                    // Das erste DataUpdate wurde durchgeführt

                    InjectedPlugIn.SetIsInitialUpdateCompleted(true);

                    // Setzte die globale Variable [LastSuccessfulUpdateDateTime] auf die jetzige Zeit
                    // Diese Variable wird nicht benötigt und kann zu Infozwecken verwendet werden
                    if (syncTables.Any() && syncTables.Sum(s => s.TotalChanges) > 0)
                    {
                        InjectedPlugIn.SetLastSuccessfulBaseDataUpdateCheckDateTime(DateTime.Now);
                    }
                }
                // ************************************************************************

                // Gebe zurück, ob die Aktualisierung erfolgreich war oder nicht.
                success = updateOK;

                if (success)
                    InjectedPlugIn.SetLastSuccessfulBaseDataUpdateCheckDateTime(DateTime.Now);
            }
            else
            {
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                // Es läuft gerade ein Update !! 
                // Setze dennoch IsInitialUpdateCompleted auf true
                InjectedPlugIn.SetIsInitialUpdateCompleted(true);

                success = false;
                // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }
        }
        catch (Exception ex)
        {
            ProgressItem updateresult = new ProgressItem { ProgressText = "END" };
            updateresult.Changed = updateresult.TotalChanges > 0 ? updateresult.TotalChanges : 1;
            updateresult.TotalChanges = updateresult.TotalChanges > 0 ? updateresult.TotalChanges : 1;

            foreach (ProgressItem syncItem in new List<ProgressItem>() { updateresult })
            {
                InjectedPlugIn.RefreshProgressItem.RefreshSingleUpdateProgressItem(syncItem);
            }

            success = false;

            throw new Exception(ex.Message);
        }
        finally
        {
            // Es dürfen wieder Aktualisierungen gestartet werden
            isInternalUpdateIsRunning = false;
        }

        return success;
    }


    /// <summary>
    /// In den Settings im Frontend gibt es einen Button zum Zurücksetzen der Stammdaten.
    /// Dieser Button führt diesen Task aus.
    /// </summary>
    /// <returns></returns>
    public async Task ResetUpdateAsync()
    {
        await InjectedPlugIn.ResetUpdateAsync();
        FillInitialProgressItemVMs();
    }

    #endregion

}