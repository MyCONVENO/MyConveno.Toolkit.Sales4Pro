using MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels;

public class SyncMetadataDevice : ISyncMetadataDevice
{
    public string DeviceID { get; set; }
    public string DeviceModel { get; set; }
    public string DeviceManufracturer { get; set; }
    public string DeviceName { get; set; }
    public string DeviceOSName { get; set; }
    public string AppVersion { get; set; }
}
