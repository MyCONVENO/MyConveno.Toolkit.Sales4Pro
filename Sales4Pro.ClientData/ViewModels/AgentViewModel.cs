using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class AgentViewModel : ObservableObject
{
    public override string ToString()
    {
        return AgentName;
    }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string agentNumber = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string agentName = string.Empty;

    [ObservableProperty]
    public MetadataAgentContent metadataAgentContent = new();

    #endregion

    #region Computed Properties

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(AgentNumber) || string.IsNullOrEmpty(AgentName))
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
            AgentName = string.Empty;
            //Password = string.Empty;

            MetadataAgentContent = new();
        }
        else
        {
            AgentNumber = agent.AgentNumber;
            AgentName = agent.AgentName;
            //Password = user.Password;

            Agent tempAgent = new() { Metadata = agent.Metadata };
            tempAgent.DeserializeMetadata();
            MetadataAgentContent = tempAgent.MetadataContent;
        }
        //OnPropertyChanged(nameof(ComputeIsAdmin));
        //OnPropertyChanged(nameof(ComputeIsUser));
        //OnPropertyChanged(nameof(ComputeIsUserOrAdmin));
        //OnPropertyChanged(nameof(ComputeIsB2B));
        //OnPropertyChanged(nameof(ComputeDisplayname));
        //OnPropertyChanged(nameof(ComputeDisplaynameAndRole));
    }

    public Agent GetModel()
    {
        Agent model = new()
        {
            AgentNumber = agentNumber,
            AgentName = agentName,
            MetadataContent = MetadataAgentContent
        };
        model.SerializeMetadata();
        return model;
    }

}
