using System;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Service Provider
    /// </summary>
    public class PrestadorVM : BaseVM
    {
        private Propostas _usuario;
        public Propostas Usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
                RaisePropertyChanged("Usuario");
            }
        }

        public PrestadorVM(AgendamentoInfo agendamento)
        {
            try
            {
                CarregaDados();
            }
            catch (Exception ex)
            {
                IsEmpty = true;
            }
        }

        private async void CarregaDados()
        {
            try
            {
                var apiUsuario = new UsuarioApi();
                var prestador = await apiUsuario.GetDadosDoUsuarioNaApiAsync(2104);
                var teste = prestador;
            }
            catch (Exception e)
            {
                
            }
        }

        private void CarregaDados(AgendamentoInfo agendamento)
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    var apiUsuario = new UsuarioApi();
                    var prestador = await apiUsuario.GetDadosDoUsuarioNaApiAsync(agendamento.PrestadorId);

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
