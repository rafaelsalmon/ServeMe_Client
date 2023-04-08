using SirvaMe.ViewModels;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class ProfissionaisListaPage : ContentPage
    {
        public ProfissionaisListaPage()
        {
            InitializeComponent();
            BindingContext = new ProfissionaisVM();

            //ToolbarItems.Add(new ToolbarItem
            //{
            //    Name = "Mapa",
            //    Icon = "map.png",
            //    Order = ToolbarItemOrder.Primary,
            //    Command = new Command(() => Navigation.PushAsync(new ProfissionaisMapaPage(new Models.AgendamentoInfo())))
            //});
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        private async void ListaServicosOnItemTapped(object sender, ItemTappedEventArgs e)
        {
            //await Navigation.PushAsync(new ProfissionalPerfilPage(App.Rating));
        }
    }
}
