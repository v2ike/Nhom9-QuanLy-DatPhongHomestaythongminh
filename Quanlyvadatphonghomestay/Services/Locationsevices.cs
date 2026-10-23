using System.Net.Http.Json;

namespace Quanlyvadatphonghomestay.Services
{
    public class Locationsevices
    {
        private readonly HttpClient _httpClient;

        public Locationsevices()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://10.0.2.2:49700/api/")
            };
        }

        public async Task<List<Quanlyvadatphonghomestay.Models.Locations>?> GetLocationsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Quanlyvadatphonghomestay.Models.Locations>>("Locations");
            }
            catch
            {
                return new List<Quanlyvadatphonghomestay.Models.Locations>();
            }
        }
    }
}