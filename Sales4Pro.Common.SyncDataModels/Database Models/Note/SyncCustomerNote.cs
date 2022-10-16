using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncCustomerNote: ISyncCustomerNote
{
    [Newtonsoft.Json.JsonProperty("Id")]
    public string Id { get; set; }

    [Newtonsoft.Json.JsonProperty("NoteCreated")]
    public DateTimeOffset NoteCreated { get; set; }

    [Newtonsoft.Json.JsonProperty("CustomerNumber")]
    public string CustomerNumber { get; set; }

    [Newtonsoft.Json.JsonProperty("NoteText")]
    public string NoteText { get; set; }

    [Newtonsoft.Json.JsonProperty("NoteImage")]
    public byte[] NoteImage { get; set; }

}
