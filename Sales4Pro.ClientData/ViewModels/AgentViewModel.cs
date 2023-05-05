﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class AgentViewModel : ObservableObject
{
    public AgentViewModel()
    {
        AgentNumber = string.Empty;
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
    public bool processOrders;

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
                string.IsNullOrEmpty(DisplayName))
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
            Pricelists = new ObservableCollection<Pricelist>();
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
            Pricelists = new ObservableCollection<Pricelist>();
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
                Pricelists = new ObservableCollection<Pricelist>()
    }
};

        model.SerializeMetadata();
        return model;
    }

}
