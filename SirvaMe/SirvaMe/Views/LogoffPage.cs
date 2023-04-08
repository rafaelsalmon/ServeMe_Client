using System;
using SirvaMe.Models;
using SirvaMe.Repositorio;
using Xamarin.Forms;

namespace SirvaMe.Views
{
    /// <summary>
    /// Controls logging out users from Android and iOS
    /// </summary>
    public class LogoffPage : ContentPage
    {
        public LogoffPage()
        {
            Title = "Logout";

            if (Device.OS == TargetPlatform.iOS)
                EfetuarLogoutIos();
            else
                EfetuarLogoutAndroid();
        }

        private async void EfetuarLogoutAndroid()
        {
            try
            {
                if (await DisplayAlert("Sair", "Deseja sair da sua conta?", "Sim", "Não"))
                {
                    using (var repo = new AcessoDados<Sistema>()) repo.Update(new Sistema { Logged = false, ManyLogged = true });
                    App.Current.Logout();
                }
                else
                    App.Current.ShowMainPage();
            }
            catch (Exception e)
            {
                await DisplayAlert("Sair", "Ocorreu um erro!", "OK");
            }
        }

        private async void EfetuarLogoutIos()
        {
            try
            {
                using (var repo = new AcessoDados<Sistema>()) repo.Update(new Sistema { Logged = false, ManyLogged = true });
                App.Current.Logout();
            }
            catch (Exception e)
            {
                await DisplayAlert("Sair", "Ocorreu um erro!", "OK");
            }
        }
    }
}
