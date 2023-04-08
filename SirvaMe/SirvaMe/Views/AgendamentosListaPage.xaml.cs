using System;
using SirvaMe.Models;
using SirvaMe.ViewModels;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    public partial class AgendamentosListaPage : ContentPage
    {
        public AgendamentosListaPage()
        {
            InitializeComponent();

            App.Current.RefreshData = false;

            BindingContext = new AgendamentosVM(true);

            #region ToolbarItem

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Agenda",
                Icon = "icon_agenda.png",
                Order = ToolbarItemOrder.Primary,
                Command = new Command(() => Navigation.PushAsync(new AgendaPage()))
            });

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Refresh",
                Icon = "icon_refresh2.png",
                Order = ToolbarItemOrder.Primary,
                Command = new Command(RefreshPage)
            });

            #endregion
        }

        protected override void OnAppearing()
        {
            try
            {
                SetaDataLabel();

                if (!string.IsNullOrEmpty(App.Current.DataCalendario) && App.Current.RefreshData)
                    BindingContext = new AgendamentosVM(true);
            }
            catch (Exception)
            {

            }
        }

        private void RefreshOnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            RefreshPage();
        }

        private void RefreshPage()
        {
            SetaDataLabel();

            App.Current.RefreshData = false;

            BindingContext = new AgendamentosVM(true);
        }

        private void SetaDataLabel()
        {
            try
            {
                var data = !string.IsNullOrEmpty(App.Current.DataCalendario)
                                                    ? Convert.ToDateTime(App.Current.DataCalendario).ToString("dd/MM/yyyy")
                                                    : DateTime.Now.Date.ToString("dd/MM/yyyy");

                DataLabel.Text = $"Pedidos em: {data}";
            }
            catch (Exception ex)
            {
                
            }
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
