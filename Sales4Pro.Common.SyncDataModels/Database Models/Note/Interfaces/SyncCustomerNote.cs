using System;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncCustomerNote
{
    string Id { get; set; }
    DateTimeOffset NoteCreated { get; set; }
    string CustomerNumber { get; set; }
    string NoteText { get; set; }
    byte[] NoteImage { get; set; }
}
