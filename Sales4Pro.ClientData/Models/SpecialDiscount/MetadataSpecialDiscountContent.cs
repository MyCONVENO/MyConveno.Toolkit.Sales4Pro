﻿namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class MetadataSpecialDiscountContent : IMetadataSpecialDiscountContent
{
    public MetadataSpecialDiscountContent()
    {
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(365);
        InitialDiscount = 0.0d;
        Discount = 0.0d;
        QtyStart = 0;
        WhiteList = string.Empty;
        SmallInterval = 0.1d;
        BigInterval = 0.1d;
    }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double InitialDiscount { get; set; }
    public double Discount { get; set; }
    public int QtyStart { get; set; }
    public string WhiteList { get; set; }
    public double SmallInterval { get; set; }
    public double BigInterval { get; set; }
}