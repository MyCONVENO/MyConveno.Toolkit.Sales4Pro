namespace MyConveno.Toolkit.Sales4Pro.Client.UserData
{
    public interface IAgent
    {
        string AgentNumber { get; set; }
        string AgentName { get; set; }
        string Metadata { get; set; }
        MetadataAgentContent MetadataContent { get; set; }

        void DeserializeMetadata();
        void SerializeMetadata();
    }
}