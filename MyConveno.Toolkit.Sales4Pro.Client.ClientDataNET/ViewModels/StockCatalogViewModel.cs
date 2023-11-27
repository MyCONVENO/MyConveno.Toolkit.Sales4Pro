using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class StockCatalogViewModel : ObservableObject
{
    public StockCatalogViewModel()
    {
        StockCatalogId = string.Empty;
        StockCatalogName = string.Empty;
    }

    public StockCatalogViewModel(StockCatalog association) : this()
    {
        PasteData(association);
    }

    public override string ToString()
    {
        return StockCatalogName;
    }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string stockCatalogId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string stockCatalogName;

    #endregion

    #region Computed Properties

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(StockCatalogId) ||
                string.IsNullOrEmpty(StockCatalogName))
                return false;
            else
                return true;
        }
    }

    #endregion

    public void PasteData(StockCatalog stockCatalog)
    {
        if (stockCatalog == null)
        {
            StockCatalogId =  string.Empty;
            StockCatalogName = string.Empty;
        }
        else
        {
            StockCatalogId = stockCatalog.StockCatalogId;
            StockCatalogName = stockCatalog.StockCatalogName;
        }
        OnPropertyChanged(nameof(ComputeIsPrimaryButtonEnabled));
    }

    public StockCatalog GetModel()
    {
        StockCatalog model = new()
        {
            StockCatalogId = StockCatalogId,
            StockCatalogName = StockCatalogName,
        };
        return model;
    }

}
