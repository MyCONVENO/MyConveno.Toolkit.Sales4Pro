using Newtonsoft.Json;

namespace MyConveno.Toolkit.Sales4Pro.Client.UserData;

public class User : IUser
    {
        public User()
        {
            MetadataContent = new MetadataUserContent();
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Metadata { get; set; }
              
        [JsonIgnore]
        public MetadataUserContent MetadataContent { get; set; }

        public void SerializeMetadata()
        {
            Metadata = JsonConvert.SerializeObject(MetadataContent);
        }

        public void DeserializeMetadata()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            if (!string.IsNullOrEmpty(Metadata.Trim())) // Wichtig!, sonst wird Content auf null gesetzt
                MetadataContent = JsonConvert.DeserializeObject<MetadataUserContent>(Metadata, settings);
        }

    }
