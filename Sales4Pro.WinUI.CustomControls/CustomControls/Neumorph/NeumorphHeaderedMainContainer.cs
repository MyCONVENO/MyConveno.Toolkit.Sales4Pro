using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;

namespace Sales4Pro.WinUI.CustomControls
{
    [TemplatePart(Name = "PART_BackButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_MainContentPresenter", Type = typeof(ContentPresenter))]

    public sealed class NeumorphHeaderedMainContainer : ContentControl
    {
        public event EventHandler GoBackClicked;
        public event EventHandler CloseMe;
        private Button backButton;
        private Button closeButton;
        private ContentPresenter mainContentPresenter;

        public NeumorphHeaderedMainContainer()
        {
            this.DefaultStyleKey = typeof(NeumorphHeaderedMainContainer);

            Loaded -= NeumorphGridPanel_Loaded;
            Loaded += NeumorphGridPanel_Loaded;
            Unloaded -= NeumorphGridPanel_Unloaded;
            Unloaded += NeumorphGridPanel_Unloaded;
        }

        private void NeumorphGridPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (backButton is not null)
            {
                backButton.Click -= BackButton_Click;
                backButton.Click += BackButton_Click;
            }

            if (closeButton is not null)
            {
                closeButton.Click -= CloseButton_Click;
                closeButton.Click += CloseButton_Click;
            }
        }

        private void NeumorphGridPanel_Unloaded(object sender, RoutedEventArgs e)
        {
            if (backButton is not null)
                backButton.Click -= BackButton_Click;

            if (closeButton is not null)
                closeButton.Click -= CloseButton_Click;
        }

        ~NeumorphHeaderedMainContainer()
        { }

        protected override void OnApplyTemplate()
        {
            backButton = (Button)GetTemplateChild("PART_BackButton");
            closeButton = (Button)GetTemplateChild("PART_CloseButton");
            mainContentPresenter = (ContentPresenter)GetTemplateChild("PART_MainContentPresenter");

            base.OnApplyTemplate();

            SetContentIntoTitleBar();
        }

        #region DependencyProperties

        public bool ExtendsContentIntoTitleBar
        {
            get { return (bool)GetValue(ExtendsContentIntoTitleBarProperty); }
            set { SetValue(ExtendsContentIntoTitleBarProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtendsContentIntoTitleBarProperty =
            DependencyProperty.Register("ExtendsContentIntoTitleBar", typeof(bool), typeof(NeumorphHeaderedMainContainer), new PropertyMetadata(false, OnExtendsContentIntoTitleBarChanged));

        private static void OnExtendsContentIntoTitleBarChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            NeumorphHeaderedMainContainer target = (NeumorphHeaderedMainContainer)d;
            if (target is not null && target.mainContentPresenter is not null )
            {
                target.SetContentIntoTitleBar();
            }
        }


        public Visibility BackButtonVisibility
        {
            get { return (Visibility)GetValue(BackButtonVisibilityProperty); }
            set { SetValue(BackButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackButtonVisibilityProperty =
            DependencyProperty.Register("BackButtonVisibility", typeof(Visibility), typeof(NeumorphHeaderedMainContainer), new PropertyMetadata(Visibility.Collapsed));


        public bool BackButtonIsEnabled
        {
            get { return (bool)GetValue(BackButtonIsEnabledProperty); }
            set { SetValue(BackButtonIsEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackButtonIsEnabledProperty =
            DependencyProperty.Register("BackButtonIsEnabled", typeof(bool), typeof(NeumorphHeaderedMainContainer), new PropertyMetadata(true));


        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CloseButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CloseButtonVisibilityProperty =
            DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(NeumorphHeaderedMainContainer), new PropertyMetadata(Visibility.Visible));


        public string HeaderLabel
        {
            get { return (string)GetValue(HeaderLabelProperty); }
            set { SetValue(HeaderLabelProperty, value); }
        }
        public static readonly DependencyProperty HeaderLabelProperty =
            DependencyProperty.Register("HeaderLabel", typeof(string), typeof(NeumorphHeaderedMainContainer), new PropertyMetadata("HeaderLabel"));


        public ImageSource HeaderImageSource
        {
            get { return (ImageSource)GetValue(HeaderImageSourceProperty); }
            set { SetValue(HeaderImageSourceProperty, value); }
        }
        public static readonly DependencyProperty HeaderImageSourceProperty =
            DependencyProperty.Register("HeaderImageSource", typeof(ImageSource), typeof(NeumorphHeaderedMainContainer), new PropertyMetadata(null));


        #endregion

        #region EventHandler

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GoBackClicked?.Invoke(this, EventArgs.Empty);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseMe?.Invoke(this, EventArgs.Empty);
        }

        #endregion


        #region Commands

        private void SetContentIntoTitleBar()
        {
            if (ExtendsContentIntoTitleBar)
            {
                Grid.SetRow(mainContentPresenter, 0);
                Grid.SetRowSpan(mainContentPresenter, 2);
            }
            else
            {
                Grid.SetRow(mainContentPresenter, 1);
                Grid.SetRowSpan(mainContentPresenter, 1);
            }
        }

        #endregion
    }
}
