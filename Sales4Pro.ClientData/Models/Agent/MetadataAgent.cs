namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class MetadataAgent
{
    public MetadataAgent()
    {
        AgentName = string.Empty;
        AgentNumber = string.Empty;
        IsDefault = false;
        Catalogs = new List<MetadataAgentCatalog>();
    }

    public string AgentName { get; set; }
    public string AgentNumber { get; set; }
    public bool IsDefault { get; set; }

    public List<MetadataAgentCatalog> Catalogs { get; set; }

    public override string ToString()
    {
        return string.Format("{0} ({1})", AgentName, AgentNumber);
    }
}
