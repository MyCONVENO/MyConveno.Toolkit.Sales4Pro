namespace MyConveno.Toolkit.Sales4Pro.Common.SyncDataModels.Interfaces;

public interface ISyncMetadataDevice
{
    string AppVersion { get; set; }
    string DeviceID { get; set; }
    string DeviceManufracturer { get; set; }
    string DeviceModel { get; set; }
    string DeviceName { get; set; }
    string DeviceOSName { get; set; }
}
