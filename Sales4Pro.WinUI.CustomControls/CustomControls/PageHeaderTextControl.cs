using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class PageHeaderTextControl : Control
{

    public PageHeaderTextControl()
    {
        this.DefaultStyleKey = typeof(PageHeaderTextControl);
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
        DependencyProperty.Register("Title", typeof(string), typeof(PageHeaderTextControl), new PropertyMetadata("Title"));

}