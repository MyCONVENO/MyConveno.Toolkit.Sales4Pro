using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

namespace Sales4Pro.WinUI.CustomControls.Converter;

public class StockQuantityToForegroundBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value.GetType() != typeof(int))
            return new SolidColorBrush(Colors.Black);

        if ((int)value <= 0)
            return new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
        else if ((int)value > 0 && (int)value <= 10)
            return new SolidColorBrush(Colors.Black);
        else
            return new SolidColorBrush(Colors.Black);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

}
