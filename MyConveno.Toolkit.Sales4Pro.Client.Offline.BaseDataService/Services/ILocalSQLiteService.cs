using Microsoft.Data.Sqlite;

namespace MyConveno.Toolkit.Sales4Pro.Client.Offline.BaseDataService
{
    public interface ILocalSQLiteService
    {
        SqliteConnection Connection { get; set; }
    }
}