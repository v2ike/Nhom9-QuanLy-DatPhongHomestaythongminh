using Quanlyvadatphonghomestay.Models;
using System.Net.Http.Json;

namespace Quanlyvadatphonghomestay.Services
{
    public class HomestayService
    {
        private readonly HttpClient _httpClient;

        public HomestayService()
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

        public async Task<List<Homestay>?> GetHomestaysAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Homestay>>("Homestays");
            }
            catch
            {
                return new List<Homestay>();
            }
        }
    }
}