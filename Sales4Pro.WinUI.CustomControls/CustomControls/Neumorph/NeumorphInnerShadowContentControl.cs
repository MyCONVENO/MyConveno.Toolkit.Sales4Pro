using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class NeumorphInnerShadowContentControl : ContentControl
{
    public NeumorphInnerShadowContentControl()
    {
        DefaultStyleKey = typeof(NeumorphInnerShadowContentControl);
    }

    //public string Title
    //{
    //    get { return (string)GetValue(TitleProperty); }
    //    set { SetValue(TitleProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty TitleProperty =
    //    DependencyProperty.Register("Title", typeof(string), typeof(PageEntryContentControl), new PropertyMetadata("Titletext"));


    //public double CaptionMinWidth
    //{
    //    get { return (double)GetValue(CaptionMinWidthProperty); }
    //    set { SetValue(CaptionMinWidthProperty, value); }
    //}

    //// Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty CaptionMinWidthProperty =
    //    DependencyProperty.Register("CaptionMinWidth", typeof(double), typeof(PageEntryContentControl), new PropertyMetadata(140.0));

}
