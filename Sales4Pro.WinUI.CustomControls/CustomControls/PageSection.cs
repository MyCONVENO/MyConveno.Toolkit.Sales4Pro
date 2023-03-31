using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class PageSection : ContentControl
{
    public PageSection()
    {
        this.DefaultStyleKey = typeof(PageSection);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // ----- Exit initialisation here in DesignMode  ------------------------
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        if (ClipTopArea)
        {
            BorderThickness = new Thickness(0);
            Padding = new Thickness(0, 0, 0, 6);
        }
        else
        {
            BorderThickness = new Thickness(0, 1, 0, 0);
            Padding = new Thickness(0, 6, 0, 6);
        }
    }

    public bool ClipTopArea
    {
        get { return (bool)GetValue(ClipTopAreaProperty); }
        set { SetValue(ClipTopAreaProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BackButtonVisibility.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ClipTopAreaProperty =
        DependencyProperty.Register("ClipTopArea", typeof(bool), typeof(PageSection), new PropertyMetadata(false, OnClipTopAreaChanged));

    private static void OnClipTopAreaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PageSection target = (PageSection)d;
        if (target is not null)
        {
            if (target.ClipTopArea)
            {
                target.BorderThickness = new Thickness(0);
                target.Padding = new Thickness(0, 0, 0, 6);
            }
            else
            {
                target.BorderThickness = new Thickness(0, 1, 0, 0);
                target.Padding = new Thickness(0, 6, 0, 6);
            }
        }
    }

}
