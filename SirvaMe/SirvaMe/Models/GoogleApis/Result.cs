using System.Collections.Generic;

namespace SirvaMe.Models.GoogleApis
{
    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
    }
}
