using Microsoft.UI.Xaml.Data;
using System;

namespace Sales4Pro.WinUI.CustomControls.Converter;

public class DoubleToPercentDoubleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return System.Convert.ToDouble(value) * 100.00d;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return double.IsNaN((double)value)
            ? (parameter is object ? System.Convert.ToDouble(parameter) / 100.00d : 0)
            : System.Convert.ToDouble(value) / 100.00d;
    }

}




