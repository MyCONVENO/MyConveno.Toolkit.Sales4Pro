using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Windows.UI.Text;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_HeaderTextBlock", Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_TextTextBlock", Type = typeof(TextBlock))]
public sealed class PageEntryTextControl : Control
{
    private TextBlock headerTextBlock;
    private TextBlock textTextBlock;

    public PageEntryTextControl()
    {
        this.DefaultStyleKey = typeof(PageEntryTextControl);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // ----- Exit initialisation here in DesignMode  ------------------------
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        headerTextBlock = (TextBlock)GetTemplateChild("PART_HeaderTextBlock");
        textTextBlock = (TextBlock)GetTemplateChild("PART_TextTextBlock");

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (string.IsNullOrWhiteSpace(Text) && PreventHidingWhenEmpty == false)
            Visibility = Visibility.Collapsed;
        else
            Visibility = Visibility.Visible;

        if (textTextBlock is not null)
        {
            // Setze die TextLineBounds abhängig vom vorhandensein des Headers
            // Wenn kein Header da ist, dann reduziere den Zeilenabstand (optisch schöner)
            if (string.IsNullOrWhiteSpace(Title))
                textTextBlock.TextLineBounds = TextLineBounds.TrimToBaseline;
            else
                textTextBlock.TextLineBounds = TextLineBounds.Full;

            // Mache den Content fett, wenn es gewünscht ist
            if (IsBoldContentText)
                textTextBlock.Style = (Style)Application.Current.Resources["TitleTextBlockStyle"];
            else
                textTextBlock.Style = (Style)Application.Current.Resources["BodyTextBlockStyle"];

            // Färbe den Content mit der Accentfarbe, wenn es gewünscht ist
            if (IsAccentContentText)
                textTextBlock.Foreground = (Brush)Application.Current.Resources["SystemControlForegroundAccentBrush"];
        }

        if (headerTextBlock is not null && textTextBlock is not null)
        {
            if (IsIdle)
            {
                Run runTitle = new Run { Text = Title, TextDecorations = TextDecorations.Strikethrough };
                headerTextBlock.Inlines.Clear();
                headerTextBlock.Inlines.Add(runTitle);

                Run runText = new Run { Text = Text, TextDecorations = TextDecorations.Strikethrough };
                textTextBlock.Inlines.Clear();
                textTextBlock.Inlines.Add(runText);
            }
            else
            {
                Run runTitle = new Run { Text = Title, TextDecorations = TextDecorations.None };
                headerTextBlock.Inlines.Clear();
                headerTextBlock.Inlines.Add(runTitle);

                Run runText = new Run { Text = Text, TextDecorations = TextDecorations.None };
                textTextBlock.Inlines.Clear();
                textTextBlock.Inlines.Add(runText);
            }
        }
    }

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(PageEntryTextControl), new PropertyMetadata(string.Empty));


    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(PageEntryTextControl), new PropertyMetadata("Text", OnTextChanged));

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PageEntryTextControl target = (PageEntryTextControl)d;
        target.UpdateVisuals();
    }


    public double CaptionMinWidth
    {
        get { return (double)GetValue(CaptionMinWidthProperty); }
        set { SetValue(CaptionMinWidthProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CaptionMinWidthProperty =
        DependencyProperty.Register("CaptionMinWidth", typeof(double), typeof(PageEntryTextControl), new PropertyMetadata(140.0));


    public bool IsBoldContentText
    {
        get { return (bool)GetValue(IsBoldContentTextProperty); }
        set { SetValue(IsBoldContentTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BlurRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsBoldContentTextProperty =
        DependencyProperty.Register("IsBoldTextContent", typeof(bool), typeof(PageEntryTextControl), new PropertyMetadata(false, OnIsBoldContentTextChanged));

    private static void OnIsBoldContentTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PageEntryTextControl target = (PageEntryTextControl)d;
        if (target.textTextBlock is not null)
            if ((bool)e.NewValue)
                target.textTextBlock.Style = (Style)Application.Current.Resources["TitleTextBlockStyle"];
            else
                target.textTextBlock.Style = (Style)Application.Current.Resources["BodyTextBlockStyle"];
    }


    public bool IsAccentContentText
    {
        get { return (bool)GetValue(IsAccentContentTextProperty); }
        set { SetValue(IsAccentContentTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BlurRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsAccentContentTextProperty =
        DependencyProperty.Register("IsAccentTextContent", typeof(bool), typeof(PageEntryTextControl), new PropertyMetadata(false, OnIsAccentContentTextChanged));

    private static void OnIsAccentContentTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PageEntryTextControl target = (PageEntryTextControl)d;
        if (target.textTextBlock is not null)
            if ((bool)e.NewValue)
                target.textTextBlock.Foreground = (Brush)Application.Current.Resources["SystemControlForegroundAccentBrush"];
            else
                target.textTextBlock.Foreground = new SolidColorBrush(Colors.Black);
    }

    public bool PreventHidingWhenEmpty
    {
        get { return (bool)GetValue(PreventHidingWhenEmptyProperty); }
        set { SetValue(PreventHidingWhenEmptyProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BlurRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty PreventHidingWhenEmptyProperty =
        DependencyProperty.Register("PreventHidingWhenEmpty", typeof(bool), typeof(PageEntryTextControl), new PropertyMetadata(false));




    public bool IsIdle
    {
        get { return (bool)GetValue(IsIdleProperty); }
        set { SetValue(IsIdleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BlurRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsIdleProperty =
        DependencyProperty.Register("IsIdle", typeof(bool), typeof(PageEntryTextControl), new PropertyMetadata(false, OnIsIdleChanged));


    private static void OnIsIdleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        PageEntryTextControl target = (PageEntryTextControl)d;
        target.UpdateVisuals();
    }

}
