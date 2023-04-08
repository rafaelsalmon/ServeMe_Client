using System;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class TermosDeUsoPage : ContentPage
    {
        private readonly AgendamentoInfo _agendamentoInfo;

        public TermosDeUsoPage(AgendamentoInfo agendamentoInfo)
        {
            InitializeComponent();

            _agendamentoInfo = agendamentoInfo;
            CarregaTexto();
            //TestarWebView();
        }

        private void TestarWebView()
        {
            //var browser = new WebView { Source = "http://xamarin.com" };

            var texto = "<p>Aguarde...</p>";
            TermosWebView.Source = new HtmlWebViewSource { Html = $"<html><body>{texto}</body></html>" };

            var api = new ServicosApi();

            Task.Run(async () =>
            {
                texto = await api.GetTermosDeUsoNaApiAsync((int)TermoUso.TipoTermo.Cliente);
                TermosWebView.Source = new HtmlWebViewSource { Html = $"<html><body>{texto}</body></html>" };
            });
        }

        private void CarregaTexto()
        {
            try
            {
                var texto = "<br><br><br><br><br><center>Aguarde! Carregando Termos de Uso...</center>";
                TermosWebView.Source = new HtmlWebViewSource { Html = $"<html><body>{texto}</body></html>" };

                var api = new ServicosApi();

                Task.Run(async () =>
                {
                    texto = await api.GetTermosDeUsoNaApiAsync((int)TermoUso.TipoTermo.Cliente);
                    TermosWebView.Source = new HtmlWebViewSource { Html = $"<html><body>{texto}</body></html>" };
                });
                ButtonAceitar.IsEnabled = true;
            }
            catch (Exception)
            {
                DisplayAlert("Erro", "Falha ao carregar Termos de Uso!", "OK");
            }
        }

        private async void AceitarTermosOnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!await DisplayAlert("Info", "Deseja enviar esta solicitação?", "Sim", "Não")) return;

                ButtonAceitar.Text = "AGUARDE...";

                var apiUsuario = new UsuarioApi();
                var enderecoApi = await apiUsuario.PostCadastraEnderecoNaApiAsync(_agendamentoInfo.Endereco, App.Current.UserID);

                if (enderecoApi.Id > 0)
                {
                    var agendamento = new Agendamento
                    {
                        ClienteId = App.Current.UserID,
                        EnderecoId = enderecoApi.Id,
                        TipoServicoId = _agendamentoInfo.TipoServicoId,
                        DataHoraInicio = _agendamentoInfo.DataHoraInicio,
                        Titulo = _agendamentoInfo.Titulo,
                        Descricao = _agendamentoInfo.Descricao
                    };

                    var apiServico = new ServicosApi();

                    if (await apiServico.PostCriaAgendamentoNaApiAsync(agendamento) != null)
                    {
                        await DisplayAlert("Enviado com sucesso!", "Por favor, aguarde propostas dos prestadores de serviço!", "OK");
                        App.Current.DataCalendario = _agendamentoInfo.DataHoraInicio.Date.ToString();
                        App.Current.ShowMainPage(new AgendamentosListaPage());
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Falha ao enviar solicitação, tente novamente mais tarde!", "OK");
                        App.Current.ShowMainPage();
                    }
                }
                else
                    await DisplayAlert("Erro", "Falha ao gravar Endereço do agendamento!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Falha ao enviar solicitação de agendamento!", "OK");
            }
            ButtonAceitar.Text = "ACEITAR E SOLICITAR";
        }
    }
}
