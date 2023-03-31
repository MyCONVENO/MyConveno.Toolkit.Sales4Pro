using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel;
using Windows.UI;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_AccentOverlayBackground", Type = typeof(Border))]
[TemplatePart(Name = "PART_TitleFontIcon", Type = typeof(FontIcon))]
[TemplatePart(Name = "PART_TitleTextBlock", Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_TitleTextTextBlock", Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
public sealed class IconInfoPanel : ContentControl
{
    private Border baseBorder;
    private FontIcon titleFontIcon;
    private TextBlock titleTextBlock;
    private TextBlock titleTextTextBlock;
    private ContentPresenter contentPresenter;

    public IconInfoPanel()
    {
        // ----- Exit initialisation here in DesignMode  ------------------------
        if (DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        this.DefaultStyleKey = typeof(IconInfoPanel);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // ----- Exit initialisation here in DesignMode  ------------------------
        if (DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        baseBorder = (Border)GetTemplateChild("PART_AccentOverlayBackground");
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
        DependencyProperty.Register("Title", typeof(string), typeof(IconInfoPanel), new PropertyMetadata("Titel:"));

    public string TitleText
    {
        get { return (string)GetValue(TitleTextProperty); }
        set { SetValue(TitleTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TitleTextProperty =
        DependencyProperty.Register("TitleText", typeof(string), typeof(IconInfoPanel), new PropertyMetadata("TitleText"));


    public enum messageTypeEnum { Info, Error, Warning, Help, Message, Accent }
    public messageTypeEnum MessageType
    {
        get { return (messageTypeEnum)GetValue(MessageTypeProperty); }
        set { SetValue(MessageTypeProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MessageType.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MessageTypeProperty =
        DependencyProperty.Register("MessageType", typeof(messageTypeEnum), typeof(IconInfoPanel), new PropertyMetadata(messageTypeEnum.Error, OnMessageTypeChanged));

    private static void OnMessageTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        IconInfoPanel target = (IconInfoPanel)d;
        if (target is not null)
            target.UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        Brush backgroundBrush;
        Brush foregroundBrush;

        switch (MessageType)
        {
            case messageTypeEnum.Info:

                backgroundBrush = new SolidColorBrush(Color.FromArgb(255, 222, 238, 222));
                foregroundBrush = new SolidColorBrush(Colors.DarkGreen);

                if (baseBorder is not null)
                    baseBorder.Background = backgroundBrush;

                if (titleFontIcon is not null)
                {
                    titleFontIcon.Glyph = "\uE930"; // Info (OK)
                    titleFontIcon.Foreground = foregroundBrush;
                }

                if (titleTextBlock is not null)
                    titleTextBlock.Foreground = foregroundBrush;

                if (titleTextTextBlock is not null)
                    titleTextTextBlock.Foreground = foregroundBrush;

                if (contentPresenter is not null)
                    contentPresenter.Foreground = foregroundBrush;

                break;
            case messageTypeEnum.Error:

                backgroundBrush = new SolidColorBrush(Color.FromArgb(255, 238, 222, 222));
                foregroundBrush = new SolidColorBrush(Colors.DarkRed);

                if (baseBorder is not null)
                    baseBorder.Background = backgroundBrush;

                if (titleFontIcon is not null)
                {
                    titleFontIcon.Glyph = "\uE783"; // Attention Kreis
                    titleFontIcon.Foreground = foregroundBrush;
                }

                if (titleTextBlock is not null)
                    titleTextBlock.Foreground = foregroundBrush;

                if (titleTextTextBlock is not null)
                    titleTextTextBlock.Foreground = foregroundBrush;

                if (contentPresenter is not null)
                    contentPresenter.Foreground = foregroundBrush;

                break;
            case messageTypeEnum.Warning:

                backgroundBrush = new SolidColorBrush(Color.FromArgb(255, 255, 242, 157));
                foregroundBrush = new SolidColorBrush(Colors.Black);

                if (baseBorder is not null)
                    baseBorder.Background = backgroundBrush;

                if (titleFontIcon is not null)
                {
                    titleFontIcon.Glyph = "\uE7BA"; // Attention Dreieck
                    titleFontIcon.Foreground = foregroundBrush;
                }

                if (titleTextBlock is not null)
                    titleTextBlock.Foreground = foregroundBrush;

                if (titleTextTextBlock is not null)
                    titleTextTextBlock.Foreground = foregroundBrush;

                if (contentPresenter is not null)
                    contentPresenter.Foreground = foregroundBrush;

                break;
            case messageTypeEnum.Help:
                backgroundBrush = (Brush)Application.Current.Resources["SystemControlBackgroundAccentBrush"];
                foregroundBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

                if (baseBorder is not null)
                    baseBorder.Background = backgroundBrush;

                if (titleFontIcon is not null)
                {
                    titleFontIcon.Glyph = "\uEA80";  // Bulb
                    titleFontIcon.Foreground = foregroundBrush;
                }

                if (titleTextBlock is not null)
                    titleTextBlock.Foreground = foregroundBrush;

                if (titleTextTextBlock is not null)
                    titleTextTextBlock.Foreground = foregroundBrush;

                if (contentPresenter is not null)
                    contentPresenter.Foreground = foregroundBrush;

                break;

            case messageTypeEnum.Accent:
                backgroundBrush = (Brush)Application.Current.Resources["SystemControlBackgroundAccentBrush"];
                foregroundBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

                if (baseBorder is not null)
                {
                    baseBorder.Background = backgroundBrush;
                }

                if (titleFontIcon is not null)
                {
                    titleFontIcon.Glyph = "\uEA80";  // Bulb
                    titleFontIcon.Foreground = foregroundBrush;
                }

                if (titleTextBlock is not null)
                    titleTextBlock.Foreground = foregroundBrush;

                if (titleTextTextBlock is not null)
                    titleTextTextBlock.Foreground = foregroundBrush;

                if (contentPresenter is not null)
                    contentPresenter.Foreground = foregroundBrush;

                break;

            case messageTypeEnum.Message:

                backgroundBrush = new SolidColorBrush(Color.FromArgb(255, 243, 243, 243));
                foregroundBrush = new SolidColorBrush(Colors.Black);

                if (baseBorder is not null)
                    baseBorder.Background = backgroundBrush;

                if (titleFontIcon is not null)
                {
                    titleFontIcon.Glyph = "\uE8BD"; // Message
                    titleFontIcon.Foreground = foregroundBrush;
                }

                if (titleTextBlock is not null)
                    titleTextBlock.Foreground = foregroundBrush;

                if (titleTextTextBlock is not null)
                    titleTextTextBlock.Foreground = foregroundBrush;

                if (contentPresenter is not null)
                    contentPresenter.Foreground = foregroundBrush;

                break;
            default:
                break;
        }
    }
}

