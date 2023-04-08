using System;
using SirvaMe.Models;
using SirvaMe.Services;
using SirvaMe.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SirvaMe.Views
{
    public partial class AgendamentoMapaPage : ContentPage
    {
        private readonly AgendamentoInfo _agendamentoInfo;

        public AgendamentoMapaPage(AgendamentoInfo agendamentoInfo)
        {
            try
            {
                InitializeComponent();

                _agendamentoInfo = agendamentoInfo;

                BusyStackLayout.IsVisible = true;

                if (!EnderecoServicoValido(agendamentoInfo.Endereco))
                    TentaCarregarEnderecoNovamente();
                else
                    HabilitaMapaComPosicaoAtual();
            }
            catch (Exception)
            {
                DisplayAlert("Erro", "Ocorreu um erro, tente novamente mais tarde!", "OK");
            }
        }

        private async void TentaCarregarEnderecoNovamente()
        {
            try
            {
                var util = new Util();
                var endereco = await util.CarregaEnderecoCompletoComPositionsAsync();

                if (EnderecoServicoValido(endereco))
                {
                    _agendamentoInfo.Endereco = endereco;
                    _agendamentoInfo.EnderecoCompleto = endereco.RetornaEnderecoCompleto(endereco);
                    HabilitaMapaComPosicaoAtual();
                }
                else
                {
                    if (await DisplayAlert("Sem localização (GPS)", "Caso persista, habilite a localização em configurações! Deseja tentar novamente?", "Sim", "Não"))
                        await Navigation.PopAsync(true);
                    else
                        HabilitaEnderecoManual();
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Ocorreu um erro, tente novamente mais tarde!", "OK");
            }
        }

        private bool EnderecoServicoValido(Endereco endereco)
        {
            try
            {
                PopulaObjetoEndereco();

                var retorno = endereco != null
                                && endereco.Latitude != 0
                                && endereco.Longitude != 0
                                && !string.IsNullOrEmpty(endereco.Logradouro)
                                && !string.IsNullOrEmpty(endereco.Numero)
                                && !string.IsNullOrEmpty(endereco.Bairro)
                                && !string.IsNullOrEmpty(endereco.Cidade);
                return retorno;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void PopulaObjetoEndereco()
        {
            try
            {
                if (!string.IsNullOrEmpty(LogradouroEntry.Text)) _agendamentoInfo.Endereco.Logradouro = LogradouroEntry.Text;
                if (!string.IsNullOrEmpty(NumeroEntry.Text)) _agendamentoInfo.Endereco.Numero = NumeroEntry.Text;
                if (!string.IsNullOrEmpty(BairroEntry.Text)) _agendamentoInfo.Endereco.Bairro = BairroEntry.Text;
                if (!string.IsNullOrEmpty(CidadeEntry.Text)) _agendamentoInfo.Endereco.Cidade = CidadeEntry.Text;
                if (!string.IsNullOrEmpty(ComplementoEntry.Text)) _agendamentoInfo.Endereco.Complemento = ComplementoEntry.Text;
                if (!string.IsNullOrEmpty(PontoReferenciaEntry.Text)) _agendamentoInfo.Endereco.PontoReferencia = PontoReferenciaEntry.Text;
                if (!string.IsNullOrEmpty(CepEntry.Text)) _agendamentoInfo.Endereco.Cep = CepEntry.Text;
                if (!string.IsNullOrEmpty(LogradouroEntry.Text)) _agendamentoInfo.EnderecoCompleto = new Endereco().RetornaEnderecoCompleto(_agendamentoInfo.Endereco);
            }
            catch (Exception ex)
            {

            }
        }

        private async void HabilitaMapaComPosicaoAtual()
        {
            try
            {
                BusyStackLayout.IsVisible = false;
                MapStackLayout.IsVisible = true;
                ContinuarStackLayout.IsVisible = true;
                MyMap.IsVisible = true;

                var gpsPosition = new Position(_agendamentoInfo.Endereco.Latitude, _agendamentoInfo.Endereco.Longitude);
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(gpsPosition, Distance.FromMeters(180)));
                EnderecoEntry.Text = _agendamentoInfo.EnderecoCompleto;
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Ocorreu um erro, tente novamente mais tarde!", "OK");
            }
        }

        private void HabilitaEnderecoManual()
        {
            BusyStackLayout.IsVisible = false;
            MapStackLayout.IsVisible = false;
            EnderecoStackLayout.IsVisible = true;
            ContinuarStackLayout.IsVisible = true;
            BackgroundColor = Color.White;
        }

        private async void ContinuarOnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (!EnderecoServicoValido(_agendamentoInfo.Endereco))
                {
                    if (!CamposObrigatoriosPreenchidos())
                    {
                        await DisplayAlert("Endereço incompleto", "Por favor, preencha os campos obrigatórios em vermelho!", "OK");
                        return;
                    }

                    var endereco = new Endereco
                    {
                        Logradouro = LogradouroEntry.Text,
                        Numero = NumeroEntry.Text,
                        Complemento = ComplementoEntry.Text,
                        Bairro = BairroEntry.Text,
                        Cidade = CidadeEntry.Text,
                        Estado = EstadoEntry.Text,
                        Cep = CepEntry.Text,
                        PontoReferencia = PontoReferenciaEntry.Text
                    };

                    var api = new UsuarioApi();
                    var positions = await api.GetPositionsFromAddressAsync(endereco.RetornaEnderecoCompleto(endereco));

                    endereco.Latitude = positions.Latitude;
                    endereco.Longitude = positions.Longitude;

                    _agendamentoInfo.Endereco = endereco;
                    _agendamentoInfo.EnderecoCompleto = endereco.RetornaEnderecoCompleto(endereco);
                }

                if (EnderecoServicoValido(_agendamentoInfo.Endereco))
                    await Navigation.PushAsync(new AgendamentoConfirmarPage(_agendamentoInfo));
                else
                    await DisplayAlert("Endereço inválido", "Digite um endereço válido para o serviço!", "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Endereço inválido", "Digite um endereço válido para o serviço!", "OK");
            }
        }

        private void EnderecoEntry_Tapped(object sender, EventArgs e)
        {
            HabilitaEnderecoManual();
            PopulaCamposEndereco();
        }

        private async void PopulaCamposEndereco()
        {
            try
            {
                LogradouroEntry.Text = _agendamentoInfo.Endereco.Logradouro;
                BairroEntry.Text = _agendamentoInfo.Endereco.Bairro;
                CidadeEntry.Text = _agendamentoInfo.Endereco.Cidade;
                EstadoEntry.Text = _agendamentoInfo.Endereco.Estado;
                CepEntry.Text = _agendamentoInfo.Endereco.Cep;
                NumeroEntry.Text = _agendamentoInfo.Endereco.Numero;
            }
            catch (Exception)
            {
                await DisplayAlert("Erro", "Ocorreu um erro, tente novamente mais tarde!", "OK");
            }
        }

        private async void BuscarLogradouroButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (CepEntry.Text == null || CepEntry.Text.Length != 8)
                {
                    await DisplayAlert("CEP", "Informe um CEP com 8 dígitos!", "OK");
                }
                else
                {
                    ButtonBuscar.Text = "AGUARDE!";

                    var api = new UsuarioApi();
                    var endereco = await api.GetEnderecoPeloCepNaApiAsync(CepEntry.Text);

                    LogradouroEntry.Text = (endereco.TipoDeLogradouro + ' ' + endereco.Logradouro);
                    BairroEntry.Text = endereco.Bairro;
                    CidadeEntry.Text = endereco.Cidade;
                    EstadoEntry.Text = endereco.Estado;
                    //NumeroEntry.Focus();
                    CamposObrigatoriosPreenchidos();

                    ButtonBuscar.Text = "BUSCAR";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("CEP", "Falha na busca pelo CEP", "OK");
            }
        }

        private void CepEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (CepEntry.Text.Length > 8) CepEntry.Text = CepEntry.Text.Remove(CepEntry.Text.Length - 1); //Remove Last character 
        }

        private bool CamposObrigatoriosPreenchidos()
        {
            try
            {
                var corTitulo = (Color)Application.Current.Resources["PerfilTitleFontColor"];
                var corErro = Color.Red;

                LogradouroLabel.TextColor = string.IsNullOrEmpty(LogradouroEntry.Text) ? corErro : corTitulo;
                BairroLabel.TextColor = string.IsNullOrEmpty(BairroEntry.Text) ? corErro : corTitulo;
                NumeroLabel.TextColor = string.IsNullOrEmpty(NumeroEntry.Text) ? corErro : corTitulo;
                CidadeLabel.TextColor = string.IsNullOrEmpty(CidadeEntry.Text) ? corErro : corTitulo;

                var retorno = !string.IsNullOrEmpty(LogradouroEntry.Text)
                                && !string.IsNullOrEmpty(NumeroEntry.Text)
                                && !string.IsNullOrEmpty(BairroEntry.Text)
                                && !string.IsNullOrEmpty(CidadeEntry.Text);
                return retorno;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //private async void PopulaEnderecoComPossivelAlteracao(string enderecoCompleto)
        //{
        //    try
        //    {
        //        var endereco = enderecoCompleto.Split(new char[] { ',', '-', '/', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        //        _endereco.Logradouro = endereco[0];
        //        _endereco.Numero = endereco[1].Trim();
        //        _endereco.Bairro = endereco[2].Trim();
        //        _endereco.Cidade = endereco[3].Trim();
        //        _endereco.Estado = endereco[4].Trim();

        //        if (_endereco.Latitude != 0) return;

        //        var api = new UsuarioApi();
        //        var positions = await api.GetPositionsFromGoogleApis(enderecoCompleto);

        //        _endereco.Latitude = positions.Lat;
        //        _endereco.Longitude = positions.Lng;
        //    }
        //    catch (Exception e)
        //    {
        //        _endereco.Logradouro = enderecoCompleto;
        //    }
        //}

        //private void PopulaObjetoEndereco(List<string> addressesList)
        //{
        //    try
        //    {
        //        if (addressesList == null) return;

        //        PopulaEnderecoComPossivelAlteracao(addressesList[0]);

        //        var cep = addressesList[4].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        //        _endereco.Cep = cep[2].Trim().Replace("-", "");
        //    }
        //    catch (Exception e)
        //    {
        //        _endereco.Logradouro = addressesList != null ? addressesList[0].Replace(Environment.NewLine, " - ") : "Falha ao buscar endereço, verifique seu GPS!";
        //    }
        //}

        //private static async Task<Position> GetPositions()
        //{
        //    try
        //    {
        //        var geo = new Geolocator();
        //        var positionPlugin = await geo.GetPositions();
        //        return new Position(positionPlugin.Latitude, positionPlugin.Longitude);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Position();
        //    }
        //}

        //private async void ContinuarOnButtonClicked2(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(EnderecoEntry.Text) || EnderecoEntry.Text.Contains("Falha"))
        //        {
        //            await DisplayAlert("Endereço em branco", "Digite um endereço válido para o serviço!", "OK");
        //            return;
        //        }

        //        if (EnderecoEntry.Text.Length < 10)
        //        {
        //            if (EnderecoEntry.Text == null || EnderecoEntry.Text.Replace("-", "").Length != 8)
        //            {
        //                await DisplayAlert("CEP", "Informe um CEP com 8 dígitos!", "OK");
        //            }
        //            else
        //            {
        //                var api = new UsuarioApi();
        //                var endereco = await api.GetEnderecoPeloCepNaApiAsync(EnderecoEntry.Text);

        //                if (endereco != null)
        //                {
        //                    _endereco.Cep = endereco.Cep;
        //                    EnderecoEntry.Text = new Endereco().RetornaEnderecoCompleto(endereco);
        //                    await DisplayAlert("CEP localizado", "Informe o número do local!", "OK");
        //                }
        //                else
        //                    await DisplayAlert("CEP inválido", "Informe apenas números!", "OK");
        //            }
        //            return;
        //        }

        //        PopulaEnderecoComPossivelAlteracao(EnderecoEntry.Text);

        //        if (_endereco.Bairro.Length > 0 && _endereco.Numero.Length == 0)
        //        {
        //            await DisplayAlert("CEP localizado", "Informe o número do local!", "OK");
        //            return;
        //        }
        //        _agendamentoInfo.EnderecoCompleto = EnderecoEntry.Text;
        //        _agendamentoInfo.Endereco = _endereco;

        //        await Navigation.PushAsync(new AgendamentoConfirmarPage(_agendamentoInfo));
        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Endereço inválido", "Digite um endereço válido para o serviço ou habilite a Localização em seu aparelho!", "OK");
        //    }
        //}

        //private async void GeraMapaPelaPosicaoAtual()
        //{
        //    try
        //    {
        //        ContinuarButton.Text = "AGUARDE...";

        //        Position gpsPosition;

        //        if (_agendamentoInfo.Endereco == null || _agendamentoInfo.Endereco.Latitude == 0)
        //            gpsPosition = await GetPositions();
        //        else
        //            gpsPosition = new Position(_agendamentoInfo.Endereco.Latitude, _agendamentoInfo.Endereco.Longitude);

        //        BusyStackLayout.IsVisible = false;
        //        ContinuarStackLayout.IsVisible = true;

        //        if (gpsPosition.Latitude == 0)
        //        {
        //            HabilitaEnderecoManual();
        //        }
        //        else
        //        {
        //            MyMap.IsVisible = true;
        //            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(gpsPosition, Distance.FromMeters(180)));

        //            var reverse = new ReverseGeocode();
        //            var addressesList = await reverse.GetAddressesFromPositionAsync(gpsPosition);

        //            EnderecoEntry.Text = addressesList != null
        //                ? addressesList[0].Replace(Environment.NewLine, " - ")
        //                : "Falha ao buscar endereço, verifique seu GPS!";

        //            _endereco.Latitude = gpsPosition.Latitude;
        //            _endereco.Longitude = gpsPosition.Longitude;
        //            PopulaObjetoEndereco(addressesList);
        //        }
        //        ContinuarButton.Text = "CONTINUAR";
        //        ContinuarButton.IsEnabled = true;
        //    }
        //    catch (Exception e)
        //    {
        //        await DisplayAlert("Endereço", "Ocorreu uma falha ao pegar sua posição atual!", "OK");
        //    }
        //}

    }
}
