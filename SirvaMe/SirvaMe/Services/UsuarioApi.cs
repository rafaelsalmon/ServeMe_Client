using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using SirvaMe.Models;
using SirvaMe.Models.GoogleApis;
using SirvaMe.Repositorio;
using SirvaMe.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SirvaMe.Services
{
    /// <summary>
    /// Cloud Api Client - api actions related to Users
    /// The cloud api contain all server-side services called by the mobile app
    /// </summary>
    public class UsuarioApi
    {
        private readonly string _urlApi;

        public UsuarioApi()
        {
            _urlApi = Application.Current.Resources["UrlApi"].ToString();
        }

        public enum Plataforma { Android = 1, iOS = 2 }

        public async Task AtualizaDadosPush(int idUsuario, Plataforma plataforma, string token)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("usuarios/atualiza_push/" + idUsuario, Method.PUT);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new
                {
                    PushPlataforma = (int)plataforma,
                    PushToken = token
                });
                var response = await client.Execute(request);
            }
            catch (Exception ex)
            {

            }
        }

        public Sistema GetSistema()
        {
            try
            {
                using (var repo = new AcessoDados<Sistema>()) return repo.GetSistema();
            }
            catch (Exception ex)
            {
                return new Sistema { Logged = false };
            }
        }

        public void InsereSistema(Sistema sistema)
        {
            try
            {
                using (var repo = new AcessoDados<Sistema>()) repo.Insert(sistema);
            }
            catch (Exception ex)
            {
            }
        }

        public void AtualizaSistema(Sistema sistema)
        {
            try
            {
                using (var repo = new AcessoDados<Sistema>()) repo.Update(sistema);
            }
            catch (Exception ex)
            {
            }
        }

        public Usuario InsereUsuarioNaBase(Usuario usuario)
        {
            try
            {
                using (var repo = new AcessoDados<Sistema>()) repo.Insert(new Sistema { Logged = true });
                using (var repo = new AcessoDados<Usuario>()) repo.Insert(usuario);

                return usuario;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Usuario> InsereUsuario(Usuario usuario)
        {
            try
            {
                var usuarioApi = await GravaPessoaNaApiAsync(usuario);
                using (var repo = new AcessoDados<Usuario>()) repo.Insert(usuarioApi);
                return usuarioApi;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Usuario AtualizaUsuarioNaBase(Usuario usuario)
        {
            try
            {
                using (var repo = new AcessoDados<Usuario>()) repo.Update(usuario);
                return usuario;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Usuario> AtualizaUsuario(Usuario usuario)
        {
            try
            {
                using (var repo = new AcessoDados<Usuario>()) repo.Update(usuario);

                if (!string.IsNullOrEmpty(usuario.FacebookToken))
                    return await GravaPessoaNaApiAsync(usuario);

                return usuario;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Usuario GetUsuarioNaBase()
        {
            try
            {
                using (var repo = new AcessoDados<Usuario>()) return repo.GetUsuario();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Usuario> GetUsuariosNaBase()
        {
            try
            {
                using (var repo = new AcessoDados<Usuario>()) return repo.Listar();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Facebook> CarregaDadoFacebookApiAsync(string token)
        {
            try
            {
                var client = new RestClient("https://graph.facebook.com/v2.6/");
                var request = new RestRequest("me?access_token=" + token + "&format=json&method=get&fields=id,name,email", Method.GET);
                var response = await client.Execute<Facebook>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Usuario> GravaPessoaNaApiAsync(Usuario usuario)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("pessoas", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(new { Pessoa = usuario });
                var response = await client.Execute<Usuario>(request);
                var resultado = response.Data;
                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Usuario> GetDadosDoUsuarioNaApiAsync(int usuarioId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("usuariospeloid/" + usuarioId, Method.GET);
                var response = await client.Execute<Usuario>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Usuario> GetDadosDoPrestadorNaApiAsync(int usuarioId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("dadosprestador/" + usuarioId, Method.GET);
                var response = await client.Execute<Usuario>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Endereco> PostCadastraEnderecoNaApiAsync(Endereco endereco, int usuarioId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("CadastrarEndereco/" + usuarioId, Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(endereco);
                var response = await client.Execute<Endereco>(request);
                var resultado = response.Data;
                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Endereco> PutAtualizaEnderecoNaApiAsync(Endereco endereco, int usuarioId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("AtualizarEndereco/" + usuarioId, Method.PUT);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(endereco);
                var response = await client.Execute<Endereco>(request);
                var resultado = response.Data;
                return resultado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Endereco> GetEnderecoPorPessoaIdNaApiAsync(int usuarioId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("ObtemEnderecoPorPessoaId/" + usuarioId, Method.GET);
                var response = await client.Execute<Endereco>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Endereco> GetEnderecoPorEnderecoIdNaApiAsync(int enderecoId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("ObtemEnderecoPorEnderecoId/" + enderecoId, Method.GET);
                var response = await client.Execute<Endereco>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<Endereco> GetEnderecoPeloCepNaApiAsync(string cep)
        {
            try
            {
                var client = new RestClient("http://correiosapi.apphb.com");
                var request = new RestRequest("cep/" + cep, Method.GET);
                var response = await client.Execute<Endereco>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Result> GetEnderecoFromGoogleApis(string latCliente, string lonCliente)
        {
            try
            {
                var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/");
                var request = new RestRequest($"json?latlng={latCliente},{lonCliente}&sensor=true", Method.GET);
                var response = await client.Execute<GoogleApisResult>(request);
                return response.Data.Results.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Location> GetPositionsFromGoogleApis(string endereco)
        {
            try
            {
                var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/");
                var request = new RestRequest($"json?address={endereco}&components=country:BR", Method.GET);
                var response = await client.Execute<GoogleApisResult>(request);
                return response.Data.Results.FirstOrDefault().geometry.Location;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Position> GetPositionsFromAddressAsync(string enderecoCompleto)
        {
            try
            {
                var reverse = new ReverseGeocode();
                var positions = await reverse.GetPositionsForAddressAsync(enderecoCompleto);

                if (positions.Latitude != 0) return positions;

                var api = await GetPositionsFromGoogleApis(enderecoCompleto);
                return new Position(api.Lat, api.Lng);
            }
            catch (Exception)
            {
                return new Position(0, 0);
            }
        }

        public async Task<List<PrestadorLocation>> GetPrestadoresPorTipoServicoIdNaApiAsync(int tipoServicoId, string latCliente, string lonCliente)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest($"ObtemPrestadoresPorTipoServicoId/{tipoServicoId}/{latCliente}/{lonCliente}", Method.GET);
                var response = await client.Execute<List<PrestadorLocation>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> PostAvaliaPrestadorNaApiAsync(Avaliacao avaliacao)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest("AvaliaPrestador/", Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(avaliacao);
                var response = await client.Execute(request);
                return response.IsSuccess;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Avaliacao>> GetAvaliacoesPrestadorNaApiAsync(int prestadorId)
        {
            try
            {
                var client = new RestClient(_urlApi);
                var request = new RestRequest($"ObtemAvaliacoesPrestador/{prestadorId}", Method.GET);
                var response = await client.Execute<List<Avaliacao>>(request);
                return response.Data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}