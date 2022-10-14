namespace MyConveno.Toolkit.Sales4Pro.Common.ClientData.Interfaces;

public interface IClientMetadataReports
{
    bool IsOrderListReportEnabled { get; set; }
    bool IsSeasonCourseReportEnabled { get; set; }
    bool IsSeasonValueReportEnabled { get; set; }
    bool IsYearValuesReportEnabled { get; set; }
}