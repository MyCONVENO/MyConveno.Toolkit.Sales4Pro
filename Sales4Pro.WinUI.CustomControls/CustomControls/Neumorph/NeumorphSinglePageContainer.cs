using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;

namespace Sales4Pro.WinUI.CustomControls
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public sealed class NeumorphSinglePageContainer : ContentControl
    {
        public event EventHandler CloseClicked;
        private Button closeButton;

        public NeumorphSinglePageContainer()
        {
            this.DefaultStyleKey = typeof(NeumorphSinglePageContainer);

            Loaded -= NeumorphHeaderedPanel_Loaded;
            Loaded += NeumorphHeaderedPanel_Loaded;
            Unloaded -= NeumorphHeaderedPanel_Unloaded;
            Unloaded += NeumorphHeaderedPanel_Unloaded;
        }

        private void NeumorphHeaderedPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (closeButton is not null)
            {
                closeButton.Click -= CloseButton_Click;
                closeButton.Click += CloseButton_Click;
            }
        }

        private void NeumorphHeaderedPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            if (closeButton is not null)
                closeButton.Click -= CloseButton_Click;
        }


        ~NeumorphSinglePageContainer()
        { }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            closeButton = (Button)GetTemplateChild("PART_CloseButton");

        }

        #region DependencyProperties

        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonVisibilityProperty =
            DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(NeumorphSinglePageContainer), new PropertyMetadata(Visibility.Visible));

        public string HeaderLabel
        {
            get { return (string)GetValue(HeaderLabelProperty); }
            set { SetValue(HeaderLabelProperty, value); }
        }
        public static readonly DependencyProperty HeaderLabelProperty =
            DependencyProperty.Register("HeaderLabel", typeof(string), typeof(NeumorphSinglePageContainer), new PropertyMetadata("HeaderLabel"));

        public ImageSource HeaderImageSource
        {
            get { return (ImageSource)GetValue(HeaderImageSourceProperty); }
            set { SetValue(HeaderImageSourceProperty, value); }
        }
        public static readonly DependencyProperty HeaderImageSourceProperty =
            DependencyProperty.Register("HeaderImageSource", typeof(ImageSource), typeof(NeumorphSinglePageContainer), new PropertyMetadata(null));


        public string NoContentTitle
        {
            get { return (string)GetValue(NoContentTitleProperty); }
            set { SetValue(NoContentTitleProperty, value); }
        }
        public static readonly DependencyProperty NoContentTitleProperty =
            DependencyProperty.Register("NoContentTitle", typeof(string), typeof(NeumorphSinglePageContainer), new PropertyMetadata("NoContentTitle", OnNoContentTitleChanged));

        private static void OnNoContentTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NeumorphSinglePageContainer target = (NeumorphSinglePageContainer)d;
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
            DependencyProperty.Register("NoContentText", typeof(string), typeof(NeumorphSinglePageContainer), new PropertyMetadata("NoContentText"));


        public Visibility NoContentPanelVisibility
        {
            get { return (Visibility)GetValue(NoContentPanelVisibilityProperty); }
            set { SetValue(NoContentPanelVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoContentPanelVisibilityProperty =
            DependencyProperty.Register("NoContentPanelVisibility", typeof(Visibility), typeof(NeumorphSinglePageContainer), new PropertyMetadata(Visibility.Visible));


        #endregion

        #region EventHandler

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }

}
