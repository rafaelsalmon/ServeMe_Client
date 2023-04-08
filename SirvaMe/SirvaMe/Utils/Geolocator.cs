using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;

namespace SirvaMe.Utils
{
    public class Geolocator
    {
        public async Task<Position> GetPositions(int accuracy, int timeout)
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = accuracy;
                var pos = await locator.GetPositionAsync(timeout);
                if (pos != null && pos.Latitude != 0) return pos;

                locator.DesiredAccuracy = 80;
                return await locator.GetPositionAsync(40000);
            }
            catch (Exception ex)
            {
                return new Position();
            }
        }
    }
}

