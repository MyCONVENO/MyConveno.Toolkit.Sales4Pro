using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class ClientViewModel : ObservableObject
{
    public override string ToString()
    {
        return ClientName;
    }

    #region Observable Properties

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string clientId = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string clientName = string.Empty;

    //[ObservableProperty]
    //public MetadataClientContent clientContent = new();



    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string defaultSeason = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string supportPersonName = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string supportPersonEmail = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string supportPersonPhone = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string imagePathString = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string accentColorString = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string accentColorLight1String = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string accentColorLight2String = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string accentColorLight3String = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string accentColorDark1String = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string accentColorDark2String = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAppPrimaryButtonEnabled))]
    public string accentColorDark3String = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressName1 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressName2 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressStreet = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeZIPCity))]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressZip = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeZIPCity))]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressCity = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeCountryCodeAndName))]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressCountryCode = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeCountryCodeAndName))]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressCountryName = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressline = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressLine1 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressLine2 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressLine3 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressLine4 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsMainDataPrimaryButtonEnabled))]
    public string addressLine5 = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerLeftHeader = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerLeftLine1 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerLeftLine2 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerLeftLine3 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerLeftLine4 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerLeftLine5 = string.Empty;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerCenterHeader = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerCenterLine1 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerCenterLine2 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerCenterLine3 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerCenterLine4 = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsReportsPrimaryButtonEnabled))]
    public string footerCenterLine5 = string.Empty;


    [ObservableProperty]
    public string footerRightHeader = string.Empty;

    [ObservableProperty]
    public string footerRightLine1 = string.Empty;

    [ObservableProperty]
    public string footerRightLine2 = string.Empty;

    [ObservableProperty]
    public string footerRightLine3 = string.Empty;

    [ObservableProperty]
    public string footerRightLine4 = string.Empty;

    [ObservableProperty]
    public string footerRightLine5 = string.Empty;


    [ObservableProperty]
    public string aEBRemarkText1 = string.Empty;

    [ObservableProperty]
    public string aEBRemarkText2 = string.Empty;

    [ObservableProperty]
    public string aEBRemarkText3 = string.Empty;

    [ObservableProperty]
    public string aEBRemarkText4 = string.Empty;

    #endregion

    #region Computed Properties

    public bool ComputeIsMainDataPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(ClientId) ||
                string.IsNullOrEmpty(ClientName) ||
                string.IsNullOrEmpty(AddressName1) ||
                string.IsNullOrEmpty(AddressStreet) ||
                string.IsNullOrEmpty(AddressCity) ||
                string.IsNullOrEmpty(AddressCountryCode) ||
                string.IsNullOrEmpty(AddressCountryName))
                return false;
            else
                return true;
        }
    }

    public bool ComputeIsAppPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(ClientId) ||
                string.IsNullOrEmpty(ClientName))
                return false;
            else
                return true;
        }
    }

    public bool ComputeIsReportsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(ClientId) ||
                string.IsNullOrEmpty(ClientName))
                return false;
            else
                return true;
        }
    }

    public string ComputeZIPCity
    {
        get
        {
            return string.Format("{0} {1}", AddressZip, AddressCity);
        }
    }

    public string ComputeCountryCodeAndName
    {
        get
        {
            return string.Format("{0} {1}", AddressCountryCode, AddressCountryName);
        }
    }

    #endregion

    public void PasteData(Client client)
    {
        if (client == null)
        {
            ClientId = string.Empty;
            ClientName = string.Empty;

            DefaultSeason = string.Empty;
            SupportPersonName = string.Empty;
            SupportPersonEmail = string.Empty;
            SupportPersonPhone = string.Empty;
            ImagePathString = string.Empty;
            AccentColorString = string.Empty;
            AccentColorLight1String = string.Empty;
            AccentColorLight2String = string.Empty;
            AccentColorLight3String = string.Empty;
            AccentColorDark1String = string.Empty;
            AccentColorDark2String = string.Empty;
            AccentColorDark3String = string.Empty;

            AddressName1 = string.Empty;
            AddressName2 = string.Empty;
            AddressStreet = string.Empty;
            AddressZip = string.Empty;
            AddressCity = string.Empty;
            AddressCountryName = string.Empty;
            AddressCountryCode = string.Empty;
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            AddressLine3 = string.Empty;
            AddressLine4 = string.Empty;
            AddressLine5 = string.Empty;

            FooterLeftHeader = string.Empty;
            FooterLeftLine1 = string.Empty;
            FooterLeftLine2 = string.Empty;
            FooterLeftLine3 = string.Empty;
            FooterLeftLine4 = string.Empty;
            FooterLeftLine5 = string.Empty;

            FooterCenterHeader = string.Empty;
            FooterCenterLine1 = string.Empty;
            FooterCenterLine2 = string.Empty;
            FooterCenterLine3 = string.Empty;
            FooterCenterLine4 = string.Empty;
            FooterCenterLine5 = string.Empty;

            FooterRightHeader = string.Empty;
            FooterRightLine1 = string.Empty;
            FooterRightLine2 = string.Empty;
            FooterRightLine3 = string.Empty;
            FooterRightLine4 = string.Empty;
            FooterRightLine5 = string.Empty;

            Addressline = string.Empty;
            AEBRemarkText1 = string.Empty;
            AEBRemarkText2 = string.Empty;
            AEBRemarkText3 = string.Empty;
            AEBRemarkText4 = string.Empty;
        }
        else
        {
            ClientId = client.ClientId;
            ClientName = client.ClientName;

            client.DeserializeMetadata();

            DefaultSeason = client.MetadataContent.App.DefaultSeason;
            SupportPersonName = client.MetadataContent.App.SupportPersonName;
            SupportPersonEmail = client.MetadataContent.App.SupportPersonEmail;
            SupportPersonPhone = client.MetadataContent.App.SupportPersonPhone;
            ImagePathString = client.MetadataContent.App.ImagePathString;
            AccentColorString = client.MetadataContent.App.AccentColorString;
            AccentColorLight1String = client.MetadataContent.App.AccentColorLight1String;
            AccentColorLight2String = client.MetadataContent.App.AccentColorLight2String;
            AccentColorLight3String = client.MetadataContent.App.AccentColorLight3String;
            AccentColorDark1String = client.MetadataContent.App.AccentColorDark1String;
            AccentColorDark2String = client.MetadataContent.App.AccentColorDark2String;
            AccentColorDark3String = client.MetadataContent.App.AccentColorDark3String;

            AddressName1 = client.MetadataContent.Reports.AddressName1;
            AddressName2 = client.MetadataContent.Reports.AddressName2;
            AddressStreet = client.MetadataContent.Reports.AddressStreet;
            AddressZip = client.MetadataContent.Reports.AddressZip;
            AddressCity = client.MetadataContent.Reports.AddressCity;
            AddressCountryName = client.MetadataContent.Reports.AddressCountryName;
            AddressCountryCode = client.MetadataContent.Reports.AddressCountryCode;
            AddressLine1 = client.MetadataContent.Reports.AddressLine1;
            AddressLine2 = client.MetadataContent.Reports.AddressLine2;
            AddressLine3 = client.MetadataContent.Reports.AddressLine3;
            AddressLine4 = client.MetadataContent.Reports.AddressLine4;
            AddressLine5 = client.MetadataContent.Reports.AddressLine5;

            FooterLeftHeader = client.MetadataContent.Reports.FooterLeftHeader;
            FooterLeftLine1 = client.MetadataContent.Reports.FooterLeftLine1;
            FooterLeftLine2 = client.MetadataContent.Reports.FooterLeftLine2;
            FooterLeftLine3 = client.MetadataContent.Reports.FooterLeftLine3;
            FooterLeftLine4 = client.MetadataContent.Reports.FooterLeftLine4;
            FooterLeftLine5 = client.MetadataContent.Reports.FooterLeftLine5;

            FooterCenterHeader = client.MetadataContent.Reports.FooterCenterHeader;
            FooterCenterLine1 = client.MetadataContent.Reports.FooterCenterLine1;
            FooterCenterLine2 = client.MetadataContent.Reports.FooterCenterLine2;
            FooterCenterLine3 = client.MetadataContent.Reports.FooterCenterLine3;
            FooterCenterLine4 = client.MetadataContent.Reports.FooterCenterLine4;
            FooterCenterLine5 = client.MetadataContent.Reports.FooterCenterLine5;

            FooterRightHeader = client.MetadataContent.Reports.FooterRightHeader;
            FooterRightLine1 = client.MetadataContent.Reports.FooterRightLine1;
            FooterRightLine2 = client.MetadataContent.Reports.FooterRightLine2;
            FooterRightLine3 = client.MetadataContent.Reports.FooterRightLine3;
            FooterRightLine4 = client.MetadataContent.Reports.FooterRightLine4;
            FooterRightLine5 = client.MetadataContent.Reports.FooterRightLine5;

            Addressline = client.MetadataContent.Reports.Addressline;
            AEBRemarkText1 = client.MetadataContent.Reports.AEBRemarkText1;
            AEBRemarkText2 = client.MetadataContent.Reports.AEBRemarkText2;
            AEBRemarkText3 = client.MetadataContent.Reports.AEBRemarkText3;
            AEBRemarkText4 = client.MetadataContent.Reports.AEBRemarkText4;
        }
    }

    public Client GetModel()
    {
        Client model = new()
        {
            ClientId = ClientId,
            ClientName = ClientName,
            MetadataContent = new()
            {
                App = new()
                {
                    DefaultSeason = DefaultSeason,
                    SupportPersonName = SupportPersonName,
                    SupportPersonEmail = SupportPersonEmail,
                    SupportPersonPhone = SupportPersonPhone,
                    ImagePathString = ImagePathString,
                    AccentColorString = AccentColorString,
                    AccentColorLight1String = AccentColorLight1String,
                    AccentColorLight2String = AccentColorLight2String,
                    AccentColorLight3String = AccentColorLight3String,
                    AccentColorDark1String = AccentColorDark1String,
                    AccentColorDark2String = AccentColorDark2String,
                    AccentColorDark3String = AccentColorDark3String,
                },
                Reports = new()
                {
                    AddressName1 = AddressName1,
                    AddressName2 = AddressName2,
                    AddressStreet = AddressStreet,
                    AddressZip = AddressZip,
                    AddressCity = AddressCity,
                    AddressCountryName = AddressCountryName,
                    AddressCountryCode = AddressCountryCode,
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    AddressLine3 = AddressLine3,
                    AddressLine4 = AddressLine4,
                    AddressLine5 = AddressLine5,
                    FooterLeftHeader = FooterLeftHeader,
                    FooterLeftLine1 = FooterLeftLine1,
                    FooterLeftLine2 = FooterLeftLine2,
                    FooterLeftLine3 = FooterLeftLine3,
                    FooterLeftLine4 = FooterLeftLine4,
                    FooterLeftLine5 = FooterLeftLine5,
                    FooterCenterHeader = FooterCenterHeader,
                    FooterCenterLine1 = FooterCenterLine1,
                    FooterCenterLine2 = FooterCenterLine2,
                    FooterCenterLine3 = FooterCenterLine3,
                    FooterCenterLine4 = FooterCenterLine4,
                    FooterCenterLine5 = FooterCenterLine5,
                    FooterRightHeader = FooterRightHeader,
                    FooterRightLine1 = FooterRightLine1,
                    FooterRightLine2 = FooterRightLine2,
                    FooterRightLine3 = FooterRightLine3,
                    FooterRightLine4 = FooterRightLine4,
                    FooterRightLine5 = FooterRightLine5,
                    Addressline = Addressline,
                    AEBRemarkText1 = AEBRemarkText1,
                    AEBRemarkText2 = AEBRemarkText2,
                    AEBRemarkText3 = AEBRemarkText3,
                    AEBRemarkText4 = AEBRemarkText4
                }
            }
        };
        model.SerializeMetadata();
        return model;
    }

}
