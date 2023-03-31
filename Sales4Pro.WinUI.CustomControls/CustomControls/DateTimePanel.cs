using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_TimeTextBlock", Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_DateTextBlock", Type = typeof(TextBlock))]
public sealed class DateTimePanel : Control
{
    private DispatcherTimer _digtTimer = new DispatcherTimer(); // digital timer

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
        if (this.dateTextBlock is not null)
            this.dateTextBlock.Text = DateTime.Now.ToString("D");

        if (this.timeTextBlock is not null)
            this.timeTextBlock.Text = DateTime.Now.ToString("t");
    }

}
