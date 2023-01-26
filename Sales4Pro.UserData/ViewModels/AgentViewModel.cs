using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.UserData;

public partial class AgentViewModel : ObservableObject
{
    public override string ToString()
    {
        return AgentName;
    }

    #region Observable Properties

    [ObservableProperty]
    public string agentNumber = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string agentName = string.Empty;

    //[ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    //public string password = string.Empty;

    [ObservableProperty]
    //[NotifyPropertyChangedFor(nameof(ComputeIsAdmin))]
    //[NotifyPropertyChangedFor(nameof(ComputeIsUser))]
    //[NotifyPropertyChangedFor(nameof(ComputeIsUserOrAdmin))]
    //[NotifyPropertyChangedFor(nameof(ComputeIsB2B))]
    public MetadataAgentContent metadataAgentContent = new();

    #endregion

    #region Computed Properties

    //public bool ComputeIsAdmin
    //{
    //    get { return MetadataAgentContent.Role.ToLower() == "admin"; }
    //}

    //public bool ComputeIsAgent
    //{
    //    get
    //    { return MetadataAgentContent.Role.ToLower() == "user"; }
    //}

    //public bool ComputeIsUserOrAdmin
    //{
    //    get
    //    {
    //        return MetadataUserContent.Role.ToLower() == "user" ||
    //               MetadataUserContent.Role.ToLower() == "admin";
    //    }
    //}

    //public bool ComputeIsB2B
    //{
    //    get { return MetadataUserContent.Role.ToLower() == "b2b"; }
    //}

    //public string ComputeDisplayname
    //{
    //    get { return MetadataUserContent.Displayname; }
    //}

    //public string ComputeDisplaynameAndRole
    //{
    //    get { return string.Format("{0} ({1})", MetadataUserContent.Displayname, MetadataUserContent.Role); }
    //}

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(AgentName))
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
