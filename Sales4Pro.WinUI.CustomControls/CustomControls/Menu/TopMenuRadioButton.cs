using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System;
using Windows.UI;

namespace Sales4Pro.WinUI.CustomControls.Menu
{
    public sealed class TopMenuRadioButton : RadioButton
    {
        bool isRadioButtonChecked = false;
        public event EventHandler Pressed;

        public TopMenuRadioButton()
        {
            this.DefaultStyleKey = typeof(TopMenuRadioButton);

            SetValue(RadioButton.GroupNameProperty, "TabBar");
            SetValue(RadioButton.IsCheckedProperty, false);
            Background = new SolidColorBrush(Color.FromArgb(255, 00, 250, 0));

            Loaded += TopMenuRadioButton_Loaded;
            Unloaded += TopMenuRadioButton_Unloaded;
        }

        ~TopMenuRadioButton()
        { }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #region EventHandler

        private void TopMenuRadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            Checked += TopMenuRadioButton_Checked;
            Unchecked += TopMenuRadioButton_Unchecked;
            Click += TopMenuRadioButton_Click;

            if (IsHot)
                VisualStateManager.GoToState(this, "Hot", true);
            else
                VisualStateManager.GoToState(this, "NotHot", true);
        }

        private void TopMenuRadioButton_Unloaded(object sender, RoutedEventArgs e)
        {
            Checked -= TopMenuRadioButton_Checked;
            Unchecked -= TopMenuRadioButton_Unchecked;
            Click -= TopMenuRadioButton_Click;
        }

        protected override void OnPointerPressed(PointerRoutedEventArgs e)
        {
            if (IsTogglingEnabled == false)
            {
                Pressed?.Invoke(this, new EventArgs());
                e.Handled = true;
            }
            base.OnPointerPressed(e);
        }

        private void TopMenuRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if ((IsChecked).Value && !isRadioButtonChecked)
                IsChecked = false;
            else
            {
                IsChecked = true;
                isRadioButtonChecked = false;
            }
        }

        private void TopMenuRadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            isRadioButtonChecked = false;
        }

        private void TopMenuRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            isRadioButtonChecked = true;
        }

        #endregion

        #region Dependency Properties

        public enum TypeEnum { None, Start, Insert, Collections, Review, View, Sketching }
        public TypeEnum Type
        {
            get { return (TypeEnum)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(TypeEnum), typeof(TopMenuRadioButton), new PropertyMetadata(TypeEnum.Start));

        public bool IsTogglingEnabled
        {
            get { return (bool)GetValue(IsTogglingEnabledProperty); }
            set { SetValue(IsTogglingEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsTogglingEnabledProperty =
            DependencyProperty.Register("IsTogglingEnabled", typeof(bool), typeof(TopMenuRadioButton), new PropertyMetadata(true));


        public bool IsHot
        {
            get { return (bool)GetValue(IsHotProperty); }
            set { SetValue(IsHotProperty, value); }
        }

        public static readonly DependencyProperty IsHotProperty =
            DependencyProperty.Register("IsHot", typeof(bool), typeof(TopMenuRadioButton), new PropertyMetadata(false, IsHotChanged));

        private static void IsHotChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TopMenuRadioButton target = d as TopMenuRadioButton;
            if ((bool)e.NewValue)
                VisualStateManager.GoToState(target, "Hot", true);
            else
                VisualStateManager.GoToState(target, "NotHot", true);
        }


        public string Label2
        {
            get { return (string)GetValue(Label2Property); }
            set { SetValue(Label2Property, value); }
        }

        public static readonly DependencyProperty Label2Property =
            DependencyProperty.Register("Label2", typeof(string), typeof(TopMenuRadioButton), new PropertyMetadata("Label 2"));

        #endregion

    }
}
