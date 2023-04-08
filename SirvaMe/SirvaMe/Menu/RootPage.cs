using System;
using Xamarin.Forms;

namespace SirvaMe.Menu
{
    public class RootPage : MasterDetailPage
    {
        public RootPage(Page detailPage)
        {
            var menuPage = new MenuPage();
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            Master = menuPage;
            Detail = new NavigationPage(detailPage) { BarBackgroundColor = (Color)Application.Current.Resources["BarBackgroundColor"] };
        }

        void NavigateTo(MenuItem menu)
        {
            var displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            Detail = new NavigationPage(displayPage) { BarBackgroundColor = (Color)Application.Current.Resources["BarBackgroundColor"] };
            IsPresented = false;
        }
    }
}
