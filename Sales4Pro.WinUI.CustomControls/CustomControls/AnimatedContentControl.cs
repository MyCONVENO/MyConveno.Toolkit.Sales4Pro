using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using Windows.Foundation;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_ContentPresenter", Type = typeof(Grid))]
[TemplatePart(Name = "PART_ShadowElement", Type = typeof(Border))]
public sealed class AnimatedContentControl : ContentControl
{
    private Grid contentPresenter;
    int currentIndex = 0;

    public AnimatedContentControl()
    {
        this.DefaultStyleKey = typeof(AnimatedContentControl);
        ContentWidth = 0;
        ContentHeight = 0;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        contentPresenter = (Grid)GetTemplateChild("PART_ContentPresenter");

        switch (Dock)
        {
            case DockEnum.Left:
                VerticalContentAlignment = VerticalAlignment.Stretch;
                HorizontalContentAlignment = HorizontalAlignment.Left;
                break;
            case DockEnum.Right:
                VerticalContentAlignment = VerticalAlignment.Stretch;
                HorizontalContentAlignment = HorizontalAlignment.Right;
                break;
            case DockEnum.Top:
                VerticalContentAlignment = VerticalAlignment.Top;
                HorizontalContentAlignment = HorizontalAlignment.Stretch;
                break;
            case DockEnum.Bottom:
                VerticalContentAlignment = VerticalAlignment.Bottom;
                HorizontalContentAlignment = HorizontalAlignment.Stretch;
                break;
            default:
                break;
        }

        currentIndex = (int)GetValue(Canvas.ZIndexProperty);

        SetWidthHeight();
        TranslateToSourcePosition(RenderSize);
        AnimateToTargetPosition(false);
        UpdateLeftOffset();
    }

    #region DependencyProperties

    public enum DockEnum { Left, Top, Right, Bottom }
    public DockEnum Dock
    {
        get { return (DockEnum)GetValue(DockProperty); }
        set { SetValue(DockProperty, value); }
    }
    public static readonly DependencyProperty DockProperty =
        DependencyProperty.Register("Dock", typeof(DockEnum), typeof(AnimatedContentControl), new PropertyMetadata(DockEnum.Right, OnDockChanged));

    private static void OnDockChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedContentControl target = (AnimatedContentControl)d;

        switch ((DockEnum)e.NewValue)
        {
            case DockEnum.Left:
                target.VerticalContentAlignment = VerticalAlignment.Stretch;
                target.HorizontalContentAlignment = HorizontalAlignment.Left;
                break;
            case DockEnum.Right:
                target.VerticalContentAlignment = VerticalAlignment.Stretch;
                target.HorizontalContentAlignment = HorizontalAlignment.Right;
                break;
            case DockEnum.Top:
                target.VerticalContentAlignment = VerticalAlignment.Top;
                target.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                break;
            case DockEnum.Bottom:
                target.VerticalContentAlignment = VerticalAlignment.Bottom;
                target.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                break;
            default:
                break;
        }
    }

    public bool IsVisible
    {
        get { return (bool)GetValue(IsVisibleProperty); }
        set { SetValue(IsVisibleProperty, value); }
    }
    public static readonly DependencyProperty IsVisibleProperty =
        DependencyProperty.Register("IsVisible", typeof(bool), typeof(AnimatedContentControl), new PropertyMetadata(false, IsVisibleChanged));

    private static void IsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedContentControl target = (AnimatedContentControl)d;

        if ((bool)e.NewValue)
        {
            target.TranslateToSourcePosition(target.RenderSize);
            target.AnimateToTargetPosition(true);
        }
        else
        {
            RelativePanel rp = FindParent<RelativePanel>(target);

            if (rp is null)
                return;

            // Setze nur meinen Z-Index nach hinten
            foreach (UIElement uielement in rp.Children)
            {
                if (uielement.GetType() == target.GetType() && uielement == target)
                    target.SetValue(Canvas.ZIndexProperty, -1000);
            }

            target.TranslateToSourcePosition(target.RenderSize);
            target.AnimateToTargetPosition(true);
        }
    }

    public static T FindParent<T>(DependencyObject child) where T : DependencyObject
    {
        //get parent item
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        //we've reached the end of the tree
        if (parentObject is null) return null;

        //check if the parent matches the type we're looking for
        T parent = parentObject as T;
        if (parent is not null)
            return parent;
        else
            return FindParent<T>(parentObject);
    }

    public bool IsSticky
    {
        get { return (bool)GetValue(IsStickyProperty); }
        set { SetValue(IsStickyProperty, value); }
    }
    public static readonly DependencyProperty IsStickyProperty =
        DependencyProperty.Register("IsSticky", typeof(bool), typeof(AnimatedContentControl), new PropertyMetadata(false, OnIsStickyChanged));

    private static void OnIsStickyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        AnimatedContentControl target = (AnimatedContentControl)d;
        target.UpdateLeftOffset();
    }

    public double RightOffset
    {
        get { return (double)GetValue(RightOffsetProperty); }
        set { SetValue(RightOffsetProperty, value); }
    }
    public static readonly DependencyProperty RightOffsetProperty =
        DependencyProperty.Register("RightOffset", typeof(double), typeof(AnimatedContentControl), new PropertyMetadata(0.0));

    public double ContentWidth
    {
        get { return (double)GetValue(ContentWidthProperty); }
        set { SetValue(ContentWidthProperty, value); }
    }
    public static readonly DependencyProperty ContentWidthProperty =
        DependencyProperty.Register("ContentWidth", typeof(double), typeof(AnimatedContentControl), new PropertyMetadata(0));

    public double ContentHeight
    {
        get { return (double)GetValue(ContentHeightProperty); }
        set { SetValue(ContentHeightProperty, value); }
    }
    public static readonly DependencyProperty ContentHeightProperty =
        DependencyProperty.Register("ContentHeight", typeof(double), typeof(AnimatedContentControl), new PropertyMetadata(0));

    public bool IsCompact
    {
        get { return (bool)GetValue(IsCompactProperty); }
        set { SetValue(IsCompactProperty, value); }
    }
    public static readonly DependencyProperty IsCompactProperty =
        DependencyProperty.Register("IsCompact", typeof(bool), typeof(AnimatedContentControl), new PropertyMetadata(false));

    public double CompactPaneLength
    {
        get { return (double)GetValue(CompactPaneLengthProperty); }
        set { SetValue(CompactPaneLengthProperty, value); }
    }
    public static readonly DependencyProperty CompactPaneLengthProperty =
        DependencyProperty.Register("CompactPaneLength", typeof(double), typeof(AnimatedContentControl), new PropertyMetadata(48.0));

    public double CompactPaneHeight
    {
        get { return (double)GetValue(CompactPaneHeightProperty); }
        set { SetValue(CompactPaneHeightProperty, value); }
    }
    public static readonly DependencyProperty CompactPaneHeightProperty =
        DependencyProperty.Register("CompactPaneHeight", typeof(double), typeof(AnimatedContentControl), new PropertyMetadata(48.0));

    #endregion

    #region Methods

    private void TranslateToSourcePosition(Size newSize)
    {
        Visibility = Visibility.Visible;
        SetWidthHeight();

        if (contentPresenter is null)
            return;

        double compactPaneHeight = IsCompact ? CompactPaneHeight : 0;
        double compactPaneLength = IsCompact ? CompactPaneLength : 0;

        CompositeTransform ct = (CompositeTransform)contentPresenter.RenderTransform;

        switch (Dock)
        {
            case DockEnum.Left:
                if (IsVisible)
                {
                    contentPresenter.Margin = new Thickness(0);
                    ct.TranslateX = -ContentWidth + compactPaneLength;
                }
                else
                {
                    contentPresenter.Margin = new Thickness(0, 0, -ContentWidth + compactPaneLength, 0);
                    ct.TranslateX = 0;
                }
                break;
            case DockEnum.Right:
                if (IsVisible)
                {
                    contentPresenter.Margin = new Thickness(0);
                    ct.TranslateX = ContentWidth - compactPaneLength;
                }
                else
                {
                    contentPresenter.Margin = new Thickness(-ContentWidth + compactPaneLength, 0, 0, 0);
                    ct.TranslateX = 0;
                }
                break;
            case DockEnum.Top:
                if (IsVisible)
                {
                    contentPresenter.Margin = new Thickness(0);
                    ct.TranslateY = -ContentHeight + compactPaneHeight;
                }
                else
                {
                    contentPresenter.Margin = new Thickness(0, 0, 0, -ContentHeight + compactPaneHeight);
                    ct.TranslateY = 0;
                }
                break;
            case DockEnum.Bottom:
                if (IsVisible)
                {
                    contentPresenter.Margin = new Thickness(0);
                    ct.TranslateY = ContentHeight;
                }
                else
                {
                    contentPresenter.Margin = new Thickness(0, -ContentHeight, 0, 0);
                    ct.TranslateY = 0;
                }
                break;
            default:
                break;
        }
    }

    private void SetWidthHeight()
    {
        double compactPaneHeight = IsCompact ? CompactPaneHeight : 0;
        double compactPaneLength = IsCompact ? CompactPaneLength : 0;

        switch (Dock)
        {
            case DockEnum.Left:
                Width = IsVisible ? ContentWidth : compactPaneLength;
                break;
            case DockEnum.Right:
                Width = IsVisible ? ContentWidth : compactPaneLength;
                break;
            case DockEnum.Top:
                Height = IsVisible ? ContentHeight : compactPaneHeight;
                break;
            case DockEnum.Bottom:
                Height = IsVisible ? ContentHeight : compactPaneHeight;
                break;
            default:
                break;
        }
    }

    private void AnimateToTargetPosition(bool animateTransition)
    {
        if (contentPresenter is null)
            return;

        double compactPaneHeight = IsCompact ? CompactPaneHeight : 0;
        double compactPaneLength = IsCompact ? CompactPaneLength : 0;

        Storyboard myStoryboard = new();
        myStoryboard.Completed += MyStoryboard_Completed;

        DoubleAnimation myDoubleAnimation = new();

        if (animateTransition)
            myDoubleAnimation.Duration = new Duration(new TimeSpan(0, 0, 0, 0, 1000));
        else
            myDoubleAnimation.Duration = new Duration(new TimeSpan(0));

        QuarticEase easingFunction = new()
        {
            EasingMode = EasingMode.EaseOut
        };
        myDoubleAnimation.EasingFunction = easingFunction;

        switch (Dock)
        {
            case DockEnum.Left:
                myDoubleAnimation.To = IsVisible ? 0 : -ContentWidth + compactPaneLength;
                Storyboard.SetTargetProperty(myDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
                break;
            case DockEnum.Right:
                myDoubleAnimation.To = IsVisible ? 0 : ContentWidth - compactPaneLength;
                Storyboard.SetTargetProperty(myDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateX)");
                break;
            case DockEnum.Top:
                myDoubleAnimation.To = IsVisible ? 0 : -ContentHeight + compactPaneHeight;
                Storyboard.SetTargetProperty(myDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
                break;
            case DockEnum.Bottom:
                myDoubleAnimation.To = IsVisible ? 0 : ContentHeight - compactPaneHeight;
                Storyboard.SetTargetProperty(myDoubleAnimation, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
                break;
            default:
                break;
        }

        Storyboard.SetTarget(myDoubleAnimation, contentPresenter);
        myStoryboard.Children.Add(myDoubleAnimation);
        myStoryboard.Begin();
    }

    private void UpdateLeftOffset()
    {
        if (IsSticky)
            RightOffset = ContentWidth / 2;
        else
            RightOffset = CompactPaneLength / 2;

    }

    #endregion

    #region Events

    private void MyStoryboard_Completed(object sender, object e)
    {
        // Setze die Visibility erst jetzt auf Collapsed, wenn das Control unsichtbar wird.
        if (!IsVisible && !IsCompact)
            Visibility = Visibility.Collapsed;

        SetValue(Canvas.ZIndexProperty, currentIndex);
    }

    #endregion

}
