using CommunityToolkit.WinUI.UI;
using Microsoft.UI.Xaml.Controls;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class DashboardButton : Button
{
    private Border shadowTarget;
    private AttachedDropShadow attachedDropShadow;

    public DashboardButton()
    {
        this.DefaultStyleKey = typeof(DashboardButton);
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
       
}
