using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace Sales4Pro.WinUI.CustomControls.Menu
{
    public class TabContainerPage : Page
    {
        public TabContainerPage()
        { }

        ~TabContainerPage()
        { }

        public TabContainer GetTabContainer()
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this.Frame);
            while (parent is not null)
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (parent is not null && ((FrameworkElement)parent).GetType() == typeof(TabContainer))
                    return (TabContainer)parent;
            }
            return new TabContainer();
        }

        public void ClearTopMenu()
        {
            TabContainer tabContainer = GetTabContainer();
            tabContainer.Items.Clear();
        }

        public void AddTopMenuRadioButton(TopMenuRadioButton topMenuRadioButton)
        {
            TabContainer tabContainer = GetTabContainer();
            tabContainer.Items.Add(topMenuRadioButton);
            tabContainer.UpdateSubMenuBarVisibility();
        }

        public void SetSubMenu(UIElement subMenu)
        {
            TabContainer tabContainer = GetTabContainer();
            if (tabContainer.AppBarContent.GetType() == typeof(SubMenuShadowAppBar))
            {
                SubMenuShadowAppBar smab = (SubMenuShadowAppBar)tabContainer.AppBarContent;
                smab.Content = subMenu;
            }
        }

    }
}
