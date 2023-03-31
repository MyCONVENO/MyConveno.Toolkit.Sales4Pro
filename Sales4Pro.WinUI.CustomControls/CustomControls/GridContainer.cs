using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_TopInfo", Type = typeof(Grid))]
public sealed class GridContainer : ContentControl
{
    private Grid topInfo;
    public event RoutedEventHandler TopInfo_Tapped;

    public GridContainer()
    {
        this.DefaultStyleKey = typeof(GridContainer);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        topInfo = (Grid)GetTemplateChild("PART_TopInfo");

        if (topInfo is not null)
        {
            topInfo.Tapped += TopInfoTapped; ;
        }
    }

    private void TopInfoTapped(object sender, RoutedEventArgs e)
    {
        TopInfo_Tapped?.Invoke(sender, e);
    }

    #region DependencyProperties

    public ImageSource HeaderImageSource
    {
        get { return (ImageSource)GetValue(HeaderImageSourceProperty); }
        set { SetValue(HeaderImageSourceProperty, value); }
    }
    public static readonly DependencyProperty HeaderImageSourceProperty =
        DependencyProperty.Register("HeaderImageSource", typeof(ImageSource), typeof(GridContainer), new PropertyMetadata(null));


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

    #region TopInfo

    public string TopInfoTitle
    {
        get { return (string)GetValue(TopInfoTitleProperty); }
        set { SetValue(TopInfoTitleProperty, value); }
    }
    public static readonly DependencyProperty TopInfoTitleProperty =
        DependencyProperty.Register("TopInfoTitle", typeof(string), typeof(GridContainer), new PropertyMetadata("Title"));

    public string TopInfoFilterByText
    {
        get { return (string)GetValue(TopInfoFilterByTextProperty); }
        set { SetValue(TopInfoFilterByTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoFilterByTextProperty =
        DependencyProperty.Register("TopInfoFilterByText", typeof(string), typeof(GridContainer), new PropertyMetadata("FilterByText"));

    public string TopInfoSortByText
    {
        get { return (string)GetValue(TopInfoSortByTextProperty); }
        set { SetValue(TopInfoSortByTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoSortByTextProperty =
        DependencyProperty.Register("TopInfoSortByText", typeof(string), typeof(GridContainer), new PropertyMetadata("A bis Z"));

    public string TopInfoSearchHitsCountText
    {
        get { return (string)GetValue(TopInfoSearchHitsCountTextProperty); }
        set { SetValue(TopInfoSearchHitsCountTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoSearchHitsCountTextProperty =
        DependencyProperty.Register("TopInfoSearchHitsCountText", typeof(string), typeof(GridContainer), new PropertyMetadata("50"));

    public string TopInfoTotalCountText
    {
        get { return (string)GetValue(TopInfoTotalCountTextProperty); }
        set { SetValue(TopInfoTotalCountTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoTotalCountTextProperty =
        DependencyProperty.Register("TopInfoTotalCountText", typeof(string), typeof(GridContainer), new PropertyMetadata("100"));

    public string TopInfoItemText
    {
        get { return (string)GetValue(TopInfoItemTextProperty); }
        set { SetValue(TopInfoItemTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoItemTextProperty =
        DependencyProperty.Register("TopInfoItemText", typeof(string), typeof(GridContainer), new PropertyMetadata("Kunden"));

    public Visibility TopInfoTotalsVisibilty
    {
        get { return (Visibility)GetValue(TopInfoTotalsVisibiltyProperty); }
        set { SetValue(TopInfoTotalsVisibiltyProperty, value); }
    }
    public static readonly DependencyProperty TopInfoTotalsVisibiltyProperty =
        DependencyProperty.Register("TopInfoTotalsVisibilty", typeof(Visibility), typeof(GridContainer), new PropertyMetadata(Visibility.Visible));

    public Visibility TopInfoSortingVisibilty
    {
        get { return (Visibility)GetValue(TopInfoSortingVisibiltyProperty); }
        set { SetValue(TopInfoSortingVisibiltyProperty, value); }
    }
    public static readonly DependencyProperty TopInfoSortingVisibiltyProperty =
        DependencyProperty.Register("TopInfoSortingVisibilty", typeof(Visibility), typeof(GridContainer), new PropertyMetadata(Visibility.Visible));

    #endregion

    #endregion

}

