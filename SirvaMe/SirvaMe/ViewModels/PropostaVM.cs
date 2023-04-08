using System;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Services;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Proposal
    /// </summary>
    public class PropostaVM : BaseVM
    {
        private Propostas _proposta;
        public Propostas Proposta
        {
            get { return _proposta; }
            set
            {
                _proposta = value;
                RaisePropertyChanged("Proposta");
            }
        }

        public PropostaVM(AgendamentoInfo agendamento)
        {
            try
            {
                this.Proposta = new Propostas();
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

                    var apiUsuario = new UsuarioApi();
                    var prestador = await apiUsuario.GetDadosDoUsuarioNaApiAsync(agendamento.PrestadorId);

                    var avatar = !string.IsNullOrEmpty(prestador.FacebookToken)
                                    ? $"https://graph.facebook.com/{prestador.FacebookToken}/picture?type=large"
                                    : "icon_profile2.png";

                    Proposta = new Propostas
                    {
                        AgendamentoId = agendamento.Id,
                        TipoServicoId = agendamento.TipoServicoId,
                        TipoServico = agendamento.TipoServico,
                        Avatar = avatar,
                        Prestador = prestador
                    };
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
