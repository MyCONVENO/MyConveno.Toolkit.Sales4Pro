using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_TopInfo", Type = typeof(Grid))]
public sealed class FilterHeaderLine : Control
{
    private Grid topInfo;
    public event RoutedEventHandler TopInfo_Tapped;

    public FilterHeaderLine()
    {
        this.DefaultStyleKey = typeof(FilterHeaderLine);
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
       
    #region TopInfo

    public string TopInfoTitle
    {
        get { return (string)GetValue(TopInfoTitleProperty); }
        set { SetValue(TopInfoTitleProperty, value); }
    }
    public static readonly DependencyProperty TopInfoTitleProperty =
        DependencyProperty.Register("TopInfoTitle", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("Title"));

    public string TopInfoFilterByText
    {
        get { return (string)GetValue(TopInfoFilterByTextProperty); }
        set { SetValue(TopInfoFilterByTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoFilterByTextProperty =
        DependencyProperty.Register("TopInfoFilterByText", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("FilterByText"));

    public string TopInfoSortByText
    {
        get { return (string)GetValue(TopInfoSortByTextProperty); }
        set { SetValue(TopInfoSortByTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoSortByTextProperty =
        DependencyProperty.Register("TopInfoSortByText", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("A bis Z"));

    public string TopInfoSearchHitsCountText
    {
        get { return (string)GetValue(TopInfoSearchHitsCountTextProperty); }
        set { SetValue(TopInfoSearchHitsCountTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoSearchHitsCountTextProperty =
        DependencyProperty.Register("TopInfoSearchHitsCountText", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("50"));

    public string TopInfoTotalCountText
    {
        get { return (string)GetValue(TopInfoTotalCountTextProperty); }
        set { SetValue(TopInfoTotalCountTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoTotalCountTextProperty =
        DependencyProperty.Register("TopInfoTotalCountText", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("100"));

    public string TopInfoItemText
    {
        get { return (string)GetValue(TopInfoItemTextProperty); }
        set { SetValue(TopInfoItemTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoItemTextProperty =
        DependencyProperty.Register("TopInfoItemText", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("Kunden"));

    public Visibility TopInfoTotalsVisibilty
    {
        get { return (Visibility)GetValue(TopInfoTotalsVisibiltyProperty); }
        set { SetValue(TopInfoTotalsVisibiltyProperty, value); }
    }
    public static readonly DependencyProperty TopInfoTotalsVisibiltyProperty =
        DependencyProperty.Register("TopInfoTotalsVisibilty", typeof(Visibility), typeof(FilterHeaderLine), new PropertyMetadata(Visibility.Visible));

    public Visibility TopInfoSortingVisibilty
    {
        get { return (Visibility)GetValue(TopInfoSortingVisibiltyProperty); }
        set { SetValue(TopInfoSortingVisibiltyProperty, value); }
    }
    public static readonly DependencyProperty TopInfoSortingVisibiltyProperty =
        DependencyProperty.Register("TopInfoSortingVisibilty", typeof(Visibility), typeof(FilterHeaderLine), new PropertyMetadata(Visibility.Visible));

    public string TopInfoItemSortByText
    {
        get { return (string)GetValue(TopInfoItemSortByTextProperty); }
        set { SetValue(TopInfoItemSortByTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoItemSortByTextProperty =
        DependencyProperty.Register("TopInfoItemSortByText", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("Sortieren nach"));

    public string TopInfoItemHitsAtText
    {
        get { return (string)GetValue(TopInfoItemHitsAtTextProperty); }
        set { SetValue(TopInfoItemHitsAtTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoItemHitsAtTextProperty =
        DependencyProperty.Register("TopInfoItemHitsAtText", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("Treffer bei"));

    public string TopInfoItemFilterByText
    {
        get { return (string)GetValue(TopInfoItemFilterByTextProperty); }
        set { SetValue(TopInfoItemFilterByTextProperty, value); }
    }
    public static readonly DependencyProperty TopInfoItemFilterByTextProperty =
        DependencyProperty.Register("TopInfoItemFilterByText", typeof(string), typeof(FilterHeaderLine), new PropertyMetadata("Gefiltert nach"));

    #endregion

    #endregion

}

