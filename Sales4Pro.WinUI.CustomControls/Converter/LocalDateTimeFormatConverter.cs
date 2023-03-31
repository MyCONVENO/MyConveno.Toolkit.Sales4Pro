using Microsoft.UI.Xaml.Data;
using System;

namespace Sales4Pro.WinUI.CustomControls.Converter;

public class LocalDateTimeFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return String.Format("{0:f}", value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
