using Microsoft.Datasync.Client;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Client.AzureMobileAppService;

public class SyncCustomerNote : DatasyncClientData, IEquatable<SyncCustomerNote>
{
    public SyncCustomerNote()
    {
        CustomerNumber = string.Empty;
        NoteText = string.Empty;
        NoteImage = Array.Empty<byte>();
        NoteCreated = DateTimeOffset.Now;
    }

    public string CustomerNumber { get; init; }
    public string NoteText { get; set; }
    public byte[] NoteImage { get; set; }
    public DateTimeOffset NoteCreated { get; init; }

    bool IEquatable<SyncCustomerNote>.Equals(SyncCustomerNote? other)
    => other != null && other.Id == Id && other.CustomerNumber == CustomerNumber && other.NoteText == NoteText;

}
