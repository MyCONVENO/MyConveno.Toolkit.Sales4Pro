using Microsoft.UI;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;


namespace Sales4Pro.WinUI.CustomControls.Converter;

public class ColorContrastConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        SolidColorBrush brush = (SolidColorBrush)value;
        int yiq = ((brush.Color.R * 299) + (brush.Color.G * 587) + (brush.Color.B * 114)) / 1000;
        Color contrastColor;
        bool invert = (parameter is not null) && System.Convert.ToBoolean(parameter);

        // check to see if we actually need to invert
        contrastColor = invert
                            ? ((yiq >= 128) ? Colors.White : Colors.Black)
                            : ((yiq >= 128) ? Colors.Black : Colors.White);

        return new SolidColorBrush(contrastColor);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        return value;
    }
}
