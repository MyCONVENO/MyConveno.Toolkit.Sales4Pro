using Microsoft.Datasync.Client;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncCustomerNote : DatasyncClientData, IEquatable<SyncCustomerNote>
{
    public string? CustomerNumber { get; set; }
    public string? NoteText { get; set; }
    public byte[]? NoteImage { get; set; }
    public DateTimeOffset NoteCreated { get; set; }

    bool IEquatable<SyncCustomerNote>.Equals(SyncCustomerNote? other)
    => other != null && other.Id == Id && other.CustomerNumber == CustomerNumber && other.NoteText == NoteText;

}
