using BaseDataHost.AzureServices;
using CsvHelper;
using Dapper;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Text;
using Z.Dapper.Plus;

namespace MyConveno.Toolkit.Sales4Pro.Server.BaseDataHost
{
    public class CSVBaseDataService : IDisposable
    {
        private readonly IConfiguration _config;

        readonly long minDatetimeTicks = new DateTime(2000, 2, 1).Ticks;
        readonly AzureBlobStorageServices azureService;
        readonly SqlConnection connection;

        public CSVBaseDataService(IConfiguration config)
        {

            // *****************************************************************
            // Dapper Plus licensing
            string licenseName = "5529;700-myconveno.de";              // Lizenz für SQLServer!
            string licenseKey = "27c20de2-a412-b5bf-7d1a-d086088c0759";// Lizenz für SQLServer!
            DapperPlusManager.AddLicense(licenseName, licenseKey);

            string licenseErrorMessage;
            if (!DapperPlusManager.ValidateLicense(out licenseErrorMessage))
            {
                throw new Exception(licenseErrorMessage);
            }
            // *****************************************************************

            _config = config;

            connection = new SqlConnection(_config.GetConnectionString("SQLAZURECONNSTR_ClientDB"));
            azureService = new AzureBlobStorageServices(_config.GetValue<string>("AzureBLOBContainerName"));
        }

        internal int GetTableChangesCount(string tableName, long SyncDateTimeTicks)
        {
            try
            {
                StringBuilder sbSelect = new();

                if (SyncDateTimeTicks < minDatetimeTicks)
                {
                    sbSelect.Append("SELECT COUNT(*) FROM [" + tableName + "] ");
                    //sbSelect.Append("WHERE [" + tableName + "].SyncDateTimeTicks > " + SyncDateTimeTicks + " ");
                    sbSelect.Append("WHERE [" + tableName + "].IsDeleted = 0 ");
                }
                else
                {
                    sbSelect.Append("SELECT COUNT(*) FROM [" + tableName + "] ");
                    sbSelect.Append("WHERE [" + tableName + "].SyncDateTimeTicks > " + SyncDateTimeTicks + " ");
                }

                int count = connection.ExecuteScalar<int>(sbSelect.ToString());
                return count;
            }
            catch (Exception ex)
            {
                ex.ToString();
                //////SendMail("sascha.weber@myconveno.de", ex.Message, "Error getTableChangesCount (" + tableName + ")", string.Empty, UserID, UserID);
                return 0;
            }
        }

        internal string CreateAndUploadZippedCSVPackage(string tableName, long syncdatetimeticks)
        {
            string fileName = Guid.NewGuid().ToString() + ".zip";

            StringBuilder sbSelect = new();

            if (syncdatetimeticks < minDatetimeTicks)  // Daten werden initial geladen
            {
                sbSelect.Append("SELECT * ");
                sbSelect.Append("FROM " + tableName + " ");
                sbSelect.Append("WHERE IsDeleted = 0 ");
            }
            else // Daten werden differentiell (delta) geladen
            {
                sbSelect.Append("SELECT * ");
                sbSelect.Append("FROM " + tableName + " ");
                //sbSelect.Append("WHERE IsDeleted = 0 ");
                sbSelect.Append("WHERE SyncDateTimeTicks > " + syncdatetimeticks);
            }

            List<dynamic> items = connection.Query(sbSelect.ToString()).ToList();

            if (items.Count == 0)
                return string.Empty;

            using (MemoryStream mem = new())
            using (StreamWriter writer = new(mem))
            using (CsvWriter csvWriter = new(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.Delimiter = "|";
                csvWriter.WriteRecords(items);

                writer.Flush();
                string result = Encoding.UTF8.GetString(mem.ToArray());
                byte[] data = Encoding.UTF8.GetBytes(result);

                using (MemoryStream OutStream = new())
                {
                    using (ZipOutputStream zipstream = new(OutStream))
                    {
                        ZipEntry entry = new("data.csv");
                        zipstream.PutNextEntry(entry);
                        zipstream.Write(data, 0, Convert.ToInt32(data.Length));
                        zipstream.Close();
                    }
                    byte[] zipcontent = OutStream.ToArray();

                    azureService.UploadByteArray(zipcontent, fileName);
                }
            }
            return fileName;
        }

        internal void DeleteZIPFileFromBLOBStorage(string file)
        {
            // Lösche die Datei aus dem Blob Storage
            azureService.DeleteFile(file);
        }

        public void Dispose()
        { }
    }
}
