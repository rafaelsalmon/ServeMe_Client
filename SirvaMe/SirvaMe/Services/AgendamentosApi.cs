using System;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using SirvaMe.Models;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace SirvaMe.Services
{
    /// <summary>
    /// Cloud Api Client - api actions related to Scheduling Appointments
    /// The cloud api contain all server-side services called by the mobile app
    /// </summary>
    public class AgendamentosApi
    {
        private readonly string _urlApi;

        public AgendamentosApi()
        {
            _urlApi = Application.Current.Resources["UrlApi"].ToString();
        }
        
        public async Task<ObservableCollection<Agendamento>> GetAgendamentosNaApiAsync(int clienteId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("obtemagendamentos/" + clienteId, Method.GET);
                var response = await client.Execute<ObservableCollection<Agendamento>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
