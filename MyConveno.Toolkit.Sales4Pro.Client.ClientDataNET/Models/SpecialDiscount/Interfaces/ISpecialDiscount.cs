namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface ISpecialDiscount
{
    string SpecialDiscountId { get; set; }
    string Metadata { get; set; }
    MetadataSpecialDiscountContent MetadataContent { get; set; }

    void DeserializeMetadata();
    void SerializeMetadata();
}