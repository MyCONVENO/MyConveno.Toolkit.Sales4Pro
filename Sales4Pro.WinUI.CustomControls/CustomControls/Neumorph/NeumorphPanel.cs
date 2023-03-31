using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Sales4Pro.WinUI.CustomControls
{
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    public sealed class NeumorphPanel : ContentControl
    {
        public event EventHandler CloseClicked;
        private Button closeButton;

        public NeumorphPanel()
        {
            this.DefaultStyleKey = typeof(NeumorphPanel);

            Loaded -= NeumorphPanel_Loaded;
            Loaded += NeumorphPanel_Loaded;

            Unloaded -= NeumorphPanel_Unloaded;
            Unloaded += NeumorphPanel_Unloaded;
        }

        private void NeumorphPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (closeButton is not null)
            {
                closeButton.Click -= CloseButton_Click;
                closeButton.Click += CloseButton_Click;
            }
        }

        private void NeumorphPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            if (closeButton is not null)
                closeButton.Click -= CloseButton_Click;
        }

        protected override void OnApplyTemplate()
        {
            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            closeButton = (Button)GetTemplateChild("PART_CloseButton");

            base.OnApplyTemplate();
        }


        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonVisibilityProperty =
            DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(NeumorphPanel), new PropertyMetadata(Visibility.Collapsed));

        #region EventHandler

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }

}
