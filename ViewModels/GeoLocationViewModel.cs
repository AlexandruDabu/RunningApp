using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GroopWebApp.ViewModels
{
    public class GeoLocationViewModel
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("country_name")]
        public string CountryName { get; set; }
        [JsonProperty("region_code")]
        public string RegionCode { get; set; }
        [JsonProperty("region_name")]
        public string RegionName { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }
        [JsonProperty("latitude")]
        public decimal Latitude { get; set; }
        [JsonProperty("longitude")]
        public decimal Longitude { get; set; }
    }
}