using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Linq;
using Windows.Foundation;

namespace Sales4Pro.WinUI.CustomControls
{
    [TemplatePart(Name = "PART_TitleTextBlock", Type = typeof(TextBlock))]

    public sealed class NeumorphFilterSectionCard : ContentControl
    {
        private ContentPresenter contentPresenter;
        private TextBlock titleTextBlock;

        public NeumorphFilterSectionCard()
        {
            this.DefaultStyleKey = typeof(NeumorphFilterSectionCard);

            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            SizeChanged -= NeumorphFilterSectionCard_SizeChanged;
            SizeChanged += NeumorphFilterSectionCard_SizeChanged;
        }
        ~NeumorphFilterSectionCard()
        { }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            contentPresenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");
            titleTextBlock = (TextBlock)GetTemplateChild("PART_TitleTextBlock");

            if (titleTextBlock is not null)
            {
                if (Title is not null && Title.ToString().Trim().Any())
                    titleTextBlock.Visibility = Visibility.Visible;
                else
                    titleTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private void NeumorphFilterSectionCard_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (IsTopClippingPanelEnabled && contentPresenter is not null)
            {
                contentPresenter.Clip = new RectangleGeometry();
                contentPresenter.Clip.Rect = new Rect(0, 8, contentPresenter.ActualWidth, contentPresenter.ActualHeight - 8);
            }
        }

        public bool IsTopClippingPanelEnabled
        {
            get { return (bool)GetValue(IsTopClippingPanelEnabledProperty); }
            set { SetValue(IsTopClippingPanelEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsTopClippingPanelEnabledProperty =
            DependencyProperty.Register("IsTopClippingPanelEnabled", typeof(bool), typeof(NeumorphFilterSectionCard), new PropertyMetadata(false));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NeumorphFilterSectionCard), new PropertyMetadata("Title", OnTitleChanged));

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NeumorphFilterSectionCard target = (NeumorphFilterSectionCard)d;

            if (target is null || e.NewValue is null)
                return;

            if (target.titleTextBlock is not null)
            {
                if (((string)e.NewValue).Length > 0)
                    target.titleTextBlock.Visibility = Visibility.Visible;
                else
                    target.titleTextBlock.Visibility = Visibility.Collapsed;
            }
        }
    }
}
