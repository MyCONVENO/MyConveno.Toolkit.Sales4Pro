using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Threading.Tasks;

namespace Sales4Pro.WinUI.CustomControls;

public sealed class ExtendedTextBox : TextBox
{
    private bool isInEditMode = false;
    private string oldText = string.Empty;

    public event EventHandler LostFocusWithChanges;

    public ExtendedTextBox()
    {
        this.DefaultStyleKey = typeof(TextBox);
        IsSpellCheckEnabled = false;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
    }

    protected async override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);
        await Task.Delay(5);
        if (isInEditMode && oldText != Text || (Text.Length > 0 && Text.Length == MaxLength))
        {
            LostFocusWithChanges?.Invoke(this, new EventArgs());
        }
        isInEditMode = false;
    }

    protected override void OnKeyDown(KeyRoutedEventArgs e)
    {
        oldText = Text;
        isInEditMode = true;
        base.OnKeyDown(e);
    }

    protected override void OnPointerPressed(PointerRoutedEventArgs e)
    {
        oldText = Text;
        isInEditMode = true;
        base.OnPointerPressed(e);
    }

}
