using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class PaymentTermViewModel : ObservableObject
{
    public PaymentTermViewModel()
    {
        PaymentTermId = string.Empty;
        PaymentTermName = string.Empty;
    }

    public PaymentTermViewModel(PaymentTerm paymentTerm) : this()
    {
       PasteData(paymentTerm);
    }

    public override string ToString()
    {
        return PaymentTermName;
    }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string paymentTermId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string paymentTermName;

    #endregion

    #region Computed Properties

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(PaymentTermId) ||
                string.IsNullOrEmpty(PaymentTermName))
                return false;
            else
                return true;
        }
    }

    #endregion

    public void PasteData(PaymentTerm paymentTerm)
    {
        if (paymentTerm == null)
        {
            PaymentTermId =  string.Empty;
            PaymentTermName = string.Empty;
        }
        else
        {
            PaymentTermId = paymentTerm.PaymentTermId;
            PaymentTermName = paymentTerm.PaymentTermName;
        }
        OnPropertyChanged(nameof(ComputeIsPrimaryButtonEnabled));
    }

    public PaymentTerm GetModel()
    {
        PaymentTerm model = new()
        {
            PaymentTermId = PaymentTermId,
            PaymentTermName = PaymentTermName,
        };
        return model;
    }

}
