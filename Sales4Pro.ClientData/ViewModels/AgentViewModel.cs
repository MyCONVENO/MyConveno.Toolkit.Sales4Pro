using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class AgentViewModel : ObservableObject
{
    public AgentViewModel()
    {
        AgentNumber = string.Empty;

        DisplayName = string.Empty;
        Mobile = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
        ConfirmationEmail = string.Empty;
        DefaultPricelistNumber = string.Empty;
        Pricelists = new ObservableCollection<Pricelist>();
    }

    public AgentViewModel(Agent agent) : this()
    {
        PasteData(agent);
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
    [NotifyPropertyChangedFor(nameof(ComputeHasPricelists))]
    [NotifyPropertyChangedFor(nameof(ComputePricelistsCount))]
    public ObservableCollection<Pricelist> pricelists;

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

    public bool ComputeHasPricelists
    {
        get { return Pricelists.Count > 0; }
    }

    public int ComputePricelistsCount
    {
        get { return Pricelists.Count; }
    }

    public string ComputeNumberName
    {
        get { return string.Format("{0} ({1})", DisplayName, AgentNumber); }
    }

    #endregion

    public void PasteData(Agent agent)
    {
        if (agent == null)
        {
            AgentNumber = string.Empty;
            DisplayName = string.Empty;
            Mobile = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            ConfirmationEmail = string.Empty;
            DefaultPricelistNumber = string.Empty;
            Pricelists = new ObservableCollection<Pricelist>();
        }
        else
        {
            AgentNumber = agent.AgentNumber;

            agent.DeserializeMetadata();

            DisplayName = agent.MetadataContent.DisplayName;
            Mobile = agent.MetadataContent.Mobile;
            Phone = agent.MetadataContent.Phone;
            Email = agent.MetadataContent.Email;
            ConfirmationEmail = agent.MetadataContent.ConfirmationEmail;
            DefaultPricelistNumber = agent.MetadataContent.DefaultPricelistNumber;
            Pricelists = agent.MetadataContent.Pricelists;
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
                Mobile = Mobile,
                Phone = Phone,
                Email = Email,
                ConfirmationEmail = ConfirmationEmail,
                DefaultPricelistNumber = DefaultPricelistNumber,
                Pricelists = Pricelists
            }
        };

        model.SerializeMetadata();
        return model;
    }

}
