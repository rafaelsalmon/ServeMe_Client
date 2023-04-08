using System;
using System.ComponentModel;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// User
    /// </summary>
    public class UsuarioVM : BaseVM
    {
        private Usuario _usuario;

        public Usuario Usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
                RaisePropertyChanged("Usuario");
            }
        }

        private Endereco _endereco;

        public Endereco Endereco
        {
            get { return _endereco; }
            set
            {
                _endereco = value;
                RaisePropertyChanged("Endereco");
            }
        }

        public UsuarioVM()
        {
            try
            {
                this.Usuario = new Usuario();
                this.Endereco = new Endereco();

                CarregaDadosDoUsuario();
                CarregaEnderecoDoUsuario();
            }
            catch (Exception e)
            {
                IsEmpty = true;
            }
        }

        private void CarregaDadosDoUsuario()
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    var api = new UsuarioApi();
                    var usuario = await api.GetDadosDoUsuarioNaApiAsync(App.Current.UserID);
                    //var teste = usuario;

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (usuario != null)
                            this.Usuario = usuario;
                        else
                            this.Usuario.Nome = App.Current.UserName;
                    });
                }
                catch (Exception ex)
                {
                    IsEmpty = true;
                }
                IsBusy = false;
            });
        }

        private void CarregaEnderecoDoUsuario()
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    var api = new UsuarioApi();
                    var endereco = await api.GetEnderecoPorPessoaIdNaApiAsync(App.Current.UserID);
                    //var teste = endereco;

                    if (endereco != null) Device.BeginInvokeOnMainThread(() => { this.Endereco = endereco; });
                }
                catch (Exception ex)
                {
                    IsEmpty = true;
                }
                IsBusy = false;
            });
        }
    }
}
