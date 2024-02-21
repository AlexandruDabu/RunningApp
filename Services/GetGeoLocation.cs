using GroopWebApp.Interfaces;

namespace GroopWebApp.Services
{
    public class GetGeoLocation : IGeoLocation
    {
        private readonly HttpClient _httpClient;
        public GetGeoLocation(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetGeoInfo()
        {
            var ipAddress = await GetIpAddress();
            var response = await _httpClient.GetAsync($"http://api.ipstack.com/" 
            + ipAddress + "?access_key=f621df86a23be1080b48b19dee86ad7c");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return json;
            }
            return null;
        }

        public async Task<string> GetIpAddress()
        {
            var ipAddressResponse = await _httpClient.GetAsync($"http://ipinfo.io/ip");
            if(ipAddressResponse.IsSuccessStatusCode)
            {
                var json = await ipAddressResponse.Content.ReadAsStringAsync();
                return json.ToString();
            }
            return "";
        }
    }
}