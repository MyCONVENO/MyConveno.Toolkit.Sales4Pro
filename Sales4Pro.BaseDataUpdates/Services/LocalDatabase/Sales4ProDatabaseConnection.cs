using SQLite;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public class Sales4ProDatabaseConnection : SQLiteAsyncConnection
{
    const int maxErrorCount = 50;

    public Sales4ProDatabaseConnection(string dbFileName) : base(DatabasePath, Flags, true)
    { }

    public static new string DatabasePath
    {
        get
        {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, "Sales4Pro_12.db");
        }
    }

    public const SQLite.SQLiteOpenFlags Flags =
   // open the database in read/write mode
   SQLite.SQLiteOpenFlags.ReadWrite |
   // create the database if it doesn't exist
   SQLite.SQLiteOpenFlags.Create |
   // enable multi-threaded database access
   SQLite.SQLiteOpenFlags.SharedCache;

    //public async Task CreateBaseDataDatabaseTables()
    //{
    //    try
    //    {
    //        await CreateTableAsync<Asset>();
    //        await CreateTableAsync<Article>();
    //        await CreateTableAsync<Color>();
    //        await CreateTableAsync<Customer>();
    //        await CreateTableAsync<ProductImage>();
    //    }
    //    catch (Exception ex)
    //    {
    //        //await ErrorMessages.WriteErrorAsync(ex.Message);
    //        ex.ToString();
    //        throw;
    //    }
    //}

    //public async Task DropBaseDataDatabaseTables()
    //{
    //    await DropTableAsync<Asset>();
    //    await DropTableAsync<Article>();
    //    await DropTableAsync<Color>();
    //    await DropTableAsync<Customer>();
    //    await DropTableAsync<ProductImage>();

    //    await ExecuteAsync("vacuum");
    //}

    public async Task InsertAsyncRetry(object dbItem)
    {
        bool saved;
        int errorCount = 0;
        do
        {
            try
            {
                await InsertAsync(dbItem);
                saved = true;
            }
            catch (Exception)
            {
                //await ErrorMessages.WriteErrorAsync(ex);
                saved = false;
                await Task.Delay(200);
                errorCount++;
                if (errorCount > maxErrorCount)
                    throw new Exception("InsertAsyncRetry");
            }
        } while (!saved);
    }

    public async Task UpdateAsyncRetry(object dbItem)
    {
        bool saved;
        int errorCount = 0;
        do
        {
            try
            {
                await UpdateAsync(dbItem);
                saved = true;
            }
            catch (Exception)
            {
                //await ErrorMessages.WriteErrorAsync(ex);
                saved = false;
                await Task.Delay(200);
                errorCount++;
                if (errorCount > maxErrorCount)
                    throw new Exception("UpdateAsyncRetry");
            }
        } while (!saved);
    }

    public async Task DeleteAsyncRetry(object dbItem)
    {
        bool saved;
        int errorCount = 0;
        do
        {
            try
            {
                await DeleteAsync(dbItem);
                saved = true;
            }
            catch (Exception)
            {
                //await ErrorMessages.WriteErrorAsync(ex);
                saved = false;
                await Task.Delay(200);
                errorCount++;
                if (errorCount > maxErrorCount)
                    throw new Exception("DeleteAsyncRetry");
            }
        } while (!saved);
    }

    public async Task InsertAllAsyncRetry(IEnumerable dbItems)
    {
        bool saved;
        int errorCount = 0;
        do
        {
            try
            {
                await InsertAllAsync(dbItems);
                saved = true;
            }
            catch (Exception)
            {
                //await ErrorMessages.WriteErrorAsync(ex);
                saved = false;
                await Task.Delay(200);
                errorCount++;
                if (errorCount > maxErrorCount)
                    throw new Exception("InsertAllAsyncRetry");

            }
        } while (!saved);
    }

    public async Task UpdateAllAsyncRetry(IEnumerable dbItems)
    {
        bool saved;
        int errorCount = 0;
        do
        {
            try
            {
                await UpdateAllAsync(dbItems);
                saved = true;
            }
            catch (Exception)
            {
                //await ErrorMessages.WriteErrorAsync(ex);
                saved = false;
                await Task.Delay(200);
                errorCount++;
                if (errorCount > maxErrorCount)
                    throw new Exception("UpdateAllAsyncRetry");
            }
        } while (!saved);
    }

    public async Task ExecuteAsyncRetry(string command)
    {
        bool saved;
        int errorCount = 0;
        do
        {
            try
            {
                await ExecuteAsync(command);
                saved = true;
            }
            catch (Exception)
            {
                //await ErrorMessages.WriteErrorAsync(ex);
                saved = false;
                await Task.Delay(200);
                errorCount++;
                if (errorCount > maxErrorCount)
                    throw new Exception("ExecuteAsyncRetry");
            }
        } while (!saved);
    }

}