using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class AgentViewModel : ObservableObject
{
    public AgentViewModel()
    {
        AgentNumber = string.Empty;
    }

    public override string ToString()
    {
        return AgentNumber;
    }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string agentNumber;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string displayName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string street;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string zIP;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string city;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string country;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string mobile;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string phone;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string email;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string confirmationEmail;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string defaultPricelistNumber;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public bool isPriceOnConfirmVisible;
  
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public bool processOrders;

    #endregion

    #region Computed Properties

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(AgentNumber) ||
                string.IsNullOrEmpty(DisplayName) ||
                string.IsNullOrEmpty(DefaultPricelistNumber))
                return false;
            else
                return true;
        }
    }

    #endregion

    public void PasteData(Agent agent)
    {
        if (agent == null)
        {
            AgentNumber = string.Empty;
            DisplayName = string.Empty;
            Street = string.Empty;
            ZIP = string.Empty;
            City = string.Empty;
            Country = string.Empty;
            Mobile = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            ConfirmationEmail = string.Empty;
            DefaultPricelistNumber = "-";
            IsPriceOnConfirmVisible = true;
        }
        else
        {
            AgentNumber = agent.AgentNumber;
            
            agent.DeserializeMetadata();
           
            DisplayName = agent.MetadataContent.DisplayName;
            Street = agent.MetadataContent.Street;
            ZIP = agent.MetadataContent.ZIP;
            City = agent.MetadataContent.City;
            Country = agent.MetadataContent.Country;
            Mobile = agent.MetadataContent.Mobile;
            Phone = agent.MetadataContent.Phone;
            Email = agent.MetadataContent.Email;
            ConfirmationEmail = agent.MetadataContent.ConfirmationEmail;
            DefaultPricelistNumber = agent.MetadataContent.DefaultPricelistNumber;
            IsPriceOnConfirmVisible = agent.MetadataContent.IsPriceOnConfirmVisible;
        }
        OnPropertyChanged(nameof(ComputeIsPrimaryButtonEnabled));
    }

    public Agent GetModel()
    {
        Agent model = new()
        {
            AgentNumber = AgentNumber,
            MetadataContent = new()
            {
                DisplayName = DisplayName,
                Street = Street,
                ZIP = ZIP,
                City = City,
                Country = Country,
                Mobile = Mobile,
                Phone = Phone,
                Email = Email,
                ConfirmationEmail = ConfirmationEmail,
                DefaultPricelistNumber = DefaultPricelistNumber,
                IsPriceOnConfirmVisible = IsPriceOnConfirmVisible
            }
        };

        model.SerializeMetadata();
        return model;
    }

}
