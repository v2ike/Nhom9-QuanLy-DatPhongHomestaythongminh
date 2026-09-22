using Quanlyvadatphonghomestay.Models;
using System.Text.Json;
using System.Net.Http.Json;
namespace Quanlyvadatphonghomestay.Services
{
    
    internal class HomestayService
    {
        private readonly HttpClient _httpClient;
        public HomestayService()
        {
#if ANDROID
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5166/")
            };
#else
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5166/")
            };
#endif
        }

        public async Task<List<Homestay>> GetHomestays()
        {
            var response = await _httpClient.GetAsync("api/Homestays");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Homestay>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })
                ?? new List<Homestay>();
        }

        public async Task<List<Homestay>> SearchHomestays(string keyword)
        {
            var url =
                $"api/Homestays/search?keyword={Uri.EscapeDataString(keyword)}";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Homestay>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })
                ?? new List<Homestay>();
        }

        public async Task<Homestay> GetHomestay(int id)
        {
            var response =
                await _httpClient.GetAsync($"api/Homestays/{id}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Homestay>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
