using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;


namespace Sales4Pro.WinUI.CustomControls.Converter;

public class StockQuantityToBackgroundBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value.GetType() != typeof(int))
            return new SolidColorBrush(Colors.LightGray);

        if ((int)value <= 0)
            return new SolidColorBrush(Color.FromArgb(128, 232, 0, 0)); // Rot
        else if ((int)value > 0 && (int)value <= 10)
            return new SolidColorBrush(Color.FromArgb(128, 232, 232, 0)); // Gelb
        else
            return new SolidColorBrush(Color.FromArgb(64, 0, 232, 0)); // Grün
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }

}
