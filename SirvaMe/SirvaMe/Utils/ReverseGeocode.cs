using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace SirvaMe.Utils
{
    public class ReverseGeocode
    {
        readonly Geocoder _geoCoder;

        public ReverseGeocode()
        {
            _geoCoder = new Geocoder();
        }

        public async Task<List<string>> GetAddressesFromPositionAsync(Position position)
        {
            try
            {
                var adresses = await _geoCoder.GetAddressesForPositionAsync(position);
                return adresses.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Position> GetPositionsForAddressAsync(string address)
        {
            try
            {
                var positions = await _geoCoder.GetPositionsForAddressAsync(address);
                var position = positions.First();
                return position;
            }
            catch (Exception ex)
            {
                return new Position(0,0);
            }
        }
    }
}
