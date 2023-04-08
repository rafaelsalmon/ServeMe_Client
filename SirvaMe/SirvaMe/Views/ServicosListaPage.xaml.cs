using System;
using SirvaMe.Models;
using SirvaMe.ViewModels;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class ServicosListaPage : ContentPage
    {
        public ServicosListaPage()
        {
            InitializeComponent();
            BindingContext = new ServicosVM();
        }

        private async void ListaServicosOnItemTapped(object sender, ItemTappedEventArgs args)
        {
            try
            {
                var servico = args.Item as Servicos;
                if (servico == null) return;

                await Navigation.PushAsync(new AgendamentoSolicitarPage(servico));

                ListaServicos.SelectedItem = null;
            }
            catch (Exception e)
            {
                await DisplayAlert("Agendamento", "Falha ao solicitar!", "OK");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (App.Current.RecebeuPush) Navigation.PushAsync(new AgendamentosListaPage());

            //MessagingCenter.Subscribe<CrossPushNotificationListener, Notificacao>(this, "PushNotification", (sender, arg) =>
            //{
            //    Navigation.PushAsync(new AgendamentosListaPage());
            //});
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.Current.RecebeuPush = false;
            //MessagingCenter.Unsubscribe<CrossPushNotificationListener, Notificacao>(this, "PushNotification");
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.DataCalendario = "";
            return false;
        }
    }
}
