using System;
using SirvaMe.Models;
using SirvaMe.ViewModels;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class AgendamentosHistoricoPage : ContentPage
    {
        public AgendamentosHistoricoPage()
        {
            InitializeComponent();
            BindingContext = new AgendamentosVM(false);
        }

        protected override void OnAppearing()
        {
            if (!string.IsNullOrEmpty(App.Current.DataCalendario))
                BindingContext = new AgendamentosVM(false);
        }

        private void RefreshOnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            BindingContext = new AgendamentosVM(false);
        }

        private async void AgendamentosDisponiveis_OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            try
            {
                if (args == null) return; // has been set to null, do not 'process' tapped event
                ((ListView)sender).SelectedItem = null; // de-select the row

                var agendamento = args.Item as AgendamentoInfo;
                if (agendamento == null) return;

                await Navigation.PushAsync(new AgendamentoDetalhePage(agendamento));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao mostrar o agendamento", "OK");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.ShowMainPage();
            return true;
        }
    }
}
