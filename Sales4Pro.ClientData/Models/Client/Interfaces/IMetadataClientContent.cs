namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IMetadataClientContent
{
    MetadataClientContent.ClientMetadataApp App { get; set; }
    MetadataClientContent.ClientMetadataReports Reports { get; set; }
}