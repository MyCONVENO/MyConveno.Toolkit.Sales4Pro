using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using System.Numerics;
using Windows.Foundation;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
[TemplatePart(Name = "PART_LayoutRoot", Type = typeof(Grid))]
//[TemplatePart(Name = "PART_ShadowElement", Type = typeof(Border))]
public sealed class DynamicResizeFrame : Frame
{
    private ContentPresenter contentPresenter;
    private Border shadowElement;
    private Compositor _compositor;

    public DynamicResizeFrame()
    {
        this.DefaultStyleKey = typeof(DynamicResizeFrame);
        IsTabStop = false;

        this.SizeChanged -= DynamicResizeFrame_SizeChanged;
        this.SizeChanged += DynamicResizeFrame_SizeChanged;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // ----- Exit initialisation here in DesignMode  ------------------------
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        contentPresenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");
        if (contentPresenter is not null)
        {
            contentPresenter.Clip = new RectangleGeometry();
            contentPresenter.Clip.Rect = new Rect(0, 0, contentPresenter.ActualWidth, contentPresenter.ActualHeight);
        }

        shadowElement = (Border)GetTemplateChild("PART_ShadowElement");
        if (shadowElement is not null)
            _compositor = ElementCompositionPreview.GetElementVisual(shadowElement).Compositor;
    }

    #region DependencyProperties

    public double Elevation
    {
        get { return (double)GetValue(ElevationProperty); }
        set { SetValue(ElevationProperty, value); }
    }

    // Using a DependencyProperty as the backing store for BlurRadius.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ElevationProperty =
        DependencyProperty.Register("Elevation", typeof(double), typeof(DynamicResizeFrame), new PropertyMetadata(8.0, OnElevationChanged));

    private static void OnElevationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        DynamicResizeFrame target = (DynamicResizeFrame)d;
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled == false)
            target.UpdateVisuals(new Size(target.ActualWidth, target.ActualHeight));
    }

    #endregion

    private void DynamicResizeFrame_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (contentPresenter is not null)
        {
            contentPresenter.Clip = new RectangleGeometry();
            contentPresenter.Clip.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
        }

        UpdateVisuals(new Size(e.NewSize.Width, e.NewSize.Height));
    }

    private void UpdateVisuals(Size size)
    {
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return;

        if (_compositor is null)
            return;

        SpriteVisual myVisual = _compositor.CreateSpriteVisual();
        myVisual.BorderMode = CompositionBorderMode.Hard;
        myVisual.Size = new Vector2((float)size.Width, (float)size.Height);

        //create a drop shadow
        DropShadow shadow = _compositor.CreateDropShadow();
        shadow.Color = Colors.Black;
        shadow.Offset = new Vector3((float)0.0, (float)Elevation, (float)0.0);
        shadow.BlurRadius = (float)Elevation * 2.0f;
        shadow.Opacity = 0.2f;
        myVisual.Shadow = shadow;

        if (shadowElement is not null)
            ElementCompositionPreview.SetElementChildVisual(shadowElement, myVisual);
    }

}
