using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace BaseDataHost.AzureServices
{
    public class AzureBlobStorageServices
    {
        string _containerName = string.Empty;

        public AzureBlobStorageServices(string containerName)
        {
            _containerName = containerName;
        }

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

        public void UploadByteArray(byte[] text, string filename)
        {
            CloudStorageAccount storageAccount = CreateAzureStorageAccountFromConnectionString();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_containerName);

            CloudBlockBlob sourceblob = container.GetBlockBlobReference(filename);

            sourceblob.UploadFromByteArray(text, 0, text.Length);
        }

        public void DeleteFile(string filename)
        {
            CloudStorageAccount storageAccount = CreateAzureStorageAccountFromConnectionString();
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(_containerName);

            CloudBlockBlob blob = container.GetBlockBlobReference(filename);
            blob.DeleteIfExists();
        }

    }
}
