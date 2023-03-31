using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using System.Numerics;
using Windows.Foundation;

namespace Sales4Pro.WinUI.CustomControls.Menu
{
    [TemplatePart(Name = "PART_ContentPresenter", Type = typeof(ContentPresenter))]
    public sealed class SubMenuShadowAppBar : ContentControl
    {
        private ContentPresenter contentPresenter;
        private Border shadowElement;
        private Compositor _compositor;

        public SubMenuShadowAppBar()
        {
            this.DefaultStyleKey = typeof(SubMenuShadowAppBar);

            // Exit initialisation here in DesignMode
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled == false)
            {
                SizeChanged += SubMenuAppBar_SizeChanged;
            }
        }

        ~SubMenuShadowAppBar()
        { }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            contentPresenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");

            // Exit initialisation here in DesignMode
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;

            shadowElement = (Border)GetTemplateChild("PART_ShadowElement");
            if (shadowElement is not null)
                _compositor = ElementCompositionPreview.GetElementVisual(shadowElement).Compositor;

            UpdateContentTransition();
        }

        #region EventHandler

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            UpdateContentTransition();
            base.OnContentChanged(oldContent, newContent);
        }

        #endregion

        #region DependencyProperties

        public bool IsContentTransitionEnabled
        {
            get { return (bool)GetValue(IsContentTransitionEnabledProperty); }
            set { SetValue(IsContentTransitionEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsContentTransitionEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsContentTransitionEnabledProperty =
            DependencyProperty.Register("IsContentTransitionEnabled", typeof(bool), typeof(SubMenuShadowAppBar), new PropertyMetadata(true, OnIsContentTransitionEnabledChanged));

        private static void OnIsContentTransitionEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SubMenuShadowAppBar target = (SubMenuShadowAppBar)d;
            target.UpdateContentTransition();
        }

        //public ImageSource ClientImageSource
        //{
        //    get { return (ImageSource)GetValue(ClientImageSourceProperty); }
        //    set { SetValue(ClientImageSourceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ClientImageSource.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ClientImageSourceProperty =
        //    DependencyProperty.Register("ClientImageSource", typeof(ImageSource), typeof(SubMenuShadowAppBar), new PropertyMetadata(null));


        public double Elevation
        {
            get { return (double)GetValue(ElevationProperty); }
            set { SetValue(ElevationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlurRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ElevationProperty =
            DependencyProperty.Register("Elevation", typeof(double), typeof(SubMenuShadowAppBar), new PropertyMetadata(4.0, OnElevationChanged));

        private static void OnElevationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SubMenuShadowAppBar target = (SubMenuShadowAppBar)d;
            target.UpdateVisuals(new Size(target.ActualWidth, target.ActualHeight));
        }

        #endregion

        #region Commands

        private void UpdateContentTransition()
        {
            if (IsContentTransitionEnabled)
            {
                if (contentPresenter is not null && contentPresenter.ContentTransitions.Count == 0)
                    contentPresenter.ContentTransitions.Add(new ContentThemeTransition() { VerticalOffset = 0, HorizontalOffset = -80 });
            }
            else
            {
                if (contentPresenter is not null)
                    contentPresenter.ContentTransitions.Clear();
            }
        }

        private void SubMenuAppBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateVisuals(new Size(e.NewSize.Width, e.NewSize.Height));
        }

        private void UpdateVisuals(Size size)
        {
            this.Clip = new RectangleGeometry();
            this.Clip.Rect = new Rect(0, 0, size.Width, size.Height + 28);

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
            shadow.BlurRadius = (float)Elevation + 2.0f;
            shadow.Opacity = 0.2f;
            myVisual.Shadow = shadow;

            if (shadowElement is not null)
                ElementCompositionPreview.SetElementChildVisual(shadowElement, myVisual);
        }

        #endregion

    }
}
