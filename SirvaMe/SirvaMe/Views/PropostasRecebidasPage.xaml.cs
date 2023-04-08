using SirvaMe.ViewModels;
using System;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class PropostasRecebidasPage : ContentPage
    {
        private AgendamentoInfo Agendamento { get; set; }

        public PropostasRecebidasPage(AgendamentoInfo agendamento)
        {
            InitializeComponent();

            Agendamento = agendamento;
            BindingContext = new PropostasVM(agendamento);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (!await DisplayAlert("Info", "Deseja escolher este profissional?", "Sim", "Não")) return;

                var obj = (Button)sender;
                var id = (int)obj.CommandParameter;

                var api = new ServicosApi();
                var agendamentoPrestador = new AgendamentoPrestador
                {
                    AgendamentoId = Agendamento.Id,
                    PrestadorId = id
                };

                await api.PostClienteAceitaPrestadorNaApiAsync(agendamentoPrestador);

                await DisplayAlert("Profissional escolhido", "Aguarde a chegada no dia e horário marcados.", "OK");

                App.Current.ShowMainPage(new AgendamentosListaPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Falha ao escolher profissional!", "OK");
            }
        }

        private async void ListaPropostas_OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args == null) return; // has been set to null, do not 'process' tapped event
            ((ListView)sender).SelectedItem = null; // de-select the row

            var proposta = args.Item as Propostas;
            if (proposta == null) return;

            await Navigation.PushAsync(new ProfissionalPerfilPage(proposta, Agendamento, true));
        }
    }
}
