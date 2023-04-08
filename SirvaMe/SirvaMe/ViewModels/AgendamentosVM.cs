using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SirvaMe.Models;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.ViewModels
{
    /// <summary>
    /// Appointments
    /// </summary>
    public class AgendamentosVM : BaseVM
    {
        private ObservableCollection<AgendamentoInfo> _agendamentosDisponiveis;

        public ObservableCollection<AgendamentoInfo> AgendamentosDisponiveis
        {
            get { return _agendamentosDisponiveis; }
            set
            {
                _agendamentosDisponiveis = value;
                RaisePropertyChanged("AgendamentosDisponiveis");
            }
        }

        private ICommand _forceRefreshCommandActive;
        public ICommand ForceRefreshCommandActive => _forceRefreshCommandActive ?? (_forceRefreshCommandActive = new Command(CarregaAgendamentosAtivos));

        private ICommand _forceRefreshCommandInactive;
        public ICommand ForceRefreshCommandInactive => _forceRefreshCommandInactive ?? (_forceRefreshCommandInactive = new Command(CarregaAgendamentosHistorico));

        private void CarregaAgendamentosAtivos(object obj)
        {
            CarregaAgendamentosDisponiveis(true);
        }

        private void CarregaAgendamentosHistorico(object obj)
        {
            CarregaAgendamentosDisponiveis(false);
        }

        public AgendamentosVM(bool ativos)
        {
            try
            {
                CarregaAgendamentosDisponiveis(ativos);
            }
            catch (Exception ex)
            {
                IsEmpty = true;
            }
        }

        private void CarregaAgendamentosDisponiveis(bool ativos)
        {
            Task.Run(async () =>
            {
                try
                {
                    IsBusy = true;

                    var apiServicos = new ServicosApi();
                    ObservableCollection<Agendamento> agendamentos;

                    if (ativos)
                    {
                        var data = DateTime.Now.ToString("yyyy-MM-dd");

                        if (!string.IsNullOrEmpty(App.Current.DataCalendario))
                        {
                            data = Convert.ToDateTime(App.Current.DataCalendario).ToString("yyyy-MM-dd");
                        }
                        agendamentos = await apiServicos.GetAgendamentosAtivosPorDataNaApiAsync(data, App.Current.UserID);
                    }
                    else
                        agendamentos = await apiServicos.GetAgendamentosHistoricoNaApiAsync(App.Current.UserID);

                    if (agendamentos != null && agendamentos.Count > 0)
                    {
                        AgendamentosDisponiveis = new ObservableCollection<AgendamentoInfo>();
                        var apiUsuario = new UsuarioApi();

                        var agendamentosOrderBy = ativos
                                                    ? new List<Agendamento>(agendamentos.OrderBy(a => a.DataHoraInicio))
                                                    : new List<Agendamento>(agendamentos.OrderByDescending(a => a.DataHoraInicio));

                        foreach (var agendamento in agendamentosOrderBy)
                        {
                            var tipoServico = await apiServicos.GetTipoServicoPorIdNaApiAsync(agendamento.TipoServicoId);
                            var endereco = await apiUsuario.GetEnderecoPorEnderecoIdNaApiAsync(agendamento.EnderecoId);
                            var propostas = await apiServicos.GetAgendamentoPropostasClienteNaApiAsync(agendamento.Id);

                            if (tipoServico == null || endereco == null) continue;

                            var titulo = agendamento.Titulo.Length > 25 ? $"{agendamento.Titulo.Substring(0, 25)}..." : agendamento.Titulo;

                            AgendamentosDisponiveis.Add(new AgendamentoInfo
                            {
                                Id = agendamento.Id,
                                PrestadorId = agendamento.PrestadorId,
                                TipoServico = tipoServico.Nome,
                                DataHoraInicio = agendamento.DataHoraInicio,
                                DataHoraTexto = $"{agendamento.DataHoraInicio.ToString("d")} | {agendamento.DataHoraInicio.ToString("HH:mm")}hs",
                                Titulo = $"{agendamento.Id} - {titulo}",
                                Descricao = agendamento.Descricao,
                                Endereco = endereco,
                                EnderecoCompleto = new Endereco().RetornaEnderecoCompleto(endereco),
                                AgendamentoPrestador = propostas,
                                Status = RetornaStatus(agendamento, propostas)
                            });
                        }
                        IsEmpty = AgendamentosDisponiveis.Count == 0;
                    }
                    else
                        IsEmpty = true;
                }
                catch (Exception e)
                {
                    IsEmpty = true;
                }
                IsBusy = false;
            });
        }

        private static string RetornaStatus(Agendamento agendamento, List<AgendamentoPrestador> propostas)
        {
            try
            {

                if (agendamento.ServicoConfirmadoCliente) return "SERVIÇO CONCLUÍDO PELO CLIENTE";
                if (agendamento.ServicoConfirmadoPrestador) return "SERVIÇO CONCLUÍDO PELO PRESTADOR";

                if (agendamento.DataHoraInicio < DateTime.Now) return "EXPIRADO. AGENDE NOVAMENTE";
                if (agendamento.AgendamentoCanceladoCliente || agendamento.AgendamentoCanceladoPrestador) return "AGENDAMENTO CANCELADO";
                if (agendamento.PrestadorId > 0) return "PROFISSIONAL ESCOLHIDO,  AGUARDE A CHEGADA DO PRESTADOR";
                return propostas.Count > 0 ? "PROPOSTAS RECEBIDAS" : "AGUARDANDO PROPOSTAS";
            }
            catch (Exception e)
            {
                return "AGUARDANDO PROPOSTAS";
            }
        }
    }
}
