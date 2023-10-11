using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Globalization;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_TimeTextBlock", Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_DateTextBlock", Type = typeof(TextBlock))]
public sealed class DateTimePanel : Control
{
    private DispatcherTimer _digtTimer = new(); // digital timer

    private TextBlock timeTextBlock;
    private TextBlock dateTextBlock;

    public DateTimePanel()
    {
        this.DefaultStyleKey = typeof(DateTimePanel);

        // ----- Exit initialisation here in DesignMode  ------------------------
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        _digtTimer = new DispatcherTimer
        {
            Interval = new TimeSpan(0, 0, 0, 30, 0)
        };
        _digtTimer.Tick -= _digtTimer_Tick1;
        _digtTimer.Tick += _digtTimer_Tick1;
        _digtTimer.Start();
    }

    private void DateTimePanel_Unloaded(object sender, RoutedEventArgs e)
    {
        _digtTimer.Tick -= _digtTimer_Tick1;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        timeTextBlock = (TextBlock)GetTemplateChild("PART_TimeTextBlock");
        dateTextBlock = (TextBlock)GetTemplateChild("PART_DateTextBlock");
        _digtTimer_Tick1(this, null);
    }

    private void _digtTimer_Tick1(object sender, object e)
    {
        CultureInfo culture = new(CultureName);

        if (this.dateTextBlock is not null)
            this.dateTextBlock.Text = DateTime.Now.ToString("D", culture);

        if (this.timeTextBlock is not null)
        {
            this.timeTextBlock.Text = DateTime.Now.ToString("t", culture);
        }
    }


    public string CultureName
    {
        get { return (string)GetValue(CultureNameProperty); }
        set { SetValue(CultureNameProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MessageType.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CultureNameProperty =
        DependencyProperty.Register("CultureName", typeof(string), typeof(DateTimePanel), new PropertyMetadata("de", OnCultureNameChanged));

    private static void OnCultureNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DateTimePanel target = (DateTimePanel)d;
        if (target is not null && target.timeTextBlock is not null && target.dateTextBlock is not null)
        {
            CultureInfo culture = new(e.NewValue.ToString());
            target.dateTextBlock.Text = DateTime.Now.ToString("D", culture);
            target.timeTextBlock.Text = DateTime.Now.ToString("t", culture);
        }
    }

}
