using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataUpdates;

public partial class ProgressItemViewModel : ObservableObject
{
    public enum CurrentBaseDataUpdateStatesEnum { Pending, Unchanged, UpdatesAvailable, Downloading, Updating, Error }

    public ProgressItemViewModel()
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
    [NotifyPropertyChangedFor(nameof(LastUpdateText))]
    [NotifyPropertyChangedFor(nameof(DataProvidedOpacity))]
    private DateTime lastUpdate;

    #endregion

    #region Computed Properties

    public string ComputeChangedText
    {
        get { return Changed > 0 ? Changed.ToString() : string.Empty; }
    }

    public string LastUpdateText
    {
        get
        {
            if (lastUpdate > new DateTime(2000, 01, 01))
                return "Letzte Serveraktualisierung: " + lastUpdate.ToString("dd.MM.yy HH:mm");
            else
                return "Noch keine Daten bereitgestellt";

        }
    }

    public double DataProvidedOpacity
    {
        get
        {
            if (lastUpdate > new DateTime(2000, 01, 01))
                return 1.0;
            else
                return 0.4;
        }
    }

    public bool IsPendingStatusVisible
    {
        get { return currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Pending; }
    }

    public bool IsErrorStatusVisible
    {
        get { return currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Error; }
    }

    public bool IsDownloadingStatusVisible
    {
        get { return currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Downloading; }
    }

    public bool IsUnchangedStatusVisible
    {
        get { return currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Unchanged; }
    }

    public bool IsUpdatesAvailableStatusVisible
    {
        get { return currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.UpdatesAvailable; }
    }

    public bool IsUpdatingStatusVisible
    {
        get { return currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Updating; }
    }

    public bool IsProgressActive
    {
        get
        {
            switch (currentBaseDataUpdateState)
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
            return currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Pending ||
                   currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Unchanged;
        }
    }

    public bool IsTotalChangesTextBlockVisible
    {
        get
        {
            return totalChanges > 0 &&
                   (currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.UpdatesAvailable ||
                    currentBaseDataUpdateState == CurrentBaseDataUpdateStatesEnum.Downloading);
        }
    }

    public double Progress
    {
        get { return (double)Changed / (double)TotalChanges; }
    }

    #endregion

}
