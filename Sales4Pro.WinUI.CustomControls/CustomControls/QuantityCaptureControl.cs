using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.System;
using Windows.UI;

namespace Sales4Pro.WinUI.CustomControls;

[TemplatePart(Name = "PART_MainGrid", Type = typeof(Grid))]
[TemplatePart(Name = "PART_DigitsContainer", Type = typeof(Grid))]
[TemplatePart(Name = "PART_StockQuantityTextBlock", Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_DigitsFontIcon", Type = typeof(FontIcon))]
[TemplatePart(Name = "PART_StockQuantityBorder", Type = typeof(Border))]
public sealed class QuantityCaptureControl : TextBox
{
    public event EventHandler<int> QuantityChanged;

    private string oldText = string.Empty;
    private TextBlock stockQuantityTextBlock;
    private Border stockQuantityBorder;
    private FontIcon digitsFontIcon;
    private Grid digitsContainer;
    private bool isCtrlKeyPressed = false;

    public QuantityCaptureControl()
    {
        this.DefaultStyleKey = typeof(QuantityCaptureControl);
        MaxLength = 3;
        this.Loaded += QuantityCaptureControl_Loaded;
        this.RegisterPropertyChangedCallback(QuantityCaptureControl.MaxLengthProperty, OnMaxLengthChanged);
    }

    ~QuantityCaptureControl()
    { }

    private void QuantityCaptureControl_Loaded(object sender, RoutedEventArgs e)
    {
        this.TextChanging -= QuantityCaptureControl_TextChanging;
        this.TextChanging += QuantityCaptureControl_TextChanging;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        // ----- Exit initialisation here in DesignMode  ------------------------
        if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            return;
        // ----------------------------------------------------------------------

        stockQuantityBorder = this.GetTemplateChild("PART_StockQuantityBorder") as Border;
        stockQuantityTextBlock = this.GetTemplateChild("PART_StockQuantityTextBlock") as TextBlock;
        digitsFontIcon = this.GetTemplateChild("PART_DigitsFontIcon") as FontIcon;
        digitsContainer = this.GetTemplateChild("PART_DigitsContainer") as Grid;

        if (stockQuantityTextBlock is not null && stockQuantityBorder is not null)
        {
            if (IsStockVisible)
            {
                stockQuantityBorder.Visibility = Visibility.Visible;

                // Backgroundfarbe der Bestände setzen
                if (StockQuantity <= 0)
                    stockQuantityBorder.Background = new SolidColorBrush(Color.FromArgb(128, 232, 0, 0)); // Rot
                else if (StockQuantity > 0 && StockQuantity <= 10)
                    stockQuantityBorder.Background = new SolidColorBrush(Color.FromArgb(128, 232, 232, 0)); // Gelb
                else
                    stockQuantityBorder.Background = new SolidColorBrush(Color.FromArgb(64, 0, 232, 0)); // Grün

                // Backgroundfarbe der Bestände setzen
                if (StockQuantity <= 0)
                    stockQuantityTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
                else if (StockQuantity > 0 && StockQuantity <= 10)
                    stockQuantityTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                else
                    stockQuantityTextBlock.Foreground = new SolidColorBrush(Colors.Black);


                stockQuantityTextBlock.Text = StockQuantity == 0 ? "0" : StockQuantity.ToString();

                if (IsStockVisible)
                    stockQuantityTextBlock.Visibility = Visibility.Visible;
                else
                    stockQuantityTextBlock.Visibility = Visibility.Collapsed;


                if (StockQuantity == 0)
                    if (MaxQuantity == 999)
                        IsEnabled = true;
                    else
                        IsEnabled = false;
                else
                    IsEnabled = true;

            }
            else
            {
                stockQuantityBorder.Visibility = Visibility.Collapsed;
            }
        }

        if (digitsContainer is not null)
        {
            if (IsDigitsVisible)
                digitsContainer.Visibility = Visibility.Visible;
            else
                digitsContainer.Visibility = Visibility.Collapsed;
        }

        UpdateDigitsFontIcon();
    }

    #region Protected Overrides

    protected override void OnKeyDown(KeyRoutedEventArgs e)
    {
        base.OnKeyDown(e);

        if (e.Key == VirtualKey.Control) isCtrlKeyPressed = true;
    }

    protected override void OnKeyUp(KeyRoutedEventArgs e)
    {
        base.OnKeyUp(e);

        if (e.Key == VirtualKey.Control)
        {
            isCtrlKeyPressed = false;
            return;
        }

        // Es wurde eine Kombination aus CTR und Taste gedrückt
        // Somit wird der Wert nicht geändert
        if (isCtrlKeyPressed)
            return;

        if (e.Key == VirtualKey.Enter)
        {
            FocusManager.TryMoveFocus(FocusNavigationDirection.Next);

            // Make sure to set the Handled to true, otherwise the RoutedEvent might fire twice
            e.Handled = true;
        }
        else if (e.Key == VirtualKey.Number0 || e.Key == VirtualKey.NumberPad0 ||
                 e.Key == VirtualKey.Number1 || e.Key == VirtualKey.NumberPad1 ||
                 e.Key == VirtualKey.Number2 || e.Key == VirtualKey.NumberPad2 ||
                 e.Key == VirtualKey.Number3 || e.Key == VirtualKey.NumberPad3 ||
                 e.Key == VirtualKey.Number4 || e.Key == VirtualKey.NumberPad4 ||
                 e.Key == VirtualKey.Number5 || e.Key == VirtualKey.NumberPad5 ||
                 e.Key == VirtualKey.Number6 || e.Key == VirtualKey.NumberPad6 ||
                 e.Key == VirtualKey.Number7 || e.Key == VirtualKey.NumberPad7 ||
                 e.Key == VirtualKey.Number8 || e.Key == VirtualKey.NumberPad8 ||
                 e.Key == VirtualKey.Number9 || e.Key == VirtualKey.NumberPad9)
        {
            if (Text.Length >= MaxLength)
                FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
        }
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        oldText = Text;
        base.OnGotFocus(e);
        SelectAll();
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        int newQuantity = 0;

        if (Text == oldText)
            return;

        if (Text == string.Empty || Text == "0" || Text == "00" || Text == "000")
        {
            Text = string.Empty;
        }
        else
        {
            if (Text.StartsWith("0"))
            {
                char[] c = new char[] { '0' };
                Text = Text.TrimStart(c);
            }
            newQuantity = Convert.ToInt32(Text);
        }

        if (newQuantity > MaxQuantity)
            newQuantity = MaxQuantity;

        Quantity = newQuantity;
        QuantityChanged?.Invoke(this, newQuantity);

        WriteText(newQuantity);
    }

    #endregion

    #region Dependency Properties

    public int Quantity
    {
        get { return (int)GetValue(QuantityProperty); }
        set { SetValue(QuantityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for InkStrokeColor.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty QuantityProperty =
        DependencyProperty.Register("Quantity", typeof(int), typeof(QuantityCaptureControl), new PropertyMetadata(0, OnQuantityChanged));

    private static void OnQuantityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        QuantityCaptureControl target = (QuantityCaptureControl)d;
        target.WriteText((int)e.NewValue);
    }

    public int VPE
    {
        get { return (int)GetValue(VPEProperty); }
        set { SetValue(VPEProperty, value); }
    }

    // Using a DependencyProperty as the backing store for VPE.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty VPEProperty =
        DependencyProperty.Register("VPE", typeof(int), typeof(QuantityCaptureControl), new PropertyMetadata(1));

    public int StockQuantity
    {
        get { return (int)GetValue(StockQuantityProperty); }
        set { SetValue(StockQuantityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for InkStrokeColor.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StockQuantityProperty =
        DependencyProperty.Register("StockQuantity", typeof(int), typeof(QuantityCaptureControl), new PropertyMetadata(0, OnStockQuantityChanged));

    private static void OnStockQuantityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        QuantityCaptureControl target = (QuantityCaptureControl)d;
        if (target.stockQuantityTextBlock is not null)
        {
            // Backgroundfarbe der Bestände setzen
            if ((int)e.NewValue <= 0)
                target.stockQuantityBorder.Background = new SolidColorBrush(Color.FromArgb(128, 232, 0, 0)); // Rot
            else if ((int)e.NewValue > 0 && (int)e.NewValue <= 10)
                target.stockQuantityBorder.Background = new SolidColorBrush(Color.FromArgb(128, 232, 232, 0)); // Gelb
            else
                target.stockQuantityBorder.Background = new SolidColorBrush(Color.FromArgb(64, 0, 232, 0)); // Grün

            // Backgroundfarbe der Bestände setzen
            if ((int)e.NewValue <= 0)
                target.stockQuantityTextBlock.Foreground = new SolidColorBrush(Color.FromArgb(128, 255, 255, 255));
            else if ((int)e.NewValue > 0 && (int)e.NewValue <= 10)
                target.stockQuantityTextBlock.Foreground = new SolidColorBrush(Colors.Black);
            else
                target.stockQuantityTextBlock.Foreground = new SolidColorBrush(Colors.Black);

            // Wert des Bestandes setzen
            if ((int)e.NewValue == 0)
                target.stockQuantityTextBlock.Text = "0";
            else
                target.stockQuantityTextBlock.Text = e.NewValue.ToString();

            if ((int)e.NewValue == 0)
                if (target.MaxQuantity == 999)
                    target.IsEnabled = true;
                else
                    target.IsEnabled = false;
            else
                target.IsEnabled = true;

        }
    }



    public bool IsStockVisible
    {
        get { return (bool)GetValue(IsStockVisibleProperty); }
        set { SetValue(IsStockVisibleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsStockVisible.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsStockVisibleProperty =
        DependencyProperty.Register("IsStockVisible", typeof(bool), typeof(QuantityCaptureControl), new PropertyMetadata(true, OnIsStockVisibleChanged));

    private static void OnIsStockVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        QuantityCaptureControl target = (QuantityCaptureControl)d;
        if (target.stockQuantityTextBlock is null || target.stockQuantityBorder is null)
            return;

        if ((bool)e.NewValue)
        {
            target.stockQuantityTextBlock.Visibility = Visibility.Visible;
            target.stockQuantityBorder.Visibility = Visibility.Visible;
        }
        else
        {
            target.stockQuantityTextBlock.Visibility = Visibility.Collapsed;
            target.stockQuantityBorder.Visibility = Visibility.Collapsed;
        }
    }




    public bool IsHeaderVisible
    {
        get { return (bool)GetValue(IsHeaderVisibleProperty); }
        set { SetValue(IsHeaderVisibleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsStockVisible.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsHeaderVisibleProperty =
        DependencyProperty.Register("IsHeaderVisible", typeof(bool), typeof(QuantityCaptureControl), new PropertyMetadata(true));


    public bool IsDigitsVisible
    {
        get { return (bool)GetValue(IsDigitsVisibleProperty); }
        set { SetValue(IsDigitsVisibleProperty, value); }
    }

    // Using a DependencyProperty as the backing store for IsStockVisible.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IsDigitsVisibleProperty =
        DependencyProperty.Register("IsDigitsVisible", typeof(bool), typeof(QuantityCaptureControl), new PropertyMetadata(true, OnIsDigitsVisibleChanged));

    private static void OnIsDigitsVisibleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        QuantityCaptureControl target = (QuantityCaptureControl)d;
        if (target.digitsContainer is null)
            return;

        if ((bool)e.NewValue)
            target.digitsContainer.Visibility = Visibility.Visible;
        else
            target.digitsContainer.Visibility = Visibility.Collapsed;
    }

    public int MaxQuantity
    {
        get { return (int)GetValue(MaxQuantityProperty); }
        set { SetValue(MaxQuantityProperty, value); }
    }

    // Using a DependencyProperty as the backing store for MaxQuantity.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty MaxQuantityProperty =
        DependencyProperty.Register("MaxQuantity", typeof(int), typeof(QuantityCaptureControl), new PropertyMetadata(999, OnMaxQuantityChanged));

    private static void OnMaxQuantityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        QuantityCaptureControl target = (QuantityCaptureControl)d;

        if ((int)e.NewValue == 999)
            target.IsEnabled = true;
        else
            target.IsEnabled = true;
    }

    #endregion

    #region Events

    private void OnMaxLengthChanged(DependencyObject sender, DependencyProperty dp)
    {
        UpdateDigitsFontIcon();
    }

    private void QuantityCaptureControl_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
    {
        //// Entferne alle Zeichen, wenn es keine Zahlen sind
        //if (!Regex.IsMatch(sender.Text, "^[0-9]+$") && sender.Text != "")
        //{
        //    // Es ist mindestens ein Zeichen im Text, das keine Zahl ist
        //    int pos = sender.SelectionStart - 1;
        //    sender.Text = sender.Text.Remove(pos, 1);
        //    sender.SelectionStart = pos;
        //}
    }

    #endregion

    #region Methods

    private void WriteText(int newQuantity)
    {
        if (Text is not null)
        {
            if (newQuantity.ToString() == Text)
                return;

            if (newQuantity == 0)
                Text = string.Empty;
            else
                Text = newQuantity.ToString();
        }
    }

    private void UpdateDigitsFontIcon()
    {
        if (digitsFontIcon is null)
            return;

        if (MaxLength == 1)
            digitsFontIcon.Glyph = "\uE108";
        else if (MaxLength == 2)
            digitsFontIcon.Glyph = "\uE108 \uE108 ";
        else if (MaxLength == 3)
            digitsFontIcon.Glyph = "\uE108 \uE108 \uE108";
        else if (MaxLength == 4)
            digitsFontIcon.Glyph = "\uE108 \uE108 \uE108 \uE108";
        else
            digitsFontIcon.Glyph = "\uE10C";
    }

    #endregion

}
