using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Numerics;

namespace Sales4Pro.WinUI.CustomControls
{
    [TemplatePart(Name = "PART_XButton", Type = typeof(Button))]
    public class NeumorphContentDialog : ContentDialog
    {
        //Button xButton;
        Border xBorder;
        private Button closeButton;

        public NeumorphContentDialog()
        {
            this.DefaultStyleKey = typeof(NeumorphContentDialog);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            closeButton = (Button)GetTemplateChild("PART_CloseButton");

            xBorder = (Border)GetTemplateChild("Container");
            if (xBorder is not null)
            {
                xBorder.Loaded -= XBorder_Loaded;
                xBorder.Loaded += XBorder_Loaded;
            }
        }

        private void XNeumorphHeaderedPanel_CloseClicked(object sender, System.EventArgs e)
        {
            Hide();
        }

        private void XBorder_Loaded(object sender, RoutedEventArgs e)
        {
            Border border = sender as Border;
            border.Translation = new Vector3(0, 0, -100);

            if (closeButton is not null)
            {
                closeButton.Click -= CloseButton_Click;
                closeButton.Click += CloseButton_Click;
            }
        }

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(NeumorphContentDialog), new PropertyMetadata(new SolidColorBrush(Colors.DarkBlue)));


        #region EventHandler

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        #endregion


    }
}
