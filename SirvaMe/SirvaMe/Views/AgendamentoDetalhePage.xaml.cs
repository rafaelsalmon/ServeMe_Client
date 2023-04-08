using System;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class AgendamentoDetalhePage : ContentPage
    {
        private AgendamentoInfo Agendamento { get; set; }

        public AgendamentoDetalhePage(AgendamentoInfo agendamento)
        {
            InitializeComponent();

            App.Current.RefreshData = false;

            if (agendamento.Status == null) agendamento.Status = "AGUARDANDO PROPOSTAS";

            ValidaStatusAgendamento(agendamento);

            Agendamento = agendamento;
            BindingContext = agendamento;
        }

        private void ValidaStatusAgendamento(AgendamentoInfo agendamento)
        {
            try
            {
                if (agendamento.DataHoraInicio < DateTime.Now ||
                    agendamento.Status.Contains("CANCELADO") ||
                    agendamento.Status.Contains("CONCLUÍDO PELO CLIENTE")) Profissional2Button.IsVisible = true;

                if (agendamento.Status == null || agendamento.Status.Equals("AGUARDANDO PROPOSTAS")) CancelarButton.IsVisible = true;
                if (agendamento.Status != null && agendamento.Status.Equals("PROPOSTAS RECEBIDAS"))
                {
                    PropostasButton.IsVisible = true;
                    CancelarButton.IsVisible = true;
                }
                if (agendamento.Status != null && agendamento.Status.Contains("PROFISSIONAL ESCOLHIDO"))
                {
                    ProfissionalButton.IsVisible = true;
                    CancelarButton.IsVisible = true;
                }
                if (agendamento.Status != null && agendamento.Status.Contains("CONCLUÍDO PELO PRESTADOR"))
                {
                    ProfissionalButton.IsVisible = true;
                    ConcluirButton.IsVisible = true;
                    CancelarButton.IsVisible = false;
                }
            }
            catch (Exception e)
            {
                DisplayAlert("Agendamento", "Ocorreu um erro!", "OK");
            }
        }

        private async void PropostasOnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PropostasRecebidasPage(Agendamento));
        }

        private async void CancelarOnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!await DisplayAlert("Cancelar", "Deseja cancelar este agendamento?", "Sim", "Não")) return;

                var apiServico = new ServicosApi();

                await apiServico.PutClienteCancelaAgendamentoNaApiAsync(Agendamento.Id);

                await DisplayAlert("Info", "Agendamento cancelado com sucesso!", "OK");
                App.Current.ShowMainPage(new AgendamentosListaPage());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Falha ao cancelar agendamento!", "OK");
            }
        }

        private void AtivarOnButtonClicked(object sender, EventArgs e)
        {

        }

        private void ProfissionalOnButtonClicked(object sender, EventArgs e)
        {
            CarregaDadosProfissional(true);
        }

        private void Profissional2OnButtonClicked(object sender, EventArgs e)
        {
            CarregaDadosProfissional(false);
        }

        private async void CarregaDadosProfissional(bool mostraContato)
        {
            try
            {
                Loading.IsVisible = true;

                var apiUsuario = new UsuarioApi();
                var prestador = await apiUsuario.GetDadosDoUsuarioNaApiAsync(Agendamento.PrestadorId);

                var avatar = !string.IsNullOrEmpty(prestador.FacebookToken)
                                    ? $"https://graph.facebook.com/{prestador.FacebookToken}/picture?type=large"
                                    : "icon_profile2.png";

                var proposta = new Propostas
                {
                    AgendamentoId = Agendamento.Id,
                    TipoServicoId = Agendamento.TipoServicoId,
                    TipoServico = Agendamento.TipoServico,
                    Avatar = avatar,
                    Prestador = prestador
                };
                await Navigation.PushAsync(new ProfissionalPerfilPage(proposta, Agendamento, mostraContato));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", "Falha ao selecionar profissional!", "OK");
            }
            finally
            {
                Loading.IsVisible = false;
            }
        }

        private async void ConcluirOnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!await DisplayAlert("Concluir", "Confirma a conclusão do serviço?", "Sim", "Não")) return;

                var apiServico = new ServicosApi();
                await apiServico.PutClienteConfirmaAgendamentoPrestadorNaApiAsync(Agendamento.Id);

                var apiUsuario = new UsuarioApi();
                var prestador = await apiUsuario.GetDadosDoUsuarioNaApiAsync(Agendamento.PrestadorId);

                var avatar = !string.IsNullOrEmpty(prestador.FacebookToken)
                                ? $"https://graph.facebook.com/{prestador.FacebookToken}/picture?type=large"
                                : "icon_profile2.png";

                var proposta = new Propostas
                {
                    AgendamentoId = Agendamento.Id,
                    TipoServicoId = Agendamento.TipoServicoId,
                    TipoServico = Agendamento.TipoServico,
                    Avatar = avatar,
                    Prestador = prestador
                };

                await DisplayAlert("Info", "Conclusão confirmada! Obrigado!", "OK");
                await Navigation.PushAsync(new ProfissionalAvaliacaoPage(proposta));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Falha ao concluir serviço!", "OK");
            }
        }
    }
}
