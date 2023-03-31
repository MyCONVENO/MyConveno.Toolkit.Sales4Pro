﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class StatusBarSpinIcon : ContentControl
{
    public StatusBarSpinIcon()
    {
        this.DefaultStyleKey = typeof(StatusBarSpinIcon);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // ----- Exit initialisation here in DesignMode  ------------------------
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        if (IsSpinning)
            VisualStateManager.GoToState(this, "SpinningVisualState", true);
        else
            VisualStateManager.GoToState(this, "NormalVisualState", true);
    }

    public bool IsSpinning
    {
        get { return (bool)GetValue(IsSpinningProperty); }
        set { SetValue(IsSpinningProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsSpinning.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsSpinningProperty =
        DependencyProperty.Register("IsSpinning", typeof(bool), typeof(StatusBarSpinIcon), new PropertyMetadata(false, IsSpinningChanged));

    private static void IsSpinningChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        StatusBarSpinIcon target = (StatusBarSpinIcon)d;

        if ((bool)e.NewValue)
            VisualStateManager.GoToState(target, "SpinningVisualState", true);
        else
            VisualStateManager.GoToState(target, "NormalVisualState", true);
    }
}
