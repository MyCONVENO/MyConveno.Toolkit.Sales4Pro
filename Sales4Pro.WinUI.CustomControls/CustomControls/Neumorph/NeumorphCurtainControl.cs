using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.Foundation;

namespace Sales4Pro.WinUI.CustomControls
{
    [TemplatePart(Name = "PART_BackButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public sealed class NeumorphCurtainControl : ContentControl
    {
        public event EventHandler BackClicked;
        public event EventHandler CloseClicked;
        private Button backButton;
        private Button closeButton;

        public NeumorphCurtainControl()
        {
            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            this.DefaultStyleKey = typeof(NeumorphCurtainControl);
            IsTabStop = false;
            Loaded += HeaderedCurtainContentControl_Loaded;
            Unloaded += HeaderedCurtainContentControl_Unloaded;

            SizeChanged -= NeumorphCurtainControl_SizeChanged;
            SizeChanged += NeumorphCurtainControl_SizeChanged;
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            backButton = (Button)GetTemplateChild("PART_BackButton");
            closeButton = (Button)GetTemplateChild("PART_CloseButton");

            if (backButton is not null)
            {
                backButton.Click -= BackButton_Click;
                backButton.Click += BackButton_Click;
            }

            if (backButton is not null)
            {
                closeButton.Click -= CloseButton_Click;
                closeButton.Click += CloseButton_Click;
            }
        }

        private void NeumorphCurtainControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Clip = new RectangleGeometry
            {
                Rect = new Rect(-32, 0, e.NewSize.Width + 64, e.NewSize.Height)
            };
        }

        private void HeaderedCurtainContentControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void HeaderedCurtainContentControl_Unloaded(object sender, RoutedEventArgs e)
        {
            if (backButton is not null)
                backButton.Click -= BackButton_Click;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackClicked?.Invoke(this, EventArgs.Empty);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeaderText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(NeumorphCurtainControl), new PropertyMetadata("Header"));


        public Visibility BackButtonVisibility
        {
            get { return (Visibility)GetValue(BackButtonVisibilityProperty); }
            set { SetValue(BackButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackButtonVisibilityProperty =
            DependencyProperty.Register("BackButtonVisibility", typeof(Visibility), typeof(NeumorphCurtainControl), new PropertyMetadata(Visibility.Visible));


        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonVisibilityProperty =
            DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(NeumorphCurtainControl), new PropertyMetadata(Visibility.Visible));


        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderHeightProperty =
            DependencyProperty.Register("HeaderHeight", typeof(double), typeof(NeumorphCurtainControl), new PropertyMetadata((double)40.0));

    }
}
