using Microsoft.UI.Xaml.Data;
using System;

namespace Sales4Pro.WinUI.CustomControls.Converter;

public class DoubleToCurrencyNumberConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        //if ((double)value == 0)
        //    return string.Empty;

        // http://www.csharp-examples.net/string-format-double/
        return String.Format("{0:0.00}", value);

    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

}
