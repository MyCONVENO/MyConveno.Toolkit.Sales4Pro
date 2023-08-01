namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData
{
    public interface IMetadataSpecialDiscountContent
    {
        double BigInterval { get; set; }
        double Discount { get; set; }
        DateTime EndDate { get; set; }
        double InitialDiscount { get; set; }
        int QtyStart { get; set; }
        double SmallInterval { get; set; }
        DateTime StartDate { get; set; }
        string WhiteList { get; set; }
    }
}