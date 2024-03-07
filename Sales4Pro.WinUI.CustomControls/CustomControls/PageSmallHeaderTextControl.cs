using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class PageSmallHeaderTextControl : Control
{

    public PageSmallHeaderTextControl()
    {
        this.DefaultStyleKey = typeof(PageSmallHeaderTextControl);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(PageSmallHeaderTextControl), new PropertyMetadata("Title"));


    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }
    
    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(PageSmallHeaderTextControl), new PropertyMetadata("Text"));

}