using Microsoft.UI.Xaml.Data;
using System;

namespace Sales4Pro.WinUI.CustomControls.Converter;

public class StringFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (parameter is not null)
        {
            string formatparameter = parameter.ToString();

            if (string.IsNullOrEmpty(value.ToString()))
                return value.ToString();
            else
                return string.Format(formatparameter, value as string);
        }
        else
            return value.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
