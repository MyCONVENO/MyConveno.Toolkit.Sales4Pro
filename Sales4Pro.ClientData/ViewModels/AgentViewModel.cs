using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class AgentViewModel : ObservableObject
{
    public override string ToString()
    {
        return AgentNumber;
    }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string agentNumber = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public MetadataAgentContent metadataAgentContent = new();

    #endregion

    #region Computed Properties

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(AgentNumber) ||
                string.IsNullOrEmpty(MetadataAgentContent.Displayname) ||
                string.IsNullOrEmpty(MetadataAgentContent.Street) ||
                string.IsNullOrEmpty(MetadataAgentContent.ZIP) ||
                string.IsNullOrEmpty(MetadataAgentContent.City) ||
                string.IsNullOrEmpty(MetadataAgentContent.Country) ||
                string.IsNullOrEmpty(MetadataAgentContent.Email) ||
                string.IsNullOrEmpty(MetadataAgentContent.DefaultPricelistNumber))
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
         
            MetadataAgentContent = new();
        }
        else
        {
            AgentNumber = agent.AgentNumber;
            Agent tempAgent = new() { Metadata = agent.Metadata };
            tempAgent.DeserializeMetadata();
            MetadataAgentContent = tempAgent.MetadataContent;
        }
        OnPropertyChanged(nameof(ComputeIsPrimaryButtonEnabled));
    }

    public Agent GetModel()
    {
        Agent model = new()
        {
            AgentNumber = AgentNumber,
            MetadataContent = MetadataAgentContent
        };
        model.SerializeMetadata();
        return model;
    }

}
