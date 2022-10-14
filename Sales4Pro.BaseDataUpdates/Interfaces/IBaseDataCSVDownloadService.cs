using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public interface IBaseDataCSVDownloadService
{
 
    event EventHandler BaseDataDeleted;
    event PropertyChangedEventHandler PropertyChanged;
    event EventHandler<bool> ShowBaseDataUpdateToast;
  
    bool ComputeIsBaseDataDownloadButtonEnabled { get; }
    bool ComputeIsInitialUpdateCompleted { get; }
    string ComputeLastSuccessfulBaseDataUpdateCheckDateTimeString { get; }
    IBaseDataDownloadPlugIn InjectedPlugIn { get; set; }
    bool IsBaseDataDownloadRunning { get; }
    bool IsCheckingForUpdates { get; set; }
    void FillInitialProgressItemVMs();
    Task ResetUpdateAsync(bool raiseBaseDataDeleted);
    Task<bool> UpdateAsync(bool showToast, string currentLoginUserName);

}