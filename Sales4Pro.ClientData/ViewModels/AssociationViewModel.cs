using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class AssociationViewModel : ObservableObject
{
    public AssociationViewModel()
    {
        AssociationId = string.Empty;
        AssociationName = string.Empty;
    }

    public override string ToString()
    {
        return AssociationName;
    }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string associationId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string associationName;

    #endregion

    #region Computed Properties

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(AssociationId) ||
                string.IsNullOrEmpty(AssociationName))
                return false;
            else
                return true;
        }
    }

    #endregion

    public void PasteData(Association association)
    {
        if (association == null)
        {
            AssociationId =  string.Empty;
            AssociationName = string.Empty;
        }
        else
        {
            AssociationId = association.AssociationId;
            AssociationName = association.AssociationName;
        }
        OnPropertyChanged(nameof(ComputeIsPrimaryButtonEnabled));
    }

    public Association GetModel()
    {
        Association model = new()
        {
            AssociationId = AssociationId,
            AssociationName = AssociationName,
        };
        return model;
    }

}
