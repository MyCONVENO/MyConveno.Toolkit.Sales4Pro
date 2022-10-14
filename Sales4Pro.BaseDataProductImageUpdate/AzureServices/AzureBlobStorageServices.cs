using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

internal class AzureBlobStorageServices : IDisposable
{
    readonly CloudStorageAccount storageAccount;
    readonly CloudBlobClient blobClient;
    readonly CloudBlobContainer container;

    internal AzureBlobStorageServices(string containerName)
    {
        storageAccount = CreateAzureStorageAccountFromConnectionString();
        blobClient = storageAccount.CreateCloudBlobClient();
        container = blobClient.GetContainerReference(containerName);
    }

    void IDisposable.Dispose()
    { }

    private CloudStorageAccount CreateAzureStorageAccountFromConnectionString()
    {
        CloudStorageAccount storageAccount;
        try
        {
            string clientId = "DefaultEndpointsProtocol=https;AccountName=myconvenocoredata;AccountKey=7STxKhoKsGQt2sed2JGb4gWtSIvzYj2SJ/PIFLW3AL2ch0FyTCJ1QAvEYBOyQo64iQZIa2z/NcwDTKGn/660vQ==;EndpointSuffix=core.windows.net";
            storageAccount = CloudStorageAccount.Parse(clientId);
        }
        catch (FormatException)
        {
            throw new FormatException("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
        }

        return storageAccount;
    }

    public string DownloadAndUnzipPackageFileAsync(string filename)
    {
        try
        {
            // *********************************************************

            CloudBlockBlob blob = container.GetBlockBlobReference(filename);
            blob.FetchAttributes(); // wird benötigt, damit blob.Properties.Length gefüllt wird
            byte[] fileContent = new byte[blob.Properties.Length];
            blob.DownloadToByteArray(fileContent, 0);

            string csvdatastring = Unzip(fileContent);
            return csvdatastring; // der CSV-Content als string
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static string Unzip(byte[] zippedBuffer)
    {
        using (MemoryStream zippedStream = new MemoryStream(zippedBuffer))
        {
            using (ZipArchive archive = new ZipArchive(zippedStream))
            {
                ZipArchiveEntry entry = archive.Entries.FirstOrDefault();

                if (entry != null)
                {
                    using (Stream unzippedEntryStream = entry.Open())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            unzippedEntryStream.CopyTo(ms);
                            byte[] unzippedArray = ms.ToArray();

                            return Encoding.Default.GetString(unzippedArray);
                        }
                    }
                }
                return null;
            }
        }
    }

    public List<T> DownloadFileAndExtractRecords<T>(string filename)
    {
        string csvdatastring = DownloadAndUnzipPackageFileAsync(filename);

        List<T> dataList = new List<T>();

        // *********************************************************
        // Extrahiere aus dem string 'csvdatastring' eine Liste mit Datensätzen,
        // die dem Schema T (z.B. Color) entsprechen
        // *********************************************************
        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|",
            HeaderValidated = null,
            MissingFieldFound = null
        };
        using (StringReader reader = new StringReader(csvdatastring))
        using (CsvReader csvReader = new CsvReader(reader, config, false))
        {
            dataList = csvReader.GetRecords<T>().ToList();
        }

        return dataList;
    }

}
