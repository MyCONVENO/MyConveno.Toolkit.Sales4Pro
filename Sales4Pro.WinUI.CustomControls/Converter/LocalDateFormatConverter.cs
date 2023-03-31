using Microsoft.UI.Xaml.Data;
using System;
using Windows.Globalization;
using Windows.Globalization.DateTimeFormatting;

namespace Sales4Pro.WinUI.CustomControls.Converter;

public class LocalDateFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        //Windows.Globalization.NumberFormatting.DecimalFormatter deciamlFormatter = new DecimalFormatter(new string[] { "PL" }, "PL");
        //double d1 = (double)deciamlFormatter.ParseDouble("2,5"); // ParseDouble returns double?, not 

        DateTime dt = (DateTime)value;
        DateTimeFormatter dtf = new DateTimeFormatter("longdate", new[] { "de-DE" }, "DE", CalendarIdentifiers.Gregorian, ClockIdentifiers.TwentyFourHour);
        string longDate = dtf.Format(dt);
        return longDate;

        //return String.Format("{0:D}", value);  
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
