using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class NeumorphInnerShadowContentControl : ContentControl
{
    private Border shadowTarget;
    private AttachedDropShadow attachedDropShadow;

    public NeumorphInnerShadowContentControl()
    {
        DefaultStyleKey = typeof(NeumorphInnerShadowContentControl);
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        shadowTarget = (Border)GetTemplateChild("ShadowTarget");
        attachedDropShadow = (AttachedDropShadow)GetTemplateChild("AttachedDropShadow1");

        if (attachedDropShadow is not null)
        {
            attachedDropShadow.CastTo = shadowTarget;
        }
    }

    public CornerRadius DarkCornerRadius
    {
        get { return (CornerRadius)GetValue(DarkCornerRadiusProperty); }
        set { SetValue(DarkCornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DarkCornerRadiusProperty =
        DependencyProperty.Register("DarkCornerRadius", typeof(CornerRadius), typeof(NeumorphInnerShadowContentControl), new PropertyMetadata(new Thickness(4,4,4,4)));


    public CornerRadius LightCornerRadius
    {
        get { return (CornerRadius)GetValue(LightCornerRadiusProperty); }
        set { SetValue(LightCornerRadiusProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty LightCornerRadiusProperty =
        DependencyProperty.Register("LightCornerRadius", typeof(CornerRadius), typeof(NeumorphInnerShadowContentControl), new PropertyMetadata(new Thickness(4, 4, 4, 4)));


}
