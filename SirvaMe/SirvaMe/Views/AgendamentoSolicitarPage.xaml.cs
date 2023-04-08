using System;
using SirvaMe.Models;
using SirvaMe.Utils;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class AgendamentoSolicitarPage : ContentPage
    {
        private readonly AgendamentoInfo _agendamentoInfo;

        public AgendamentoSolicitarPage(Servicos servico)
        {
            InitializeComponent();

            try
            {
                _agendamentoInfo = new AgendamentoInfo
                {
                    TipoServicoId = servico.Id,
                    TipoServico = servico.Nome
                };
                TipoDeServicoLabel.Text = $"Categoria | {servico.Nome}";

                DataDatePicker.SetValue(DatePicker.MinimumDateProperty, DateTime.Now);
                DataDatePicker.SetValue(DatePicker.MaximumDateProperty, DateTime.Now.AddMonths(1));
                HoraTimePicker.Time = DateTime.Now.AddHours(1).TimeOfDay;

                CarregaEnderecoCompletoComPositionsAsync();
            }
            catch (Exception ex)
            {
                DisplayAlert("Agendamento", "Falha ao solicitar!", "OK");
            }
        }

        private async void CarregaEnderecoCompletoComPositionsAsync()
        {
            try
            {
                var util = new Util();
                var endereco = await util.CarregaEnderecoCompletoComPositionsAsync();

                _agendamentoInfo.Endereco = endereco;
                _agendamentoInfo.EnderecoCompleto = endereco.RetornaEnderecoCompleto(endereco);
            }
            catch (Exception ex)
            {
                _agendamentoInfo.Endereco = new Endereco();
            }
        }

        private async void ContinuarOnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var data = DataDatePicker.Date;
                var hora = HoraTimePicker.Time;
                var dataHoraServico = new DateTime(data.Year, data.Month, data.Day, hora.Hours, hora.Minutes, 0);

                if (string.IsNullOrEmpty(DescricaoEntry.Text) || string.IsNullOrEmpty(TituloEntry.Text))
                {
                    TituloLabel.TextColor = Color.Red;
                    DescricaoLabel.TextColor = Color.Red;
                    return;
                }
                if (dataHoraServico < DateTime.Now.AddMinutes(15))
                {
                    HoraLabel.TextColor = Color.Red;
                    await DisplayAlert("Hora inválida", "O horário do serviço não pode ser inferior a 15 minutos, sendo no mesmo dia!", "OK");
                    return;
                }

                _agendamentoInfo.Titulo = TituloEntry.Text;
                _agendamentoInfo.Descricao = DescricaoEntry.Text;
                _agendamentoInfo.DataHoraInicio = dataHoraServico;

                await Navigation.PushAsync(new AgendamentoMapaPage(_agendamentoInfo));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Agendamento", "Falha ao criar!", "OK");
            }
        }

        //_descricao = " Descreva o serviço que precisa";
        //DescricaoEntry.Text = _descricao;
        //DescricaoEntry.TextColor = (Color)Application.Current.Resources["PerfilPlaceFontColor"];

        //private void DescricaoEntry_OnFocused(object sender, FocusEventArgs e)
        //{
        //    if (DescricaoEntry.Text.Equals(_descricao))
        //    {
        //        DescricaoEntry.Text = "";
        //        DescricaoEntry.TextColor = (Color)Application.Current.Resources["PerfilTitleFontColor"];
        //    }
        //}

        //private void DescricaoEntry_OnUnfocused(object sender, FocusEventArgs e)
        //{
        //    if (DescricaoEntry.Text.Equals(""))
        //    {
        //        DescricaoEntry.Text = _descricao;
        //        DescricaoEntry.TextColor = (Color)Application.Current.Resources["PerfilPlaceFontColor"];
        //    }
        //}
    }
}
