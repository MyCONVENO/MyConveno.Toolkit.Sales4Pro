using Microsoft.UI.Xaml.Data;
using System;

namespace Sales4Pro.WinUI.CustomControls.Converter;

public class NullBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string languag)
    {
        if (value is not null)
        {
            return value;
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string languag)
    {
        if (value is not null)
        {
            return value;
        }
        return null;
    }
}
