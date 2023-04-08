using System;
using Plugin.Connectivity;
using SirvaMe.Aplicacao;
using SirvaMe.Services;

namespace SirvaMe.Views
{
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();

            try
            {
                LoadingStackLayout.IsVisible = false;
                
                if (!CrossConnectivity.Current.IsConnected)
                {
                    SemConexaoLabel.IsVisible = true;
                    BtnLogin.IsVisible = false;
                    LblInfoLogin.Text = "Verifique sua conexão e tente novamente!";
                }
                
                App.PostSuccessFacebookAction = async token =>
                {
                    LoadingStackLayout.IsVisible = true;
                    BtnLogin.IsVisible = false;
                    LblInfoLogin.IsVisible = false;

                    var app = new LoginApp();
                    var api = new UsuarioApi();
                    var sistema = api.GetSistema();
                    var facebook = await app.CarregaDadosFacebookESalva(token, sistema.PushToken);

                    if (facebook != null)
                    {
                        App.Current.UserFacebookID = facebook.id;
                        App.Current.UserName = facebook.name;
                        App.Current.Logged = true;

                        if (sistema != null && !sistema.ManyLogged)
                            App.Current.ShowPerfilPage();
                        else
                            App.Current.ShowMainPage();
                    }
                    else
                        await DisplayAlert("Login", "Falha ao efetuar login no Facebook!", "OK");

                    LoadingStackLayout.IsVisible = false;
                    BtnLogin.IsVisible = true;
                    LblInfoLogin.IsVisible = true;
                };

                App.PostErrorFacebookAction = async error =>
                {
                    await DisplayAlert("Falha ao efetuar login no Facebook!", error, "OK");
                };
            }
            catch (Exception e)
            {
                DisplayAlert("Login", "Falha ao efetuar login no Facebook!", "OK");
            }
        }
    }
}