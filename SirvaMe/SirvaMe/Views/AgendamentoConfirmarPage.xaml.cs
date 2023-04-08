using System;
using System.Collections.Generic;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class AgendamentoConfirmarPage : ContentPage
    {
        private readonly AgendamentoInfo _agendamentoInfo;
        private readonly List<CustomPin> _prestadoresListaMapa;

        public AgendamentoConfirmarPage(AgendamentoInfo agendamentoInfo)
        {
            InitializeComponent();

            _agendamentoInfo = agendamentoInfo;
            _prestadoresListaMapa = new List<CustomPin>();

            BindingContext = agendamentoInfo;

            GeraMapaPelaPosicaoDosPrestadores(agendamentoInfo.TipoServicoId, agendamentoInfo.Endereco.Latitude, agendamentoInfo.Endereco.Longitude);
        }

        private async void GeraMapaPelaPosicaoDosPrestadores(int tipoServicoId, double latCliente, double lonCliente)
        {
            try
            {
                var api = new UsuarioApi();
                _agendamentoInfo.PrestadorLocation = await api.GetPrestadoresPorTipoServicoIdNaApiAsync(tipoServicoId, latCliente.ToString().Replace(",", "."), lonCliente.ToString().Replace(",", "."));

                AguardeStackLayout.IsVisible = false;
                DadosStackLayout.IsVisible = true;
                ContinuarButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                ContinuarButton.IsEnabled = true;
            }
        }

        private async void ContinuarOnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfissionaisMapaPage(_agendamentoInfo));
        }
    }
}
