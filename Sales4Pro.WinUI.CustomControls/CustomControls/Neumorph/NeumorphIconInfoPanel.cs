using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel;
using Windows.UI;

namespace Sales4Pro.WinUI.CustomControls
{
    [TemplatePart(Name = "PART_BaseGrid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_TitleFontIcon", Type = typeof(FontIcon))]
    [TemplatePart(Name = "PART_TitleTextBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_TitleTextTextBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    public sealed class NeumorphIconInfoPanel : ContentControl
    {
        private Grid baseGrid;
        private FontIcon titleFontIcon;
        private TextBlock titleTextBlock;
        private TextBlock titleTextTextBlock;
        private ContentPresenter contentPresenter;

        public NeumorphIconInfoPanel()
        {
            // ----- Exit initialisation here in DesignMode  ------------------------
            if (DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            this.DefaultStyleKey = typeof(NeumorphIconInfoPanel);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // ----- Exit initialisation here in DesignMode  ------------------------
            if (DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            baseGrid = (Grid)GetTemplateChild("PART_BaseGrid");
            titleFontIcon = (FontIcon)GetTemplateChild("PART_TitleFontIcon");
            titleTextBlock = (TextBlock)GetTemplateChild("PART_TitleTextBlock");
            titleTextTextBlock = (TextBlock)GetTemplateChild("PART_TitleTextTextBlock");
            contentPresenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");

            UpdateVisuals();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NeumorphIconInfoPanel), new PropertyMetadata("Titel:"));

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(NeumorphIconInfoPanel), new PropertyMetadata("TitleText"));


        public enum messageTypeEnum { Info, Error, Warning, Help, Message }
        public messageTypeEnum MessageType
        {
            get { return (messageTypeEnum)GetValue(MessageTypeProperty); }
            set { SetValue(MessageTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MessageType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MessageTypeProperty =
            DependencyProperty.Register("MessageType", typeof(messageTypeEnum), typeof(NeumorphIconInfoPanel), new PropertyMetadata(messageTypeEnum.Error, OnMessageTypeChanged));

        private static void OnMessageTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NeumorphIconInfoPanel target = (NeumorphIconInfoPanel)d;
            if (target is not null)
                target.UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            switch (MessageType)
            {
                case messageTypeEnum.Info:

                    Brush lightGreenBrush = new SolidColorBrush(Color.FromArgb(255, 222, 238, 222));
                    Brush darkGreenBrush = new SolidColorBrush(Colors.DarkGreen);

                    if (baseGrid is not null)
                        baseGrid.Background = lightGreenBrush;

                    if (titleFontIcon is not null)
                    {
                        titleFontIcon.Glyph = "\uE930"; // Info (OK)
                        titleFontIcon.Foreground = darkGreenBrush;
                    }

                    if (titleTextBlock is not null)
                        titleTextBlock.Foreground = darkGreenBrush;

                    if (titleTextTextBlock is not null)
                        titleTextTextBlock.Foreground = darkGreenBrush;

                    if (contentPresenter is not null)
                        contentPresenter.Foreground = darkGreenBrush;

                    break;
                case messageTypeEnum.Error:

                    Brush lightRedBrush = new SolidColorBrush(Color.FromArgb(255, 238, 222, 222));
                    Brush darkRedBrush = new SolidColorBrush(Colors.DarkRed);

                    if (baseGrid is not null)
                        baseGrid.Background = lightRedBrush;

                    if (titleFontIcon is not null)
                    {
                        titleFontIcon.Glyph = "\uE783"; // Attention Kreis
                        titleFontIcon.Foreground = darkRedBrush;
                    }

                    if (titleTextBlock is not null)
                        titleTextBlock.Foreground = darkRedBrush;

                    if (titleTextTextBlock is not null)
                        titleTextTextBlock.Foreground = darkRedBrush;

                    if (contentPresenter is not null)
                        contentPresenter.Foreground = darkRedBrush;

                    break;
                case messageTypeEnum.Warning:

                    Brush lightYellowBrush = new SolidColorBrush(Color.FromArgb(255, 255, 242, 157));
                    Brush darkYellowBrush = new SolidColorBrush(Colors.Black);

                    if (baseGrid is not null)
                        baseGrid.Background = lightYellowBrush;

                    if (titleFontIcon is not null)
                    {
                        titleFontIcon.Glyph = "\uE7BA"; // Attention Dreieck
                        titleFontIcon.Foreground = darkYellowBrush;
                    }

                    if (titleTextBlock is not null)
                        titleTextBlock.Foreground = darkYellowBrush;

                    if (titleTextTextBlock is not null)
                        titleTextTextBlock.Foreground = darkYellowBrush;

                    if (contentPresenter is not null)
                        contentPresenter.Foreground = darkYellowBrush;

                    break;
                case messageTypeEnum.Help:
                    //Brush lightBlueBrush = (Brush)Application.Current.Resources["SystemControlBackgroundAccentBrush"];
                    //Brush darkBlueBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

                    //Brush lightBlueBrush = new SolidColorBrush(Color.FromArgb(255, 214, 219, 233));
                    //Brush darkBlueBrush = new SolidColorBrush(Color.FromArgb(255, 41, 57, 85));

                    //if (baseGrid is not null)
                    //    baseGrid.Background = lightBlueBrush;

                    if (titleFontIcon is not null)
                    {
                        titleFontIcon.Glyph = "\uEA80";  // Bulb
                        //titleFontIcon.Foreground = darkBlueBrush;
                    }

                    //if (titleTextBlock is not null)
                    //    titleTextBlock.Foreground = darkBlueBrush;

                    //if (titleTextTextBlock is not null)
                    //    titleTextTextBlock.Foreground = darkBlueBrush;

                    //if (contentPresenter is not null)
                    //    contentPresenter.Foreground = darkBlueBrush;

                    break;
                case messageTypeEnum.Message:

                    Brush transparentBrush = new SolidColorBrush(Color.FromArgb(255, 243, 243, 243));
                    Brush blackBrush = new SolidColorBrush(Colors.Black);

                    if (baseGrid is not null)
                        baseGrid.Background = transparentBrush;

                    if (titleFontIcon is not null)
                    {
                        titleFontIcon.Glyph = "\uE8BD"; // Message
                        titleFontIcon.Foreground = blackBrush;
                    }

                    if (titleTextBlock is not null)
                        titleTextBlock.Foreground = blackBrush;

                    if (titleTextTextBlock is not null)
                        titleTextTextBlock.Foreground = blackBrush;

                    if (contentPresenter is not null)
                        contentPresenter.Foreground = blackBrush;

                    break;
                default:
                    break;
            }
        }
    }

}
