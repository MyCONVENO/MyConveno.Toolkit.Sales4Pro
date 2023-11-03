using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public partial class ObservableProgressItem : ObservableObject
{
    public enum CurrentBaseDataUpdateStatesEnum { Pending, Unchanged, UpdatesAvailable, Downloading, Updating, Error }

    public ObservableProgressItem()
    { }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsPendingStatusVisible))]
    [NotifyPropertyChangedFor(nameof(IsErrorStatusVisible))]
    [NotifyPropertyChangedFor(nameof(IsDownloadingStatusVisible))]
    [NotifyPropertyChangedFor(nameof(IsUnchangedStatusVisible))]
    [NotifyPropertyChangedFor(nameof(IsUpdatesAvailableStatusVisible))]
    [NotifyPropertyChangedFor(nameof(IsUpdatingStatusVisible))]
    [NotifyPropertyChangedFor(nameof(IsProgressActive))]
    [NotifyPropertyChangedFor(nameof(IsLastUpdateDateVisible))]
    public CurrentBaseDataUpdateStatesEnum currentBaseDataUpdateState;

    [ObservableProperty]
    private string tableName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Progress))]
    [NotifyPropertyChangedFor(nameof(IsTotalChangesTextBlockVisible))]
    private int totalChanges;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Progress))]
    [NotifyPropertyChangedFor(nameof(ComputeChangedText))]
    private int changed;

    [ObservableProperty]
    private string progressText;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DataProvidedOpacity))]
    private DateTime lastUpdate;

    #endregion

    #region Computed Properties

    public string ComputeChangedText
    {
        get { return Changed > 0 ? Changed.ToString() : string.Empty; }
    }
       
    public double DataProvidedOpacity
    {
        get
        {
            if (LastUpdate > new DateTime(2000, 01, 01))
                return 1.0;
            else
                return 0.4;
        }
    }

    public bool IsPendingStatusVisible
    {
        get { return CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Pending; }
    }

    public bool IsErrorStatusVisible
    {
        get { return CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Error; }
    }

    public bool IsDownloadingStatusVisible
    {
        get { return CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Downloading; }
    }

    public bool IsUnchangedStatusVisible
    {
        get { return CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Unchanged; }
    }

    public bool IsUpdatesAvailableStatusVisible
    {
        get { return CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.UpdatesAvailable; }
    }

    public bool IsUpdatingStatusVisible
    {
        get { return CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Updating; }
    }

    public bool IsProgressActive
    {
        get
        {
            switch (CurrentBaseDataUpdateState)
            {
                case CurrentBaseDataUpdateStatesEnum.Downloading:
                    return true;
                case CurrentBaseDataUpdateStatesEnum.Error:
                    return false;
                case CurrentBaseDataUpdateStatesEnum.Unchanged:
                    return false;
                case CurrentBaseDataUpdateStatesEnum.UpdatesAvailable:
                    return false;
                case CurrentBaseDataUpdateStatesEnum.Updating:
                    return true;
                default:
                    return false;
            }
        }
    }

    public bool IsLastUpdateDateVisible
    {
        get
        {
            return CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Pending ||
                   CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Unchanged;
        }
    }

    public bool IsTotalChangesTextBlockVisible
    {
        get
        {
            return TotalChanges > 0 &&
                   (CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.UpdatesAvailable ||
                    CurrentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Downloading);
        }
    }

    public double Progress
    {
        get { return (double)Changed / (double)TotalChanges; }
    }

    #endregion

}
