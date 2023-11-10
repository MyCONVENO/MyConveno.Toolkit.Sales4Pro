using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_AddRepeatButton", Type = typeof(RepeatButton))]
[TemplatePart(Name = "PART_AddBigStepRepeatButton", Type = typeof(RepeatButton))]
[TemplatePart(Name = "PART_SubtractRepeatButton", Type = typeof(RepeatButton))]
[TemplatePart(Name = "PART_ResetButton", Type = typeof(Button))]
[TemplatePart(Name = "PART_HamburgerFontIcon", Type = typeof(FontIcon))]
[TemplatePart(Name = "PART_ValueTextBox", Type = typeof(TextBox))]
public sealed class TouchDoubleUpDownPresenterControl : Control
{
    private RepeatButton addRepeatButton;
    private RepeatButton addBigStepRepeatButton;
    private RepeatButton subtractRepeatButton;
    private Button resetButton;
    private FontIcon hamburgerFontIcon;
    private TextBox valueTextBox;
    private bool isInEditMode = false;

    public event EventHandler LostFocusWithChanges;
    public event EventHandler ValueChanged;

    public TouchDoubleUpDownPresenterControl()
    {
        this.DefaultStyleKey = typeof(TouchDoubleUpDownPresenterControl);

        Loaded += TouchDoubleUpDownPresenterControl_Loaded;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        addRepeatButton = (RepeatButton)GetTemplateChild("PART_AddRepeatButton");
        addBigStepRepeatButton = (RepeatButton)GetTemplateChild("PART_AddBigStepRepeatButton");
        subtractRepeatButton = (RepeatButton)GetTemplateChild("PART_SubtractRepeatButton");
        resetButton = (Button)GetTemplateChild("PART_ResetButton");
        hamburgerFontIcon = (FontIcon)GetTemplateChild("PART_HamburgerFontIcon");
        valueTextBox = (TextBox)GetTemplateChild("PART_ValueTextBox");

        if (hamburgerFontIcon != null)
            hamburgerFontIcon.Visibility = HamburgerIconVisibility;

        if (addRepeatButton != null)
        {
            addRepeatButton.Click -= AddRepeatButton_Click;
            addRepeatButton.Click += AddRepeatButton_Click;
        }

        if (addBigStepRepeatButton != null)
        {
            addBigStepRepeatButton.Click -= AddBigStepRepeatButton_Click;
            addBigStepRepeatButton.Click += AddBigStepRepeatButton_Click;
        }

        if (subtractRepeatButton != null)
        {
            subtractRepeatButton.Click -= SubtractRepeatButton_Click;
            subtractRepeatButton.Click += SubtractRepeatButton_Click;
        }

        if (resetButton != null)
        {
            resetButton.Click -= ResetButton_Click;
            resetButton.Click += ResetButton_Click;
            resetButton.Visibility = ResetButtonVisibility;
        }

        if (hamburgerFontIcon != null)
        {
            hamburgerFontIcon.Visibility = HamburgerIconVisibility;
        }
    }

    private void TouchDoubleUpDownPresenterControl_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateValueTextBox();
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        Task.Delay(5);
        if (isInEditMode)
        {
            // Debug.WriteLine("LostFocusWithChanges " + this.Value.ToString());
            LostFocusWithChanges?.Invoke(this, new EventArgs());
        }
        isInEditMode = false;
    }

    private void ResetButton_Click(object sender, RoutedEventArgs e)
    {
        isInEditMode = true;
        Value = InitialValue;
    }

    private void AddRepeatButton_Click(object sender, RoutedEventArgs e)
    {
        if (Value + Step <= MaxValue)
        {
            isInEditMode = true;
            Value += Step;
        }
    }

    private void AddBigStepRepeatButton_Click(object sender, RoutedEventArgs e)
    {
        if (Value + BigStep <= MaxValue)
        {
            isInEditMode = true;
            Value += BigStep;
        }
    }

    private void SubtractRepeatButton_Click(object sender, RoutedEventArgs e)
    {
        if (Value - Step >= MinValue)
        {
            isInEditMode = true;
            Value -= Step;
        }
    }

    private void UpdateValueTextBox()
    {
        if (valueTextBox != null)
        {
            switch (Digits)
            {
                case digitsEnum.D0:
                    valueTextBox.Text = (String.Format(CultureInfo.InvariantCulture, IsPercentFormat ? "{0:0%}" : "{0:0}", Value));
                    break;
                case digitsEnum.D1:
                    valueTextBox.Text = (String.Format(CultureInfo.InvariantCulture, IsPercentFormat ? "{0:0.0%}" : "{0:0.0}", Value));
                    break;
                case digitsEnum.D2:
                    valueTextBox.Text = (String.Format(CultureInfo.InvariantCulture, IsPercentFormat ? "{0:0.00%}" : "{0:0.00}", Value));
                    break;
                default:
                    break;
            }
        }
    }

    #region Dependency Properties

    public string Header
    {
        get { return (string)GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata("Header"));

    public double Value
    {
        get { return (double)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, Math.Round(value, 3)); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(double), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(0.00, OnValueChanged));

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TouchDoubleUpDownPresenterControl target = (TouchDoubleUpDownPresenterControl)d;
        target.UpdateValueTextBox();
        target.ValueChanged?.Invoke(target, new EventArgs());
    }

    public enum digitsEnum { D0, D1, D2 }
    public digitsEnum Digits
    {
        get { return (digitsEnum)GetValue(DigitsProperty); }
        set { SetValue(DigitsProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsHot.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DigitsProperty =
        DependencyProperty.Register("Digits", typeof(digitsEnum), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(digitsEnum.D1, OnDigitsChanged));

    private static void OnDigitsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TouchDoubleUpDownPresenterControl target = (TouchDoubleUpDownPresenterControl)d;
        target.UpdateValueTextBox();
    }

    public double Step
    {
        get { return (double)GetValue(StepProperty); }
        set { SetValue(StepProperty, Math.Round(value, 3)); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StepProperty =
        DependencyProperty.Register("Step", typeof(double), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(0.001));

    public double BigStep
    {
        get { return (double)GetValue(BigStepProperty); }
        set { SetValue(BigStepProperty, Math.Round(value, 3)); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty BigStepProperty =
        DependencyProperty.Register("BigStep", typeof(double), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(0.01));

    public double MinValue
    {
        get { return (double)GetValue(MinValueProperty); }
        set { SetValue(MinValueProperty, Math.Round(value, 3)); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MinValueProperty =
        DependencyProperty.Register("MinValue", typeof(double), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(0.00));

    public double MaxValue
    {
        get { return (double)GetValue(MaxValueProperty); }
        set { SetValue(MaxValueProperty, Math.Round(value, 3)); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register("MaxValue", typeof(double), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(1.00));


    public double InitialValue
    {
        get { return (double)GetValue(InitialValueProperty); }
        set { SetValue(InitialValueProperty, Math.Round(value, 3)); }
    }

    // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty InitialValueProperty =
        DependencyProperty.Register("InitialValue", typeof(double), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(1.00));

    public bool IsPercentFormat
    {
        get { return (bool)GetValue(IsPercentFormatProperty); }
        set { SetValue(IsPercentFormatProperty, value); }
    }

    // Using a DependencyProperty as the backing store for HamburgerButtonVisibility.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsPercentFormatProperty =
        DependencyProperty.Register("IsPercentFormat", typeof(bool), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(true, IsPercentFormatChanged));

    private static void IsPercentFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TouchDoubleUpDownPresenterControl target = (TouchDoubleUpDownPresenterControl)d;
        target.UpdateValueTextBox();
    }

    public Visibility HamburgerIconVisibility
    {
        get { return (Visibility)GetValue(HamburgerIconVisibilityProperty); }
        set { SetValue(HamburgerIconVisibilityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for HamburgerButtonVisibility.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty HamburgerIconVisibilityProperty =
        DependencyProperty.Register("HamburgerIconVisibility", typeof(Visibility), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(Visibility.Visible, HamburgerIconVisibilityChanged));

    private static void HamburgerIconVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TouchDoubleUpDownPresenterControl target = (TouchDoubleUpDownPresenterControl)d;

        if (target.hamburgerFontIcon != null)
            target.hamburgerFontIcon.Visibility = (Visibility)e.NewValue;
    }

    public Visibility ResetButtonVisibility
    {
        get { return (Visibility)GetValue(ResetButtonVisibilityProperty); }
        set { SetValue(ResetButtonVisibilityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for HamburgerButtonVisibility.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ResetButtonVisibilityProperty =
        DependencyProperty.Register("ResetButtonVisibility", typeof(Visibility), typeof(TouchDoubleUpDownPresenterControl), new PropertyMetadata(Visibility.Visible, ResetButtonVisibilityChanged));

    private static void ResetButtonVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        TouchDoubleUpDownPresenterControl target = (TouchDoubleUpDownPresenterControl)d;

        if (target.hamburgerFontIcon != null)
            target.hamburgerFontIcon.Visibility = (Visibility)e.NewValue;
    }

    #endregion

}
