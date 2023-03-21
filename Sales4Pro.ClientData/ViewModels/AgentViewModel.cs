using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class AgentViewModel : ObservableObject
{
    public AgentViewModel()
    {
        AgentNumber = string.Empty;
        AgentContent = new MetadataAgentContent();
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
    public MetadataAgentContent agentContent;

    #endregion

    #region Computed Properties

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(AgentNumber) ||
                string.IsNullOrEmpty(AgentContent.Displayname) ||
                string.IsNullOrEmpty(AgentContent.Street) ||
                string.IsNullOrEmpty(AgentContent.ZIP) ||
                string.IsNullOrEmpty(AgentContent.City) ||
                string.IsNullOrEmpty(AgentContent.Country) ||
                string.IsNullOrEmpty(AgentContent.Email) ||
                string.IsNullOrEmpty(AgentContent.DefaultPricelistNumber))
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

            AgentContent = new();
        }
        else
        {
            AgentNumber = agent.AgentNumber;
            Agent tempAgent = new() { Metadata = agent.Metadata };
            tempAgent.DeserializeMetadata();
            AgentContent = tempAgent.MetadataContent;
        }
        OnPropertyChanged(nameof(ComputeIsPrimaryButtonEnabled));
    }

    public Agent GetModel()
    {
        Agent model = new()
        {
            AgentNumber = AgentNumber,
            MetadataContent = AgentContent
        };
        model.SerializeMetadata();
        return model;
    }

}
