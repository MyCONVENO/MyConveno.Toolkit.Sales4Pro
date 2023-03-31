using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using System.Numerics;
using Windows.Foundation;
using static CommunityToolkit.WinUI.UI.Animations.Expressions.ExpressionValues;

namespace Sales4Pro.WinUI.CustomControls
{
    public sealed class NeumorphCard : ContentControl
    {
        private Grid mainGrid;
        private TextBlock headerLabel;

        private ContentPresenter contentPresenter;
        private Border shadowElementBlack;
        private Compositor _compositorBlack;

        public NeumorphCard()
        {
            this.DefaultStyleKey = typeof(NeumorphCard);

            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            SizeChanged -= NeumorphCard_SizeChanged;
            SizeChanged += NeumorphCard_SizeChanged;
        }

        private void NeumorphCard_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //if (IsTopClippingPanelEnabled && contentPresenter is not null)
            //{
            //    contentPresenter.Clip = new();
            //    contentPresenter.Clip.Rect = new Rect(0, 8, contentPresenter.ActualWidth, contentPresenter.ActualHeight - 8);
            //}

            //UpdateVisuals(new Size(e.NewSize.Width, e.NewSize.Height));

        }

        ~NeumorphCard()
        { }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // ----- Exit initialisation here in DesignMode  ------------------------
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                return;
            // ----------------------------------------------------------------------

            contentPresenter = (ContentPresenter)GetTemplateChild("PART_ContentPresenter");
            headerLabel = (TextBlock)GetTemplateChild("PART_HeaderLabel");
            mainGrid = (Grid)GetTemplateChild("PART_MainGrid");

            shadowElementBlack = (Border)GetTemplateChild("PART_ShadowElementBlack");
            if (shadowElementBlack is not null)
                _compositorBlack = ElementCompositionPreview.GetElementVisual(shadowElementBlack).Compositor;

            if (string.IsNullOrEmpty(headerLabel.Text))
                headerLabel.Visibility = Visibility.Collapsed;
            else
                headerLabel.Visibility = Visibility.Visible;

        }

        //private void UpdateVisuals(Size size)
        //{
        //    this.Clip = new();
        //    this.Clip.Rect = new Rect(-2, 0, size.Width + 4, size.Height + 10);

        //    SpriteVisual myVisualBlack = _compositorBlack.CreateSpriteVisual();
        //    myVisualBlack.BorderMode = CompositionBorderMode.Hard;
        //    myVisualBlack.Size = new Vector2((float)mainGrid.ActualWidth, (float)mainGrid.ActualHeight);

        //    //create a drop shadow
        //    DropShadow shadowBlack = _compositorBlack.CreateDropShadow();
        //    shadowBlack.Color = Colors.Black;
        //    shadowBlack.Offset = new Vector3(0.0f, 3.0f, 0.0f);
        //    shadowBlack.BlurRadius = 5.0f;
        //    shadowBlack.Opacity = 0.2f;
        //    myVisualBlack.Shadow = shadowBlack;

        //    if (shadowElementBlack is not null)
        //        ElementCompositionPreview.SetElementChildVisual(shadowElementBlack, myVisualBlack);
        //}

        public bool IsTopClippingPanelEnabled
        {
            get { return (bool)GetValue(IsTopClippingPanelEnabledProperty); }
            set { SetValue(IsTopClippingPanelEnabledProperty, value); }
        }
        public static readonly DependencyProperty IsTopClippingPanelEnabledProperty =
            DependencyProperty.Register("IsTopClippingPanelEnabled", typeof(bool), typeof(NeumorphCard), new PropertyMetadata(false));


        public string HeaderLabel
        {
            get { return (string)GetValue(HeaderLabelProperty); }
            set { SetValue(HeaderLabelProperty, value); }
        }
        public static readonly DependencyProperty HeaderLabelProperty =
            DependencyProperty.Register("HeaderLabel", typeof(string), typeof(NeumorphCard), new PropertyMetadata(string.Empty, OnHeaderLabelChanged));

        private static void OnHeaderLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NeumorphCard target = (NeumorphCard)d;

            if (target.headerLabel is null)
                return;

            if (string.IsNullOrEmpty(target.headerLabel.Text))
                target.headerLabel.Visibility = Visibility.Collapsed;
            else
                target.headerLabel.Visibility = Visibility.Visible;
        }
    }
}
