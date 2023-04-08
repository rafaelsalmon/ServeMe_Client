using System;
using System.Threading.Tasks;
using SirvaMe.Interfaces;
using SirvaMe.Menu;
using SirvaMe.Services;
using SirvaMe.Views;
using Xamarin.Forms;

namespace SirvaMe
{
    public partial class App : Application, ILoginManager
    {
        #region Propriedades

        static ILoginManager _loginManager;

        public new static App Current;

        public static Action<string> PostSuccessFacebookAction { get; set; }
        public static Action<string> PostErrorFacebookAction { get; set; }

        public static double ScreenHeight;
        public static double ScreenWidth;

        public static int Rating { get; set; }

        public bool Logged
        {
            get
            {
                if (Properties.ContainsKey("Logged"))
                    return (bool)Properties["Logged"];

                return false;
            }
            set
            {
                Properties["Logged"] = value;
                SavePropertiesAsync();
            }
        }

        public int UserID
        {
            get
            {
                if (Properties.ContainsKey("UserID"))
                    return (int)Properties["UserID"];
                else
                    return 0;
            }
            set
            {
                Properties["UserID"] = value;
                SavePropertiesAsync();
            }
        }

        public string UserName
        {
            get
            {
                return Properties.ContainsKey("UserName") ? Properties["UserName"].ToString() : "";
            }
            set
            {
                Properties["UserName"] = value;
                SavePropertiesAsync();
            }
        }

        public string UserFacebookID
        {
            get
            {
                return Properties.ContainsKey("UserFacebookID") ? Properties["UserFacebookID"].ToString() : "";
            }
            set
            {
                Properties["UserFacebookID"] = value;
                SavePropertiesAsync();
            }
        }

        public string PushToken
        {
            get
            {
                return Properties.ContainsKey("PushToken") ? Properties["PushToken"].ToString() : "";
            }
            set
            {
                Properties["PushToken"] = value;
                SavePropertiesAsync();
            }
        }

        public string DataCalendario
        {
            get
            {
                return Properties.ContainsKey("DataCalendario") ? Properties["DataCalendario"].ToString() : "";
            }
            set
            {
                Properties["DataCalendario"] = value;
                SavePropertiesAsync();
            }
        }

        public bool RefreshData
        {
            get
            {
                if (Properties.ContainsKey("RefreshData"))
                    return (bool)Properties["RefreshData"];

                return false;
            }
            set
            {
                Properties["RefreshData"] = value;
                SavePropertiesAsync();
            }
        }

        public bool RecebeuPush
        {
            get
            {
                if (Properties.ContainsKey("RecebeuPush"))
                    return (bool)Properties["RecebeuPush"];

                return false;
            }
            set
            {
                Properties["RecebeuPush"] = value;
                SavePropertiesAsync();
            }
        }

        #endregion

        public App()
        {
            InitializeComponent();
            Current = this;
        }

        public void ShowMainPage()
        {
            MainPage = new RootPage(new ServicosListaPage());
        }

        public void ShowMainPage(Page page)
        {
            MainPage = new RootPage(page);
        }

        public void ShowPerfilPage()
        {
            MainPage = new RootPage(new MeuPerfilPage());
        }

        public void Logout()
        {
            //Properties.Remove("UserID");
            DependencyService.Get<ILoginManager>().Logout(); //Facebook
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //MainPage = new RootPage(new TermosDeUsoPage(new Models.AgendamentoInfo()));
            //MainPage = new RootPage(new ProfissionalPerfilPage(new Models.Propostas { Prestador = new Models.Usuario { Nome = "Juvenal", Nota = 5 } }, new Models.AgendamentoInfo { PrestadorId = 7 }, false));

            var api = new UsuarioApi();
            var sistema = api.GetSistema();
            var usuario = api.GetUsuarioNaBase();

            if (sistema != null && sistema.Logged && usuario.Id > 0)
            {
                Current.UserID = usuario.Id;
                Current.UserName = usuario.Nome;
                Current.UserFacebookID = usuario.FacebookToken;
                Current.PushToken = sistema.PushToken ?? usuario.PushTokenCliente;

                if (!string.IsNullOrEmpty(usuario.FacebookToken) &&
                    !string.IsNullOrEmpty(usuario.PushTokenCliente) &&
                    !string.IsNullOrEmpty(sistema.PushToken) &&
                    sistema.PushToken != usuario.PushTokenCliente)
                {
                    usuario.PushTokenCliente = sistema.PushToken;
                    Task.Run(async () => { await api.GravaPessoaNaApiAsync(usuario); });
                }
                MainPage = new RootPage(new ServicosListaPage());
            }
            else
                MainPage = new LoginPage();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}