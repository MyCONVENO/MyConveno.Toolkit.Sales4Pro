using Microsoft.Graphics.Canvas.Effects;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Windows.UI;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_GlassHostElement", Type = typeof(Canvas))]
public sealed class DropTargetAdorner : Control
{
    private Canvas glassHostElement;

    public DropTargetAdorner()
    {
        this.DefaultStyleKey = typeof(DropTargetAdorner);
        IsEnabled = false;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // ----- Exit initialisation here in DesignMode  ------------------------
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        glassHostElement = (Canvas)GetTemplateChild("PART_GlassHostElement");
        if (glassHostElement is not null)
            InitializeFrostedGlass(glassHostElement);

        IsEnabledChanged -= DropTargetAdorner_IsEnabledChanged;
        IsEnabledChanged += DropTargetAdorner_IsEnabledChanged;

        VisualStateManager.GoToState(this, "NormalVisualState", true);
    }

    private void DropTargetAdorner_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if ((bool)e.NewValue == true)
        {
            Visibility = Visibility.Visible;
            VisualStateManager.GoToState(this, "PulsatingVisualState", true);
        }
        else
        {
            Visibility = Visibility.Collapsed;
            VisualStateManager.GoToState(this, "NormalVisualState", true);
        }
    }

    #region Dependency Properties

    public Visibility GlasHostElementVisibility
    {
        get { return (Visibility)GetValue(GlasHostElementVisibilityProperty); }
        set { SetValue(GlasHostElementVisibilityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for GlasHostElementVisibility.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty GlasHostElementVisibilityProperty =
        DependencyProperty.Register("GlasHostElementVisibility", typeof(Visibility), typeof(DropTargetAdorner), new PropertyMetadata(Visibility.Collapsed));

    #endregion

    private void InitializeFrostedGlass(UIElement glassHost)
    {
        Visual hostVisual = ElementCompositionPreview.GetElementVisual(glassHost);
        Compositor compositor = hostVisual.Compositor;

        // Create a glass effect, requires Win2D NuGet package
        GaussianBlurEffect glassEffect = new GaussianBlurEffect
        {
            BlurAmount = 2f,
            BorderMode = EffectBorderMode.Hard,
            Source = new ArithmeticCompositeEffect
            {
                MultiplyAmount = 0,
                Source1Amount = 0.5f,
                Source2Amount = 0.5f,
                Source1 = new CompositionEffectSourceParameter("backdropBrush"),
                Source2 = new ColorSourceEffect
                {
                    Color = Color.FromArgb(255, 238, 238, 238)
                }
            }
        };

        //  Create an instance of the effect and set its source to a CompositionBackdropBrush
        CompositionEffectFactory effectFactory = compositor.CreateEffectFactory(glassEffect);
        CompositionBackdropBrush backdropBrush = compositor.CreateBackdropBrush();
        CompositionEffectBrush effectBrush = effectFactory.CreateBrush();

        effectBrush.SetSourceParameter("backdropBrush", backdropBrush);

        // Create a Visual to contain the frosted glass effect
        SpriteVisual glassVisual = compositor.CreateSpriteVisual();
        glassVisual.Brush = effectBrush;

        // Add the blur as a child of the host in the visual tree
        ElementCompositionPreview.SetElementChildVisual(glassHost, glassVisual);

        // Make sure size of glass host and glass visual always stay in sync
        ExpressionAnimation bindSizeAnimation = compositor.CreateExpressionAnimation("hostVisual.Size");
        bindSizeAnimation.SetReferenceParameter("hostVisual", hostVisual);

        glassVisual.StartAnimation("Size", bindSizeAnimation);
    }

}

