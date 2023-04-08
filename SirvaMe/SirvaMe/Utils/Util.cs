using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SirvaMe.Models;
using SirvaMe.Models.GoogleApis;
using SirvaMe.Services;
using Xamarin.Forms;

namespace SirvaMe.Utils
{
    public class Util
    {
        public async Task<Endereco> CarregaEnderecoCompletoComPositionsAsync()
        {
            try
            {
                var geo = new Geolocator();
                var pos = await geo.GetPositions(50, 20000);
                if (pos == null || pos.Latitude == 0) return new Endereco();

                //Retornar Endereço do Google Apis
                //Get Address from Google Apis
                var api = new UsuarioApi();
                var enderecoGoogle = await api.GetEnderecoFromGoogleApis(pos.Latitude.ToString().Replace(",", "."), pos.Longitude.ToString().Replace(",", "."));
                return PopulaObjetoEnderecoFromGoogleResults(enderecoGoogle.address_components, new Endereco {Latitude = pos.Latitude, Longitude = pos.Longitude});
            }
            catch (Exception ex)
            {
                return new Endereco();
            }
        }

        private static Endereco PopulaObjetoEnderecoFromGoogleResults(List<AddressComponent> enderecoGoogle, Endereco endereco)
        {
            try
            {
                if (enderecoGoogle == null || enderecoGoogle.Count == 0) return new Endereco();

                endereco.Numero = enderecoGoogle[0].short_name;
                endereco.Logradouro = enderecoGoogle[1].short_name;
                endereco.Bairro = enderecoGoogle[2].short_name;
                endereco.Cidade = enderecoGoogle[3].short_name;
                endereco.Estado = enderecoGoogle[5].short_name;
                endereco.Cep = enderecoGoogle[7].short_name.Length == 5 ? $"{enderecoGoogle[7].short_name}000" : enderecoGoogle[7].short_name;

                return endereco;
            }
            catch (Exception ex)
            {
                return new Endereco();
            }
        }

        private static Endereco PopulaObjetoEnderecoFromGoogle(string enderecoCompleto, Endereco endereco)
        {
            try
            {
                if (string.IsNullOrEmpty(enderecoCompleto)) return new Endereco();

                var enderecoSplit = enderecoCompleto.Split(new char[] { ',', '-', '/', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                endereco.Logradouro = enderecoSplit[0];
                endereco.Numero = enderecoSplit[1].Trim();
                endereco.Bairro = enderecoSplit[2].Trim();
                endereco.Cidade = enderecoSplit[3].Trim();
                endereco.Estado = enderecoSplit[4].Trim();
                endereco.Cep = enderecoSplit[5].Trim();

                return endereco;
            }
            catch (Exception ex)
            {
                return new Endereco();
            }
        }

        private static Endereco PopulaObjetoEndereco(List<string> addressesList, Endereco endereco)
        {
            try
            {
                if (addressesList == null || addressesList.Count == 0) return new Endereco();

                var enderecoSplit = addressesList[0].Split(new char[] { ',', '-', '/', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                endereco.Logradouro = enderecoSplit[0];
                endereco.Numero = enderecoSplit[1].Trim();
                endereco.Bairro = enderecoSplit[2].Trim();
                endereco.Cidade = enderecoSplit[3].Trim();
                endereco.Estado = enderecoSplit[4].Trim();

                var cep = addressesList[4].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                endereco.Cep = cep[2].Trim().Replace("-", "");

                return endereco;
            }
            catch (Exception e)
            {
                return new Endereco();
            }
        }

        public static async void EfeitoBotao(Image sender)
        {
            var imageSender = sender;

            var b = imageSender.Bounds;
            b.X = b.X + 5;
            b.Y = b.Y + 5;
            await imageSender.LayoutTo(b, 100);

            b.X = b.X - 5;
            b.Y = b.Y - 5;
            await imageSender.LayoutTo(b, 100);
        }
    }
}
