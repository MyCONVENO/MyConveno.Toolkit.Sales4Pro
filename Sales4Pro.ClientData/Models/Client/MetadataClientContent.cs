namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public class MetadataClientContent : IMetadataClientContent
{
    public MetadataClientContent()
    {
        App = new ClientMetadataApp();
        Reports = new ClientMetadataReports();
    }

    public ClientMetadataApp App { get; set; }
    public ClientMetadataReports Reports { get; set; }

    public class ClientMetadataApp
    {
        public string DefaultSeason { get; set; } = string.Empty;
        public string SupportPersonName { get; set; } = string.Empty;
        public string SupportPersonEmail { get; set; } = string.Empty;
        public string SupportPersonPhone { get; set; } = string.Empty;
        public string ImagePathString { get; set; } = string.Empty;
        
        public string AccentColorString { get; set; } = string.Empty;
        public string AccentColorLight1String { get; set; } = string.Empty;
        public string AccentColorLight2String { get; set; } = string.Empty;
        public string AccentColorLight3String { get; set; } = string.Empty;
        public string AccentColorDark1String { get; set; } = string.Empty;
        public string AccentColorDark2String { get; set; } = string.Empty;
        public string AccentColorDark3String { get; set; } = string.Empty;
    }

    public class ClientMetadataReports
    {
        public string AddressName1 { get; set; } = string.Empty;
        public string AddressName2 { get; set; } = string.Empty;
        public string AddressStreet { get; set; } = string.Empty;
        public string AddressZip { get; set; } = string.Empty;
        public string AddressCity { get; set; } = string.Empty;
        public string AddressCountryName { get; set; } = string.Empty;
        public string AddressCountryCode { get; set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string AddressLine3 { get; set; } = string.Empty;
        public string AddressLine4 { get; set; } = string.Empty;
        public string AddressLine5 { get; set; } = string.Empty;

        public string FooterLeftHeader { get; set; } = string.Empty;
        public string FooterLeftLine1 { get; set; } = string.Empty;
        public string FooterLeftLine2 { get; set; } = string.Empty;
        public string FooterLeftLine3 { get; set; } = string.Empty;
        public string FooterLeftLine4 { get; set; } = string.Empty;
        public string FooterLeftLine5 { get; set; } = string.Empty;

        public string FooterCenterHeader { get; set; } = string.Empty;
        public string FooterCenterLine1 { get; set; } = string.Empty;
        public string FooterCenterLine2 { get; set; } = string.Empty;
        public string FooterCenterLine3 { get; set; } = string.Empty;
        public string FooterCenterLine4 { get; set; } = string.Empty;
        public string FooterCenterLine5 { get; set; } = string.Empty;

        public string FooterRightHeader { get; set; } = string.Empty;
        public string FooterRightLine1 { get; set; } = string.Empty;
        public string FooterRightLine2 { get; set; } = string.Empty;
        public string FooterRightLine3 { get; set; } = string.Empty;
        public string FooterRightLine4 { get; set; } = string.Empty;
        public string FooterRightLine5 { get; set; } = string.Empty;
        public string Addressline { get; set; } = string.Empty;

        public string AEBRemarkText1 { get; set; } = string.Empty;
        public string AEBRemarkText2 { get; set; } = string.Empty;
        public string AEBRemarkText3 { get; set; } = string.Empty;
        public string AEBRemarkText4 { get; set; } = string.Empty;
    }

}