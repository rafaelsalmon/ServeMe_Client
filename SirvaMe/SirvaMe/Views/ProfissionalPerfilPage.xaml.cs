using System;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class ProfissionalPerfilPage : ContentPage
    {
        private Propostas Proposta { get; set; }
        private AgendamentoInfo Agendamento { get; set; }

        public ProfissionalPerfilPage(Propostas proposta, AgendamentoInfo agendamento, bool mostraContato)
        {
            try
            {
                InitializeComponent();

                Proposta = proposta;
                Agendamento = agendamento;
                BindingContext = proposta;
                
                AlteraBotaoEscolherLigar(agendamento, mostraContato);
                SetaStarsRating(proposta.Prestador.Nota);
                GetAvaliacoesPrestador();
            }
            catch (Exception e)
            {
                DisplayAlert("Agendamento", "Ocorreu um erro!", "OK");
            }
        }

        private async void GetAvaliacoesPrestador()
        {
            try
            {
                var apiUsuario = new UsuarioApi();
                var avaliacoes = await apiUsuario.GetAvaliacoesPrestadorNaApiAsync(Proposta.Prestador.Id);
                if (avaliacoes == null || avaliacoes.Count <= 0) return;

                AvaliacoesStackLayout.IsVisible = true;
                ListaAvaliacoes.ItemsSource = avaliacoes;
            }
            catch (Exception ex)
            {
               
            }
        }

        private void AlteraBotaoEscolherLigar(AgendamentoInfo agendamento, bool mostraContato)
        {
            if (mostraContato)
                if (agendamento.PrestadorId > 0)
                {
                    LigarButton.IsVisible = true;
                    EscolherButton.IsVisible = false;
                }
                else
                {
                    LigarButton.IsVisible = false;
                    EscolherButton.IsVisible = true;
                }
            else
            {
                LigarButton.IsVisible = false;
                EscolherButton.IsVisible = false;
            }
        }

        private void SetaStarsRating(int stars)
        {
            switch (stars)
            {
                case 0:
                    StarSelectedOne.IsVisible = false;
                    StarSelectedTwo.IsVisible = false;
                    StarSelectedThree.IsVisible = false;
                    StarSelectedFour.IsVisible = false;
                    StarSelectedFive.IsVisible = false;
                    break;
                case 1:
                    StarSelectedTwo.IsVisible = false;
                    StarSelectedThree.IsVisible = false;
                    StarSelectedFour.IsVisible = false;
                    StarSelectedFive.IsVisible = false;
                    break;
                case 2:
                    StarSelectedThree.IsVisible = false;
                    StarSelectedFour.IsVisible = false;
                    StarSelectedFive.IsVisible = false;
                    break;
                case 3:
                    StarSelectedFour.IsVisible = false;
                    StarSelectedFive.IsVisible = false;
                    break;
                case 4:
                    StarSelectedFive.IsVisible = false;
                    break;
            }
        }

        private async void EscolherOnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!await DisplayAlert("Info", "Deseja escolher este profissional?", "Sim", "Não")) return;

                var api = new ServicosApi();
                var agendamentoPrestador = new AgendamentoPrestador
                {
                    AgendamentoId = Proposta.AgendamentoId,
                    PrestadorId = Proposta.Prestador.Id
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

        private async void LigarOnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var telefone = Proposta.Prestador.TelefonePrestador;
                Device.OpenUri(new Uri($"tel:{telefone.Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "")}"));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", $"Falha ao carregar telefone: {Proposta.Prestador.TelefonePrestador}!", "OK");
            }
        }

        private void ListaAvaliacoes_OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args == null) return; // has been set to null, do not 'process' tapped event
            ((ListView)sender).SelectedItem = null; // de-select the row
        }
    }
}
