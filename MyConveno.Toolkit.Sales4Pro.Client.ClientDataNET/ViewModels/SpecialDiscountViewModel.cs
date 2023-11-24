using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class SpecialDiscountViewModel : ObservableObject
{
    public SpecialDiscountViewModel()
    {
        WhiteList = string.Empty;
        SpecialDiscountId = string.Empty;
        PasteData(new SpecialDiscount());
    }

    public SpecialDiscountViewModel(SpecialDiscount specialDiscount) : this()
    {
        PasteData(specialDiscount);
    }

    public override string ToString()
    {
        return SpecialDiscountId;
    }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string specialDiscountId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public DateTime startDate;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public DateTime endDate;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public double initialDiscount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public double discount;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public int qtyStart;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string whiteList;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public double smallInterval;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public double bigInterval;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public bool isStandardOrderScope;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public bool isStockOrderScope;
      
    #endregion

    public DateTimeOffset StartDateDateTimeOffset
    {
        get { return StartDate; }
        set
        {
            StartDate = value.Date;
            OnPropertyChanged();
            OnPropertyChanged(nameof(EndDateDateTimeOffset));
        }
    }

    public DateTimeOffset EndDateDateTimeOffset
    {
        get { return EndDate; }
        set
        {
            EndDate = value.Date;
            OnPropertyChanged();
            OnPropertyChanged(nameof(StartDateDateTimeOffset));
        }
    }

    #region Computed Properties

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(SpecialDiscountId))
                return false;
            else
                return true;
        }
    }

    #endregion

    public void PasteData(SpecialDiscount specialDiscount)
    {
        if (specialDiscount == null)
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(365);
            InitialDiscount = 0.0d;
            Discount = 0.0d;
            QtyStart = 0;
            WhiteList = string.Empty;
            SmallInterval = 0.1d;
            BigInterval = 0.1d;
            IsStandardOrderScope = true;
            IsStockOrderScope = true;
        }
        else
        {
            SpecialDiscountId = specialDiscount.SpecialDiscountId;

            specialDiscount.DeserializeMetadata();
            StartDate = specialDiscount.MetadataContent.StartDate;
            EndDate = specialDiscount.MetadataContent.EndDate;
            InitialDiscount = specialDiscount.MetadataContent.InitialDiscount;
            Discount = specialDiscount.MetadataContent.Discount;
            QtyStart = specialDiscount.MetadataContent.QtyStart;
            WhiteList = specialDiscount.MetadataContent.WhiteList;
            SmallInterval = specialDiscount.MetadataContent.SmallInterval;
            BigInterval = specialDiscount.MetadataContent.BigInterval;
            IsStandardOrderScope = specialDiscount.MetadataContent.IsStandardOrderScope;
            IsStockOrderScope = specialDiscount.MetadataContent.IsStockOrderScope;

        }
        OnPropertyChanged(nameof(ComputeIsPrimaryButtonEnabled));
    }

    public SpecialDiscount GetModel()
    {
        SpecialDiscount model = new()
        {
            SpecialDiscountId = SpecialDiscountId,
        };
        model.MetadataContent.StartDate = StartDate;
        model.MetadataContent.EndDate = EndDate;
        model.MetadataContent.InitialDiscount = InitialDiscount;
        model.MetadataContent.Discount = Discount;
        model.MetadataContent.QtyStart = QtyStart;
        model.MetadataContent.WhiteList = WhiteList;
        model.MetadataContent.SmallInterval = SmallInterval;
        model.MetadataContent.BigInterval = BigInterval;
        model.MetadataContent.IsStandardOrderScope = IsStandardOrderScope;
        model.MetadataContent.IsStockOrderScope = IsStockOrderScope;

        model.SerializeMetadata();
        return model;
    }

}
