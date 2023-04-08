using System;
using System.Text.RegularExpressions;
using SirvaMe.Models;
using SirvaMe.Services;
using SirvaMe.ViewModels;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class MeuPerfilPage : ContentPage
    {
        private UsuarioVM ViewModel { get; set; }

        public MeuPerfilPage()
        {
            InitializeComponent();

            ViewModel = new UsuarioVM();
            BindingContext = ViewModel;
        }

        private async void SalvarOnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                if (ValidaCamposObrigatorios())
                {
                    await DisplayAlert("Campos Obrigatórios", "Preencha todos os campos em vermelho!", "OK");
                    return;
                }

                ViewModel.IsBusy = true;
                AguardeLabel.Text = "Aguarde! Salvando...";

                var api = new UsuarioApi();
                var usuario = api.GetUsuarioNaBase();

                usuario.Nome = NomeEntry.Text;
                usuario.Cpf = CpfEntry.Text;
                usuario.DataNascimento = Convert.ToDateTime(Convert.ToDateTime(NascimentoEntry.Text).ToString("yyyy-MM-dd"));
                usuario.TelefoneCliente = TelefoneEntry.Text;
                usuario.EmailCliente = EmailEntry.Text;

                App.Current.UserName = NomeEntry.Text;

                var endereco = new Endereco
                {
                    Logradouro = LogradouroEntry.Text,
                    PontoReferencia = PontoReferenciaEntry.Text,
                    Numero = NumeroEntry.Text,
                    Complemento = ComplementoEntry.Text,
                    Bairro = BairroEntry.Text,
                    Cidade = CidadeEntry.Text,
                    Estado = EstadoEntry.Text,
                    Cep = CepEntry.Text
                };

                var positions = await api.GetPositionsFromAddressAsync(endereco.RetornaEnderecoCompleto(endereco));

                endereco.Latitude = positions.Latitude; 
                endereco.Longitude = positions.Longitude;

                var enderecoBase = ViewModel.Endereco != null
                                        ? await api.PutAtualizaEnderecoNaApiAsync(endereco, App.Current.UserID)
                                        : await api.PostCadastraEnderecoNaApiAsync(endereco, App.Current.UserID);

                if (enderecoBase != null && enderecoBase.Id > 0)
                {
                    if (await api.AtualizaUsuario(usuario) != null)
                    {
                        await DisplayAlert("Perfil", "Dados salvos com sucesso!", "OK");
                        App.Current.ShowMainPage();
                    }
                    else
                        throw new Exception();
                }
                else
                    await DisplayAlert("Perfil", "Falha ao salvar endereço!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Perfil", "Falha ao salvar dados!", "OK");
            }
            ViewModel.IsBusy = false;
        }

        private bool ValidaCamposObrigatorios()
        {
            var corTitulo = (Color)Application.Current.Resources["PerfilTitleFontColor"];
            var corErro = Color.Red;

            NomeLabel.TextColor = NomeEntry.Text == null ? corErro : corTitulo;
            CpfLabel.TextColor = CpfEntry.Text == null ? corErro : corTitulo;
            NascimentoLabel.TextColor = NascimentoEntry.Text == null ? corErro : corTitulo;
            TelefoneLabel.TextColor = TelefoneEntry.Text == null ? corErro : corTitulo;
            EmailLabel.TextColor = EmailEntry.Text == null ? corErro : corTitulo;
            LogradouroLabel.TextColor = LogradouroEntry.Text == null ? corErro : corTitulo;
            CepLabel.TextColor = CepEntry.Text == null ? corErro : corTitulo;
            NumeroLabel.TextColor = NumeroEntry.Text == null ? corErro : corTitulo;

            return NomeEntry.Text == null || CpfEntry.Text == null || NascimentoEntry.Text == null || EmailEntry.Text == null ||
                   TelefoneEntry.Text == null || LogradouroEntry.Text == null || CepEntry.Text == null || NumeroEntry.Text == null;
        }

        private void TelefoneOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var numeroTel = TelefoneEntry.Text; //Get Current Text

            if (numeroTel.Length >= 8 && numeroTel.Length <= 9)
            {
                numeroTel = numeroTel.Replace("-", "");
                numeroTel = numeroTel.Replace("(", "");
                numeroTel = numeroTel.Replace(")", "");
                numeroTel = numeroTel.Replace(" ", "");

                TelefoneEntry.Text = String.Format("{0}-{1}",
                                            numeroTel.Substring(0, numeroTel.Length - 4),
                                            numeroTel.Substring(numeroTel.Length - 4));
            }
            if (numeroTel.Length > 9 && numeroTel.Length <= 15)
            {
                numeroTel = numeroTel.Replace("-", "");
                numeroTel = numeroTel.Replace("(", "");
                numeroTel = numeroTel.Replace(")", "");
                numeroTel = numeroTel.Replace(" ", "");

                TelefoneEntry.Text = String.Format("({0}) {1}-{2}",
                                            numeroTel.Substring(0, 2),
                                            numeroTel.Substring(2, numeroTel.Length - 6),
                                            numeroTel.Substring(numeroTel.Length - 4));
            }
            if (numeroTel.Length > 15) //If it is more than your character restriction
            {
                TelefoneEntry.Text = numeroTel.Remove(numeroTel.Length - 1); //Remove Last character 
            }
        }

        private void CpfOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var cpf = CpfEntry.Text;

            if (cpf.Length > 11)
            {
                CpfEntry.Text = cpf.Remove(cpf.Length - 1); //Remove Last character 
            }
            else if (cpf.Length == 11)
            {
                if (!ValidaCpf(cpf))
                {
                    CpfLabel.TextColor = Color.Red;
                    CpfLabel.Text = "CPF Inválido";
                }
                else
                {
                    CpfLabel.TextColor = Color.Green;
                    CpfLabel.Text = "CPF OK";
                }
                SalvarButton.IsEnabled = true;
            }
            else
            {
                CpfLabel.TextColor = Color.Red;
                SalvarButton.IsEnabled = false;
            }
        }

        private void NascimentoEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var dataNasc = NascimentoEntry.Text.Replace("/", ""); //Get Current Text

            if (dataNasc.Length == 8)
            {
                dataNasc = dataNasc.Replace("/", "");

                NascimentoEntry.Text = $"{dataNasc.Substring(0, 2)}/{dataNasc.Substring(2, dataNasc.Length - 6)}/{dataNasc.Substring(dataNasc.Length - 4)}";
            }
            if (dataNasc.Length > 8) //If it is more than your character restriction
            {
                NascimentoEntry.Text = dataNasc.Remove(dataNasc.Length - 1); //Remove Last character 
            }
        }

        private async void NascimentoEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            if (NascimentoEntry.Text == null || NascimentoEntry.Text.Length < 8)
            {
                NascimentoLabel.TextColor = Color.Red;
                await DisplayAlert("Nascimento", "Informe uma Data de Nascimento válida!", "OK");
            }
            else
            {
                NascimentoLabel.TextColor = !ValidaDataNascimento(NascimentoEntry.Text) ? Color.Red : Color.Green;
            }
        }

        private bool ValidaDataNascimento(string dataNasc)
        {
            var resultado = DateTime.MinValue;
            return DateTime.TryParse(dataNasc.Trim(), out resultado);
        }

        private static bool ValidaCpf(string valor)
        {
            if (valor.Length != 11) return false;

            var igual = true;

            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909") return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(valor[i].ToString());

            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0) return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0) return false;
            }
            else
                if (numeros[10] != 11 - resultado) return false;

            return true;
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
                    //ButtonBuscar.TextColor = Color.Red;

                    var api = new UsuarioApi();
                    var endereco = await api.GetEnderecoPeloCepNaApiAsync(CepEntry.Text);

                    LogradouroEntry.Text = (endereco.TipoDeLogradouro + ' ' + endereco.Logradouro);
                    BairroEntry.Text = endereco.Bairro;
                    CidadeEntry.Text = endereco.Cidade;
                    EstadoEntry.Text = endereco.Estado;
                    //PontoReferenciaEntry.Focus();

                    ValidaCamposObrigatorios();

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

        private void NomeEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            NomeLabel.TextColor = (NomeEntry.Text.Length < 4) ? Color.Red : (Color)Application.Current.Resources["PerfilTitleFontColor"];
        }

        private async void EmailEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            if (EmailEntry.Text.Length <= 0) return;

            if (!ValidaEmail(EmailEntry.Text))
            {
                EmailLabel.TextColor = Color.Red;
                await DisplayAlert("E-mail", "Informe um E-mail válido!", "OK");
            }
            else
                EmailLabel.TextColor = Color.Green;
        }

        bool ValidaEmail(string email)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }
}
