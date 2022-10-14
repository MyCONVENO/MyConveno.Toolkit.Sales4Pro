using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

class FileSerializer
{
    public static async Task<T> GetDeserializedDataAsync<T>(string base64String, bool encode)
    {
        if (encode)
        {
            string unzippedString = UnzipBase64String(base64String);
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(unzippedString));
        }
        else
        {
            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(base64String));
        }
    }

    private static string UnzipBase64String(string ZipBase64String)
    {
        string outstring = string.Empty;
        byte[] t = Convert.FromBase64String(ZipBase64String);
        using (MemoryStream zipstream = new MemoryStream(t))
        {
            ZipArchive zipArchive = new System.IO.Compression.ZipArchive(zipstream);
            foreach (ZipArchiveEntry entry in zipArchive.Entries)
            {
                using (Stream entryStream = entry.Open())
                {

                    using (StreamReader reader = new StreamReader(entryStream, Encoding.UTF8))
                    {
                        try
                        {
                            outstring = reader.ReadToEnd();
                        }
                        catch (Exception ex)
                        {
                            //ErrorMessages.WriteErrorAsync(ex.Message);
                            ex.ToString();
                        }
                    }
                }
            }
        }

        return outstring;
    }

}
