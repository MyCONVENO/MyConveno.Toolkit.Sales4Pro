using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class IconToggleButton : ToggleButton
{
    public IconToggleButton()
    {
        this.DefaultStyleKey = typeof(IconToggleButton);
    }

    public Object UnCheckedContent
    {
        get { return (Object)GetValue(UnCheckedContentProperty); }
        set { SetValue(UnCheckedContentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for UnCheckedIcon.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty UnCheckedContentProperty =
        DependencyProperty.Register("UnCheckedContent", typeof(Object), typeof(IconToggleButton), new PropertyMetadata(new FontIcon() { Glyph = "" }));

    public Object CheckedContent
    {
        get { return (Object)GetValue(CheckedContentProperty); }
        set { SetValue(CheckedContentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for CheckedIcon.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CheckedContentProperty =
        DependencyProperty.Register("CheckedContent", typeof(Object), typeof(IconToggleButton), new PropertyMetadata(new FontIcon() { Glyph = "" }));

}
