using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using SirvaMe.Models.GoogleApis;

namespace SirvaMe.Test
{
    [TestClass]
    public class Maps
    {
        [TestMethod]
        public async Task TestGoogleApis()
        {
            var client = new RestClient("https://maps.googleapis.com/maps/api/geocode/");
            var request = new RestRequest("json?address=Rua%20Frei%20Estanislau%20Schaette,%20526%20-%20%C3%81gua%20Verde%20-%20Blumenau%20/%20SC&components=country:BR", Method.GET);
            var response = await client.Execute<GoogleApisResult>(request);
            var endereco = response.Data;
        }

        [TestMethod]
        public async Task TestMethod1()
        {
            try
            {
                //var reverse = new ReverseGeocode();
                //var positions = await reverse.GetPositionsForAddressAsync("Brasil");

                //var geoCoder = new Geocoder();
                //var positions = "";

                //var approximateLocations = await geoCoder.GetPositionsForAddressAsync("Brasil");

                //foreach (var position in approximateLocations)
                //{
                //    positions += position.Latitude + ", " + position.Longitude + "\n";
                //}

                //var teste = positions;
            }
            catch (Exception e)
            {

            }
        }
    }
}
