using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.ObjectModel;
using Windows.Foundation;

namespace Sales4Pro.WinUI.CustomControls.Menu
{
    //[TemplatePart(Name = "PART_MiniToolbarBorder", Type = typeof(Border))]
    [TemplatePart(Name = "PART_TabMenu", Type = typeof(Border))]
    [TemplatePart(Name = "PART_TopFiller", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_BackButton", Type = typeof(Button))]
    //[TemplatePart(Name = "PART_TabItemsControl", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_AppBarPanel", Type = typeof(RelativePanel))]
    [TemplatePart(Name = "PART_AddButton", Type = typeof(Button))]
    //[TemplatePart(Name = "PART_LightLineBorder", Type = typeof(Border))]

    //[ContentProperty(Name = "Items")]
    public sealed class TabContainer : ContentControl
    {
        //private Border myMiniToolbarBorder;
        private RelativePanel appBarPanel;
        private Button backButton;
        private Rectangle topFiller;
        private ItemsControl tabItemsControl;
        private Border tabMenu;
        //private Border lightLineBorder;

        private readonly ObservableCollection<TopMenuRadioButton> _items;

        public event EventHandler BackClicked;
        //public event EventHandler AddClicked;

        public TabContainer()
        {
            DefaultStyleKey = typeof(TabContainer);

            Loaded += TabContainer_Loaded;
            Unloaded += TabContainer_Unloaded;

            _items = new ObservableCollection<TopMenuRadioButton>();
            _items.CollectionChanged += OnItemsCollectionChanged;

            SizeChanged -= TabContainer_SizeChanged;
            SizeChanged += TabContainer_SizeChanged;
        }

        ~TabContainer()
        { }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //myMiniToolbarBorder = (Border)GetTemplateChild("PART_MiniToolbarBorder");
            //lightLineBorder = (Border)GetTemplateChild("PART_LightLineBorder");
            appBarPanel = (RelativePanel)GetTemplateChild("PART_AppBarPanel");
            tabMenu = (Border)GetTemplateChild("PART_TabMenu");
            topFiller = (Rectangle)GetTemplateChild("PART_TopFiller");
            backButton = (Button)GetTemplateChild("PART_BackButton");
            tabItemsControl = (ItemsControl)GetTemplateChild("PART_TabItemsControl");
        }

        public ObservableCollection<TopMenuRadioButton> Items
        {
            get { return _items; }
        }

        #region Dependency Properties

        public FrameworkElement AppBarContent
        {
            get { return (FrameworkElement)GetValue(AppBarContentProperty); }
            set { SetValue(AppBarContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AppBarContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AppBarContentProperty =
            DependencyProperty.Register("AppBarContent", typeof(FrameworkElement), typeof(TabContainer), new PropertyMetadata(new Grid()));

        //public FrameworkElement MiniToolbarContent
        //{
        //    get { return (FrameworkElement)GetValue(MiniToolbarContentProperty); }
        //    set { SetValue(MiniToolbarContentProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for AppBarContent.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MiniToolbarContentProperty =
        //    DependencyProperty.Register("MiniToolbarContent", typeof(FrameworkElement), typeof(TabContainer), new PropertyMetadata(null, MiniToolbarContentChanged));

        //private static void MiniToolbarContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    TabContainer target = (TabContainer)d;

        //    if (target.myMiniToolbarBorder is not null)
        //    {
        //        ContentPresenter cp = new ContentPresenter
        //        {
        //            Content = target.MiniToolbarContent
        //        };
        //        target.myMiniToolbarBorder.Child = cp;
        //    }
        //}

        public Visibility BackButtonVisibility
        {
            get { return (Visibility)GetValue(BackButtonVisibilityProperty); }
            set { SetValue(BackButtonVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HamburgerButtonVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackButtonVisibilityProperty =
            DependencyProperty.Register("BackButtonVisibility", typeof(Visibility), typeof(TabContainer), new PropertyMetadata(Visibility.Collapsed, BackButtonVisibilityChanged));

        private static void BackButtonVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TabContainer target = (TabContainer)d;

            if (target.backButton is not null)
                target.backButton.Visibility = (Visibility)e.NewValue;
        }

        #endregion

        #region EventHandler

        private void TabContainer_Loaded(object sender, RoutedEventArgs e)
        {
            //SizeChanged += TabContainer_SizeChanged;

            if (backButton is not null)
            {
                backButton.Click += BackButton_Click;
                backButton.Visibility = BackButtonVisibility;
            }

            if (tabItemsControl is not null)
            {
                tabItemsControl.SizeChanged += TabItemsControl_SizeChanged;
                tabItemsControl.ItemsSource = Items;
            }

            UpdateSubMenuBarVisibility();
        }

        private void TabContainer_Unloaded(object sender, RoutedEventArgs e)
        {
            //SizeChanged -= TabContainer_SizeChanged;
            _items.CollectionChanged -= OnItemsCollectionChanged;

            if (backButton is not null)
                backButton.Click -= BackButton_Click;

            if (tabItemsControl is not null)
                tabItemsControl.SizeChanged -= TabItemsControl_SizeChanged;
        }

        private void OnItemsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            // since we can't make a custom routed event, manually attach and detach event handlers for selection event 
            if (e.NewItems is not null)
            {
                foreach (object o in e.NewItems)
                {
                    if (o is TopMenuRadioButton newItem)
                    {
                        newItem.Checked += NewItem_Checked;
                        newItem.Unchecked += NewItem_Unchecked;
                    }
                }
            }

            if (e.OldItems is not null)
            {
                foreach (object o in e.OldItems)
                {
                    if (o is TopMenuRadioButton oldItem)
                    {
                        oldItem.Checked -= NewItem_Checked;
                        oldItem.Unchecked -= NewItem_Unchecked;
                    }
                }
            }
        }

        private void TabContainer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry r = new RectangleGeometry
            {
                Rect = new Rect(0, 48, e.NewSize.Width, e.NewSize.Height)
            };
            if (appBarPanel is not null)
                appBarPanel.Clip = r;


            if (tabMenu is not null)
            {
                tabMenu.Clip = new RectangleGeometry
                {
                    Rect = new Rect(0, 0, e.NewSize.Width, 48)
                };
            }
        }

        private void TabItemsControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (tabItemsControl is not null)
            {
                tabItemsControl.Clip = new RectangleGeometry
                {
                    Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height)
                };
            }
        }

        private void NewItem_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateSubMenuBarVisibility();
        }

        private void NewItem_Checked(object sender, RoutedEventArgs e)
        {
            UpdateSubMenuBarVisibility();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            BackClicked?.Invoke(sender, new EventArgs());
        }

        //private void AddButton_Click(object sender, RoutedEventArgs e)
        //{
        //    AddClicked?.Invoke(sender, new EventArgs());
        //}

        //private void TabContainer_SizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (tabMenu is not null)
        //    {
        //        tabMenu.Clip = new RectangleGeometry();
        //        tabMenu.Clip.Rect = new Rect(0, 0, e.NewSize.Width, 48);
        //    }
        //}

        #endregion

        #region Commands

        public void UpdateSubMenuBarVisibility()
        {
            foreach (TopMenuRadioButton tb in _items)
            {
                if (tb.IsChecked == true)
                {
                    if (topFiller is not null)
                        topFiller.Visibility = Visibility.Visible;

                    //if (lightLineBorder is not null)
                    //    lightLineBorder.Visibility = Visibility.Visible;
                    return;
                }
            }

            if (topFiller is not null)
                topFiller.Visibility = Visibility.Collapsed;

            //if (lightLineBorder is not null)
            //    lightLineBorder.Visibility = Visibility.Collapsed;
        }

        #endregion

    }
}
