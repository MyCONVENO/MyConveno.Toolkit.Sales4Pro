using Newtonsoft.Json;
using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
using System;
using System.Collections.Generic;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncShoppingCart : ISyncShoppingCart
{
    public SyncShoppingCart()
    {
        Agent = new SyncMetadataAgent();
        Header = new SyncMetadataHeader();
        Customer = new SyncMetadataCustomer();
        DeliveryAddress = new SyncMetadataDeliveryAddress();
        InvoiceAddress = new SyncMetadataInvoiceAddress();
        Conditions = new SyncMetadataConditions();
        Association = new SyncMetadataAssociation();
        Device = new SyncMetadataDevice();
       
        OrderDate = DateTime.Today.Date;
        SentDateTime = new DateTime(1950, 1, 1);

        AgentMetadata = string.Empty;
        HeaderMetadata = string.Empty;
        CustomerMetadata = string.Empty;
        DeliveryAddressMetadata = string.Empty;
        InvoiceAddressMetadata = string.Empty;
        ConditionsMetadata = string.Empty;
        AssociationMetadata = string.Empty;
        DeviceMetadata = string.Empty;

        SyncShoppingCartItems = new List<SyncShoppingCartItem>();
    }

    [JsonProperty("id")]
    public string Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public string LabelNumber { get; set; }
    public string LabelName { get; set; }
    public string SeasonNumber { get; set; }
    public string SeasonName { get; set; }
    public string SeasonLongName { get; set; }
    public string OrderTypeNumber { get; set; }
    public string OrderTypeName { get; set; }
    public int StatusID { get; set; }
    public string UserID { get; set; }
    public int ConfirmationStatus { get; set; }
    public bool Sent { get; set; }
    public DateTime SentDateTime { get; set; }

    public string HeaderMetadata { get; set; }
    public string AgentMetadata { get; set; }
    public string CustomerMetadata { get; set; }
    public string DeliveryAddressMetadata { get; set; }
    public string InvoiceAddressMetadata { get; set; }
    public string AssociationMetadata { get; set; }
    public string ConditionsMetadata { get; set; }
    public string DeviceMetadata { get; set; }

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
        AgentMetadata = JsonConvert.SerializeObject(Agent, settings);
        CustomerMetadata = JsonConvert.SerializeObject(Customer, settings);
        DeliveryAddressMetadata = JsonConvert.SerializeObject(DeliveryAddress, settings);
        InvoiceAddressMetadata = JsonConvert.SerializeObject(InvoiceAddress, settings);
        AssociationMetadata = JsonConvert.SerializeObject(Association, settings);
        ConditionsMetadata = JsonConvert.SerializeObject(Conditions, settings);
        DeviceMetadata = JsonConvert.SerializeObject(Device, settings);
    }

    public void DeserializeMetadata()
    {
        JsonSerializerSettings settings = new()
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        if (!string.IsNullOrEmpty(HeaderMetadata.Trim())) // Wichtig!, sonst wird Header auf null gesetzt
            Header = JsonConvert.DeserializeObject<SyncMetadataHeader>(HeaderMetadata, settings);

        if (!string.IsNullOrEmpty(AgentMetadata)) // Wichtig!, sonst wird Agent auf null gesetzt
            Agent = JsonConvert.DeserializeObject<SyncMetadataAgent>(AgentMetadata, settings);

        if (!string.IsNullOrEmpty(CustomerMetadata.Trim())) // Wichtig!, sonst wird Customer auf null gesetzt
            Customer = JsonConvert.DeserializeObject<SyncMetadataCustomer>(CustomerMetadata, settings);

        if (!string.IsNullOrEmpty(DeliveryAddressMetadata.Trim())) // Wichtig!, sonst wird DeliveryAddress auf null gesetzt
            DeliveryAddress = JsonConvert.DeserializeObject<SyncMetadataDeliveryAddress>(DeliveryAddressMetadata, settings);

        if (!string.IsNullOrEmpty(InvoiceAddressMetadata.Trim())) // Wichtig!, sonst wird InvoiceAddress auf null gesetzt
            InvoiceAddress = JsonConvert.DeserializeObject<SyncMetadataInvoiceAddress>(InvoiceAddressMetadata, settings);

        if (!string.IsNullOrEmpty(AssociationMetadata.Trim())) // Wichtig!, sonst wird Association auf null gesetzt
            Association = JsonConvert.DeserializeObject<SyncMetadataAssociation>(AssociationMetadata, settings);

        if (!string.IsNullOrEmpty(ConditionsMetadata.Trim())) // Wichtig!, sonst wird Conditions auf null gesetzt
            Conditions = JsonConvert.DeserializeObject<SyncMetadataConditions>(ConditionsMetadata, settings);

        if (!string.IsNullOrEmpty(DeviceMetadata.Trim())) // Wichtig!, sonst wird Device auf null gesetzt
            Device = JsonConvert.DeserializeObject<SyncMetadataDevice>(DeviceMetadata, settings);
    }

}