using System.Collections.Generic;
using Xamarin.Forms;

namespace SirvaMe.Menu
{
    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = new MenuListData();
            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.White;
            SeparatorColor = Color.Black;
            
            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetValue(TextCell.TextColorProperty, Color.FromHex("#464646"));

            ItemTemplate = cell;
            SelectedItem = data[0];
        }
    }
}
