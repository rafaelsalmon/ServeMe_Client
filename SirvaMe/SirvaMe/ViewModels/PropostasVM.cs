using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Proposals (Quotes)
    /// </summary>
    public class PropostasVM : BaseVM
    {
        private ObservableCollection<Propostas> _propostas;

        public ObservableCollection<Propostas> Propostas
        {
            get { return _propostas; }
            set
            {
                _propostas = value;
                RaisePropertyChanged("Propostas");
            }
        }

        public PropostasVM(AgendamentoInfo agendamento)
        {
            try
            {
                this.Propostas = new ObservableCollection<Propostas>();
                CarregaPropostas(agendamento);
            }
            catch (Exception ex)
            {
                IsEmpty = true;
            }
        }

        private void CarregaPropostas(AgendamentoInfo agendamento)
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    var api = new ServicosApi();
                    var propostas = await api.GetAgendamentoPropostasClienteNaApiAsync(agendamento.Id);

                    if (propostas != null && propostas.Count > 0)
                    {
                        //var propostasAceitas = from a in propostas where a.AgendamentoStatus.Equals(AgendamentoPrestador.AceiteRecusa.Aceitou) select a;

                        var apiUsuario = new UsuarioApi();

                        foreach (var proposta in propostas)
                        {
                            var prestador = await apiUsuario.GetDadosDoUsuarioNaApiAsync(proposta.PrestadorId);

                            var avatar = !string.IsNullOrEmpty(prestador.FacebookToken)
                                            ? $"https://graph.facebook.com/{prestador.FacebookToken}/picture?type=large"
                                            : "icon_profile2.png";

                            Propostas.Add(new Propostas
                            {
                                AgendamentoId = agendamento.Id,
                                TipoServicoId = agendamento.TipoServicoId,
                                TipoServico = agendamento.TipoServico,
                                Avatar = avatar,
                                Prestador = prestador
                            });
                        }
                    }
                    else
                        IsEmpty = true;
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
