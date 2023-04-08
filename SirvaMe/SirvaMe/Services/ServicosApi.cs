using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using SirvaMe.Models;
using SirvaMe.ViewModels;
using Xamarin.Forms;

namespace SirvaMe.Services
{
    /// <summary>
    /// Cloud Api Client - api actions related to Services
    /// The cloud api contain all server-side services called by the mobile app
    /// </summary>
    public class ServicosApi
    {
        private readonly string _urlApi;

        public ServicosApi()
        {
            _urlApi = Application.Current.Resources["UrlApi"].ToString();
        }

        public async Task<List<Servicos>> GetServicosNaApiAsync()
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("servicos", Method.GET);
                var response = await client.Execute<List<Servicos>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Servicos> GetTipoServicoPorIdNaApiAsync(int tipoServicoId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("ObtemTipoServicoPorId/" + tipoServicoId, Method.GET);
                var response = await client.Execute<Servicos>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<Agendamento> PostCriaAgendamentoNaApiAsync(Agendamento agendamento)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("CriaAgendamento/", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(agendamento);
                var response = await client.Execute<Agendamento>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ObservableCollection<Agendamento>> GetAgendamentosAtivosNaApiAsync(int clienteId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("ObtemAgendamentosAtivosCliente/" + clienteId, Method.GET);
                var response = await client.Execute<ObservableCollection<Agendamento>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ObservableCollection<Agendamento>> GetAgendamentosHistoricoNaApiAsync(int clienteId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("ObtemAgendamentosHistoricoCliente/" + clienteId, Method.GET);
                var response = await client.Execute<ObservableCollection<Agendamento>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ObservableCollection<Agendamento>> GetAgendamentosAtivosPorDataNaApiAsync(string data, int clienteId)
        {
            try
            {
                var client = new RestClient(Application.Current.Resources["UrlApi"].ToString());
                var request = new RestRequest($"ObtemAgendamentosAtivosClientePorData/{data}/{clienteId}", Method.GET);
                var response = await client.Execute<ObservableCollection<Agendamento>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ObservableCollection<Agendamento>> GetAgendamentosHistoricoPorDataNaApiAsync(string data, int clienteId)
        {
            try
            {
                var client = new RestClient(Application.Current.Resources["UrlApi"].ToString());
                var request = new RestRequest($"ObtemAgendamentosHistoricoClientePorData/{data}/{clienteId}", Method.GET);
                var response = await client.Execute<ObservableCollection<Agendamento>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<AgendamentoPrestador>> GetAgendamentoPropostasClienteNaApiAsync(int agendamentoId)
        {
            try
            {
                var client = new RestClient(Application.Current.Resources["UrlApi"].ToString());
                var request = new RestRequest($"ObtemAgendamentoPropostasCliente/{agendamentoId}", Method.GET);
                var response = await client.Execute<List<AgendamentoPrestador>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AgendamentoPrestador> GetAgendamentoPropostaPrestadorNaApiAsync(int agendamentoId, int prestadorId)
        {
            try
            {
                var client = new RestClient(Application.Current.Resources["UrlApi"].ToString());
                var request = new RestRequest($"ObtemAgendamentoPropostaPrestador/{agendamentoId}/{prestadorId}", Method.GET);
                var response = await client.Execute<AgendamentoPrestador>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AgendamentoPrestador> PostClienteAceitaPrestadorNaApiAsync(AgendamentoPrestador agendamentoPrestador)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("ClienteAceitaPrestador/", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(agendamentoPrestador);
                var response = await client.Execute<AgendamentoPrestador>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> PutClienteConfirmaAgendamentoPrestadorNaApiAsync(int agendamentoId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("clienteconfirmaservico/" + agendamentoId, Method.PUT);
                var response = await client.Execute(request);
                return response.IsSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> PutClienteCancelaAgendamentoNaApiAsync(int agendamentoId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("clientecancelaagendamento/" + agendamentoId, Method.PUT);
                var response = await client.Execute(request);
                return response.IsSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<string> GetTermosDeUsoNaApiAsync(int tipoId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("ObtemTermosDeUso/" + tipoId, Method.GET);
                var response = await client.Execute<string>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region MOCKS

        public async Task<List<Servicos>> GetServicosMock()
        {
            try
            {
                return await Task.Run(() => new List<Servicos>
                {
                    new Servicos {Id = 1, Nome = "Eletricista", LinkFoto = ""},
                    new Servicos {Id = 2, Nome = "Esteticista", LinkFoto = ""},
                    new Servicos {Id = 3, Nome = "Marceneiro", LinkFoto = ""},
                    new Servicos {Id = 4, Nome = "Mecânico", LinkFoto = ""},
                    new Servicos {Id = 5, Nome = "Pedreiro", LinkFoto = ""}
                });
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion
    }
}
