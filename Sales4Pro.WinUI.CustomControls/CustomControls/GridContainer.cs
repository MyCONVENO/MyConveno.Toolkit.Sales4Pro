using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_TopInfo", Type = typeof(Grid))]
public sealed class GridContainer : ContentControl
{
    public GridContainer()
    {
        this.DefaultStyleKey = typeof(GridContainer);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    #region DependencyProperties

    public ImageSource HeaderImageSource
    {
        get { return (ImageSource)GetValue(HeaderImageSourceProperty); }
        set { SetValue(HeaderImageSourceProperty, value); }
    }
    public static readonly DependencyProperty HeaderImageSourceProperty =
        DependencyProperty.Register("HeaderImageSource", typeof(ImageSource), typeof(GridContainer), new PropertyMetadata(null));


    public bool IsBusy
    {
        get { return (bool)GetValue(IsBusyProperty); }
        set { SetValue(IsBusyProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsSpinning.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsBusyProperty =
        DependencyProperty.Register("IsBusy", typeof(bool), typeof(GridContainer), new PropertyMetadata(false));
      

    #region NoContentPanel

    public string NoContentTitle
    {
        get { return (string)GetValue(NoContentTitleProperty); }
        set { SetValue(NoContentTitleProperty, value); }
    }
    public static readonly DependencyProperty NoContentTitleProperty =
        DependencyProperty.Register("NoContentTitle", typeof(string), typeof(GridContainer), new PropertyMetadata("NoContentTitle", OnNoContentTitleChanged));

    private static void OnNoContentTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        GridContainer target = (GridContainer)d;
        //if (Windows.ApplicationModel.DesignMode.DesignModeEnabled == false)

        if (target is not null)
        {
            //if (string.IsNullOrWhiteSpace(e.NewValue.ToString()))
            //    target.NoContentTitleTextBlock.Visibility = Visibility.Collapsed;
            //else
            //    target.NoContentTitleTextBlock.Visibility = Visibility.Visible;
        }
    }


    public string NoContentText
    {
        get { return (string)GetValue(NoContentTextProperty); }
        set { SetValue(NoContentTextProperty, value); }
    }
    public static readonly DependencyProperty NoContentTextProperty =
        DependencyProperty.Register("NoContentText", typeof(string), typeof(GridContainer), new PropertyMetadata("NoContentText"));


    public Visibility NoContentPanelVisibility
    {
        get { return (Visibility)GetValue(NoContentPanelVisibilityProperty); }
        set { SetValue(NoContentPanelVisibilityProperty, value); }
    }
    // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty NoContentPanelVisibilityProperty =
        DependencyProperty.Register("NoContentPanelVisibility", typeof(Visibility), typeof(GridContainer), new PropertyMetadata(Visibility.Visible));

    #endregion

    #endregion

}

