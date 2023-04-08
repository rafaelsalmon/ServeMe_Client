using Plugin.Connectivity;
using SirvaMe.Models;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            NavigationPage.SetTitleIcon(this, "logo_header.png");
            
            //ToolbarItems.Add(new ToolbarItem
            //{
            //    Name = "Carrinho",
            //    Icon = "cart2.png",
            //    Order = ToolbarItemOrder.Primary,
            //    Command = new Command(() => Navigation.PushAsync(new HomePage()))
            //});

            if (!CrossConnectivity.Current.IsConnected)
            {
                SemConexaoLabel.IsVisible = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Notificacao>(this, "Notificacoes", (dados) =>
            {
                TesteLabel.Text = $"Serviço Recebido Id: {dados.ServicoId}";
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Notificacao>(this, "Notificacoes");
        }
    }
}
