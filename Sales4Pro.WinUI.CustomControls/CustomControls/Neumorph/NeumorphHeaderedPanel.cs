//using Microsoft.UI;
//using Microsoft.UI.Composition;
//using Microsoft.UI.Xaml;
//using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Hosting;
//using System;
//using System.Numerics;
//using Windows.Foundation;

//namespace Sales4Pro.WinUI.CustomControls
//{
//    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
//    public sealed class NeumorphHeaderedPanel : ContentControl
//    {
//        public event EventHandler CloseClicked;
//        private Button closeButton;
//        private Border shadowElementWhite;
//        private Border shadowElementBlack;
//        private Compositor _compositorWhite;
//        private Compositor _compositorBlack;

//        public NeumorphHeaderedPanel()
//        {
//            this.DefaultStyleKey = typeof(NeumorphHeaderedPanel);

//            // ----- Exit initialisation here in DesignMode  ------------------------
//            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
//                return;
//            // ----------------------------------------------------------------------

//            Loaded -= NeumorphHeaderedPanel_Loaded;
//            Loaded += NeumorphHeaderedPanel_Loaded;
//            Unloaded -= NeumorphHeaderedPanel_Unloaded;
//            Unloaded += NeumorphHeaderedPanel_Unloaded;

//            SizeChanged -= NeumorphHeaderedPanel_SizeChanged;
//            SizeChanged += NeumorphHeaderedPanel_SizeChanged;
//        }

//        private void NeumorphHeaderedPanel_SizeChanged(object sender, SizeChangedEventArgs e)
//        {
//            UpdateVisuals(new Size(e.NewSize.Width, e.NewSize.Height));

//        }

//        private void NeumorphHeaderedPanel_Loaded(object sender, RoutedEventArgs e)
//        {
//            if (closeButton is not null)
//            {
//                closeButton.Click -= CloseButton_Click;
//                closeButton.Click += CloseButton_Click;
//            }
//        }

//        private void NeumorphHeaderedPanel_Unloaded(object sender, RoutedEventArgs e)
//        {
//            if (closeButton is not null)
//                closeButton.Click -= CloseButton_Click;
//        }

//        ~NeumorphHeaderedPanel()
//        { }

//        protected override void OnApplyTemplate()
//        {
//            base.OnApplyTemplate();

//            // ----- Exit initialisation here in DesignMode  ------------------------
//            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
//                return;
//            // ----------------------------------------------------------------------

//            closeButton = (Button)GetTemplateChild("PART_CloseButton");

//            shadowElementWhite = (Border)GetTemplateChild("PART_ShadowElementWhite");
//            if (shadowElementWhite is not null)
//                _compositorWhite = ElementCompositionPreview.GetElementVisual(shadowElementWhite).Compositor;

//            shadowElementBlack = (Border)GetTemplateChild("PART_ShadowElementBlack");
//            if (shadowElementBlack is not null)
//                _compositorBlack = ElementCompositionPreview.GetElementVisual(shadowElementBlack).Compositor;
//        }

//        #region DependencyProperties

//        public string HeaderText
//        {
//            get { return (string)GetValue(HeaderTextProperty); }
//            set { SetValue(HeaderTextProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for HeaderText.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty HeaderTextProperty =
//            DependencyProperty.Register("HeaderText", typeof(string), typeof(NeumorphHeaderedPanel), new PropertyMetadata("Header"));


//        public Visibility CloseButtonVisibility
//        {
//            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
//            set { SetValue(CloseButtonVisibilityProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty CloseButtonVisibilityProperty =
//            DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(NeumorphHeaderedPanel), new PropertyMetadata(Visibility.Visible));




//        public double Elevation
//        {
//            get { return (double)GetValue(ElevationProperty); }
//            set { SetValue(ElevationProperty, value); }
//        }

//        // Using a DependencyProperty as the backing store for BlurRadius.  This enables animation, styling, binding, etc...
//        public static readonly DependencyProperty ElevationProperty =
//            DependencyProperty.Register("Elevation", typeof(double), typeof(NeumorphHeaderedPanel), new PropertyMetadata(4.0, OnElevationChanged));

//        private static void OnElevationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            NeumorphHeaderedPanel target = (NeumorphHeaderedPanel)d;
//            target.UpdateVisuals(new Size(target.ActualWidth, target.ActualHeight));
//        }


//        #endregion

//        private void UpdateVisuals(Size size)
//        {
//            this.Clip = new();
//            this.Clip.Rect = new Rect(-20, -20, size.Width + 40, size.Height + 40);

//            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
//                return;

//            if (_compositorWhite is null)
//                return;



//            SpriteVisual myVisualBlack = _compositorBlack.CreateSpriteVisual();
//            myVisualBlack.BorderMode = CompositionBorderMode.Hard;
//            myVisualBlack.Size = new Vector2((float)size.Width + 4, (float)size.Height + 4);

//            //create a drop shadow
//            DropShadow shadowBlack = _compositorBlack.CreateDropShadow();
//            shadowBlack.Color = Colors.White;
//            shadowBlack.Offset = new Vector3((float)Elevation * -1 - 4, (float)Elevation * -1 - 4, (float)0.0);
//            shadowBlack.BlurRadius = (float)Elevation + 6.0f;
//            shadowBlack.Opacity = 0.7f;
//            myVisualBlack.Shadow = shadowBlack;

//            if (shadowElementBlack is not null)
//                ElementCompositionPreview.SetElementChildVisual(shadowElementBlack, myVisualBlack);



//            SpriteVisual myVisualWhite = _compositorWhite.CreateSpriteVisual();
//            myVisualWhite.BorderMode = CompositionBorderMode.Hard;
//            myVisualWhite.Size = new Vector2((float)size.Width, (float)size.Height);

//            //create a drop shadow
//            DropShadow shadowWhite = _compositorWhite.CreateDropShadow();
//            shadowWhite.Color = Colors.Black;
//            shadowWhite.Offset = new Vector3((float)Elevation + 4, (float)Elevation + 4, (float)0.0);
//            shadowWhite.BlurRadius = (float)Elevation + 6.0f;
//            shadowWhite.Opacity = 0.15f;
//            myVisualWhite.Shadow = shadowWhite;

//            if (shadowElementWhite is not null)
//                ElementCompositionPreview.SetElementChildVisual(shadowElementWhite, myVisualWhite);
//        }



//        #region EventHandler

//        private void CloseButton_Click(object sender, RoutedEventArgs e)
//        {
//            CloseClicked?.Invoke(this, EventArgs.Empty);
//        }

//        #endregion

//    }

//}
