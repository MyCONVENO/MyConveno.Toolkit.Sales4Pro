using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncShoppingCart : ISyncShoppingCart
{
    public SyncShoppingCart()
    {
        Header = new SyncMetadataHeader();
        Customer = new SyncMetadataCustomer();
        Conditions = new SyncMetadataConditions();

        OrderDate = DateTime.Today.Date;
        SentDateTime = new DateTime(1950, 1, 1);

        HeaderMetadata = string.Empty;
        CustomerMetadata = string.Empty;
        ConditionsMetadata = string.Empty;

        SyncShoppingCartItems = new List<SyncShoppingCartItem>();
    }

    [JsonProperty("id")]
    public string Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public int StatusID { get; set; }
    public string LabelNumber { get; set; }
    public string SeasonNumber { get; set; }
    public string OrderTypeNumber { get; set; }
    public string UserID { get; set; }
    public int ConfirmationStatus { get; set; }
    public DateTime SentDateTime { get; set; }

    public string HeaderMetadata { get; set; }
    public string CustomerMetadata { get; set; }
    public string ConditionsMetadata { get; set; }

    [JsonIgnore]
    public ISyncMetadataHeader Header { get; set; }

    [JsonIgnore]
    public ISyncMetadataAgent Agent { get; set; }

    [JsonIgnore]
    public ISyncMetadataCustomer Customer { get; set; }

    [JsonIgnore]
    public ISyncMetadataDeliveryAddress DeliveryAddress { get; set; }

    [JsonIgnore]
    public ISyncMetadataInvoiceAddress InvoiceAddress { get; set; }

    [JsonIgnore]
    public ISyncMetadataAssociation Association { get; set; }

    [JsonIgnore]
    public ISyncMetadataConditions Conditions { get; set; }

    [JsonIgnore]
    public ISyncMetadataDevice Device { get; set; }

    [JsonIgnore]
    public List<SyncShoppingCartItem> SyncShoppingCartItems { get; set; }


    public void SerializeMetadata()
    {
        JsonSerializerSettings settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        HeaderMetadata = JsonConvert.SerializeObject(Header, settings);
        CustomerMetadata = JsonConvert.SerializeObject(Customer, settings);
        ConditionsMetadata = JsonConvert.SerializeObject(Conditions, settings);
    }

    public void DeserializeMetadata()
    {
        JsonSerializerSettings settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        if (!string.IsNullOrEmpty(HeaderMetadata.Trim())) // Wichtig!, sonst wird Header auf null gesetzt
            Header = JsonConvert.DeserializeObject<SyncMetadataHeader>(HeaderMetadata, settings);


        if (!string.IsNullOrEmpty(CustomerMetadata.Trim())) // Wichtig!, sonst wird Customer auf null gesetzt
            Customer = JsonConvert.DeserializeObject<SyncMetadataCustomer>(CustomerMetadata, settings);
            

        if (!string.IsNullOrEmpty(ConditionsMetadata.Trim())) // Wichtig!, sonst wird Conditions auf null gesetzt
            Conditions = JsonConvert.DeserializeObject<SyncMetadataConditions>(ConditionsMetadata, settings);

    }

}