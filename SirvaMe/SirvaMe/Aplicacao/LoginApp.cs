using System;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.Aplicacao
{
    public class LoginApp
    {
        public async Task<Facebook> CarregaDadosFacebookESalva(string facebookToken, string pushToken)
        {
            try
            {
                var api = new UsuarioApi();
                var facebook = await api.CarregaDadoFacebookApiAsync(facebookToken);

                if (facebook != null)
                {
                    try
                    {
                        var usuario = api.GetUsuarioNaBase();

                        if (usuario == null)
                        {
                            var resultado = await api.InsereUsuario(new Usuario
                            {
                                FacebookToken = facebook.id,
                                Nome = facebook.name,
                                EmailCliente = facebook.email,
                                PushTokenCliente = pushToken,
                                PushPlataformaCliente = (int)(Device.OS == TargetPlatform.iOS ? UsuarioApi.Plataforma.iOS : UsuarioApi.Plataforma.Android),
                                DataNascimento = null
                            });
                            App.Current.UserID = resultado.Id;
                        }
                        else
                            App.Current.UserID = usuario.Id;

                        api.AtualizaSistema(new Sistema { Logged = true, ManyLogged = true });
                    }
                    catch (Exception e)
                    {

                    }
                    return facebook;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
