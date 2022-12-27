using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public interface IBaseDataDownloadPlugIn
{
    void ResetUpdate();
    Dictionary<string, long> GetAllUpdateDateTimeTicks();
    List<ProgressItem> GetAllProgressItemsWithLocalUpdateDateTime();
    long GetTableUpdateDateTimeTicks(string tableName);
    bool GetIsInitialUpdateCompleted();
    void SetIsInitialUpdateCompleted(bool completed);
    DateTime GetLastSuccessfulBaseDataUpdateCheckDateTime();
    void SetTableUpdateDateTimeTicks(string tableName, DateTime updateDateTime);
    void SetLastSuccessfulBaseDataUpdateCheckDateTime(DateTime datetime);
    //RefreshProgressItem RefreshProgressItem { get; set; }
    Task<bool> DownloadTablesAsync(List<ProgressItem> syncTables);
    //Sales4ProDatabaseConnection Sales4ProDatabaseConnection { get; set; }
    RefreshProgressItem RefreshProgressItem { get; set; }
}