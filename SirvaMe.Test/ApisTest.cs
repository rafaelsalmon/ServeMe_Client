using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using SirvaMe.Models;
using SirvaMe.Services;

namespace SirvaMe.Test
{
    [TestClass]
    public class ApisTest
    {
        private readonly string _urlApi;
        private readonly string _urlApiHomolog;

        public ApisTest()
        {
            _urlApi = "http://srvme.azurewebsites.net/api/";
            _urlApiHomolog = "http://localhost:1972/api/";
        }

        [TestMethod]
        public void Teste()
        {
            var hora = DateTime.Now.Hour;
            var teste = hora.ToString("HH:mm");
        }

        [TestMethod]
        public async Task GetAvaliacoesPrestadorNaApiAsync()
        {
            try
            {
                const int usuarioId = 2104;

                var api = new UsuarioApi();
                var av = await api.GetAvaliacoesPrestadorNaApiAsync(usuarioId);

                //var client = new RestClient(_urlApi);
                //var request = new RestRequest("ObtemAvaliacoesPrestador/" + usuarioId, Method.GET);
                //var response = await client.Execute<List<Avaliacao>>(request);
                //var result = response.Data;
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public async Task GetUsuarioPorIdNaApiAsync()
        {
            try
            {
                const int usuarioId = 2104;

                var client = new RestClient(_urlApi);
                var request = new RestRequest("usuariospeloid/" + usuarioId, Method.GET);
                var response = await client.Execute<Usuario>(request);
                var result = response.Data;
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public async Task GetAgendamentosAtivosClientePorDataNaApiAsync()
        {
            try
            {
                const int clienteId = 7;
                var data = new DateTime(2016, 08, 15);

                var client = new RestClient(_urlApiHomolog);
                var request = new RestRequest($"ObtemAgendamentosAtivosClientePorData/{data}/{clienteId}", Method.GET);
                var response = await client.Execute<List<Agendamento>>(request);
                var result = response.Data;
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]
        public async Task GetPrestadoresPorTipoServicoIdNaApiAsync()
        {
            try
            {
                const int tipoServicoId = 2;
                const double latCliente = -26.9114602;
                const double lonCliente = -49.09354262;

                var client = new RestClient(_urlApi);
                var request = new RestRequest($"ObtemPrestadoresPorTipoServicoId/{tipoServicoId}/{latCliente}/{lonCliente}", Method.GET);
                var response = await client.Execute<List<PrestadorLocation>>(request);
                var result = response.Data;
            }
            catch (Exception ex)
            {
                
            }
        }

        [TestMethod]
        public async Task ObtemTermosDeUsoNaApiAsync()
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("ObtemTermosDeUso/1", Method.GET);
                var response = await client.Execute<string>(request);
                var resultado = response.Data;

                //Assert.IsNotNull(resultado.First().Logradouro);
            }
            catch (Exception e)
            {

            }
        }

        //[TestMethod]
        //public async Task GetPositionsForAddressAsync()
        //{
        //    try
        //    {
        //        //const string enderecoCompleto = "";

        //        //var reverse = new ReverseGeocode();
        //        //var positions = await reverse.GetPositionsForAddressAsync(enderecoCompleto);

        //        //Assert.IsTrue(positions.Latitude == 0, "Retornou 0");
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        [TestMethod]
        public async Task GetEnderecoPeloCepNaApiAsync()
        {
            try
            {
                var client = new RestClient("http://correiosapi.apphb.com");
                var request = new RestRequest("cep/" + "89037000", Method.GET);
                var response = await client.Execute<Endereco>(request);
                var resultado = response.Data;

                Assert.IsNotNull(resultado.Logradouro);
            }
            catch (Exception ex)
            {
                
            }
        }

        //[TestMethod]
        //public async Task TestaBuscaLogradouroPeloCep()
        //{
        //    var client = new RestClient("http://correiosapi.apphb.com");
        //    var request = new RestRequest("cep/" + "89037000", Method.GET);
        //    var response = await client.Execute<Endereco>(request);
        //    var endereco = response.Data;
        //}

        //[TestMethod]
        //public async Task GravaPessoaNaApiAsync()
        //{
        //    try
        //    {
        //        var pessoa = new Pessoa
        //        {
        //            FacebookToken = "12321",
        //            Nome = "Teste",
        //            Email = "teste@email.com"
        //        };

        //        var client = new RestClient(_urlApi);
        //        var request = new RestRequest("pessoas", Method.POST);
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddJsonBody(pessoa);
        //        var response = await client.Execute<Pessoa>(request);
        //        var resultado = response.Data;

        //        Assert.IsNotNull(resultado.Id);
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //}

        [TestMethod]
        public async Task GravaAgendamentoNaApiAsync()
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("agenda/1", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new Agendamento());
                var response = await client.Execute<Agendamento>(request);
                var resultado = response.Data;

                Assert.IsNotNull(resultado.Id);
            }
            catch (Exception e)
            {

            }
        }
    }
}
