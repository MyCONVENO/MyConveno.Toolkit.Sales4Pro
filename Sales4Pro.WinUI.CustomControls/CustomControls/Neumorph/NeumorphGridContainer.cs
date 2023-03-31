using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;

namespace Sales4Pro.WinUI.CustomControls
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public sealed class NeumorphGridContainer : ContentControl
    {
        public event EventHandler CloseMe;
        private Button closeButton;

        public NeumorphGridContainer()
        {
            this.DefaultStyleKey = typeof(NeumorphGridContainer);

            Loaded -= NeumorphGridPanel_Loaded;
            Loaded += NeumorphGridPanel_Loaded;
            Unloaded -= NeumorphGridPanel_Unloaded;
            Unloaded += NeumorphGridPanel_Unloaded;
        }

        private void NeumorphGridPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (closeButton is not null)
            {
                closeButton.Click -= CloseButton_Click;
                closeButton.Click += CloseButton_Click;
            }
        }

        private void NeumorphGridPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            if (closeButton is not null)
                closeButton.Click -= CloseButton_Click;
        }


        ~NeumorphGridContainer()
        { }

        protected override void OnApplyTemplate()
        {
            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            closeButton = (Button)GetTemplateChild("PART_CloseButton");

            base.OnApplyTemplate();
        }

        #region DependencyProperties

        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonVisibilityProperty =
            DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(NeumorphGridContainer), new PropertyMetadata(Visibility.Visible));

        public string HeaderLabel
        {
            get { return (string)GetValue(HeaderLabelProperty); }
            set { SetValue(HeaderLabelProperty, value); }
        }
        public static readonly DependencyProperty HeaderLabelProperty =
            DependencyProperty.Register("HeaderLabel", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("HeaderLabel"));

        public ImageSource HeaderImageSource
        {
            get { return (ImageSource)GetValue(HeaderImageSourceProperty); }
            set { SetValue(HeaderImageSourceProperty, value); }
        }
        public static readonly DependencyProperty HeaderImageSourceProperty =
            DependencyProperty.Register("HeaderImageSource", typeof(ImageSource), typeof(NeumorphGridContainer), new PropertyMetadata(null));

        #region NoContentPanel

        public string NoContentTitle
        {
            get { return (string)GetValue(NoContentTitleProperty); }
            set { SetValue(NoContentTitleProperty, value); }
        }
        public static readonly DependencyProperty NoContentTitleProperty =
            DependencyProperty.Register("NoContentTitle", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("NoContentTitle", OnNoContentTitleChanged));

        private static void OnNoContentTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NeumorphGridContainer target = (NeumorphGridContainer)d;
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
            DependencyProperty.Register("NoContentText", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("NoContentText"));


        public Visibility NoContentPanelVisibility
        {
            get { return (Visibility)GetValue(NoContentPanelVisibilityProperty); }
            set { SetValue(NoContentPanelVisibilityProperty, value); }
        }
        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoContentPanelVisibilityProperty =
            DependencyProperty.Register("NoContentPanelVisibility", typeof(Visibility), typeof(NeumorphGridContainer), new PropertyMetadata(Visibility.Visible));

        #endregion


        #region TopInfo


        public string TopInfoTitle
        {
            get { return (string)GetValue(TopInfoTitleProperty); }
            set { SetValue(TopInfoTitleProperty, value); }
        }
        public static readonly DependencyProperty TopInfoTitleProperty =
            DependencyProperty.Register("TopInfoTitle", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("Title"));


        public string TopInfoFilterByText
        {
            get { return (string)GetValue(TopInfoFilterByTextProperty); }
            set { SetValue(TopInfoFilterByTextProperty, value); }
        }
        public static readonly DependencyProperty TopInfoFilterByTextProperty =
            DependencyProperty.Register("TopInfoFilterByText", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("FilterByText"));


        public string TopInfoSortByText
        {
            get { return (string)GetValue(TopInfoSortByTextProperty); }
            set { SetValue(TopInfoSortByTextProperty, value); }
        }
        public static readonly DependencyProperty TopInfoSortByTextProperty =
            DependencyProperty.Register("TopInfoSortByText", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("A bis Z"));


        public string TopInfoSearchHitsCountText
        {
            get { return (string)GetValue(TopInfoSearchHitsCountTextProperty); }
            set { SetValue(TopInfoSearchHitsCountTextProperty, value); }
        }
        public static readonly DependencyProperty TopInfoSearchHitsCountTextProperty =
            DependencyProperty.Register("TopInfoSearchHitsCountText", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("50"));


        public string TopInfoTotalCountText
        {
            get { return (string)GetValue(TopInfoTotalCountTextProperty); }
            set { SetValue(TopInfoTotalCountTextProperty, value); }
        }
        public static readonly DependencyProperty TopInfoTotalCountTextProperty =
            DependencyProperty.Register("TopInfoTotalCountText", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("100"));


        public string TopInfoItemText
        {
            get { return (string)GetValue(TopInfoItemTextProperty); }
            set { SetValue(TopInfoItemTextProperty, value); }
        }
        public static readonly DependencyProperty TopInfoItemTextProperty =
            DependencyProperty.Register("TopInfoItemText", typeof(string), typeof(NeumorphGridContainer), new PropertyMetadata("Kunden"));


        public Visibility TopInfoTotalsVisibilty
        {
            get { return (Visibility)GetValue(TopInfoTotalsVisibiltyProperty); }
            set { SetValue(TopInfoTotalsVisibiltyProperty, value); }
        }
        public static readonly DependencyProperty TopInfoTotalsVisibiltyProperty =
            DependencyProperty.Register("TopInfoTotalsVisibilty", typeof(Visibility), typeof(NeumorphGridContainer), new PropertyMetadata(Visibility.Visible));


        public Visibility TopInfoSortingVisibilty
        {
            get { return (Visibility)GetValue(TopInfoSortingVisibiltyProperty); }
            set { SetValue(TopInfoSortingVisibiltyProperty, value); }
        }
        public static readonly DependencyProperty TopInfoSortingVisibiltyProperty =
            DependencyProperty.Register("TopInfoSortingVisibilty", typeof(Visibility), typeof(NeumorphGridContainer), new PropertyMetadata(Visibility.Visible));


        public Visibility TopInfoProgressBarVisibilty
        {
            get { return (Visibility)GetValue(TopInfoProgressBarVisibiltyProperty); }
            set { SetValue(TopInfoProgressBarVisibiltyProperty, value); }
        }
        public static readonly DependencyProperty TopInfoProgressBarVisibiltyProperty =
            DependencyProperty.Register("TopInfoProgressBarVisibilty", typeof(Visibility), typeof(NeumorphGridContainer), new PropertyMetadata(Visibility.Collapsed));


        #endregion

        #endregion

        #region EventHandler

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseMe?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }

}
