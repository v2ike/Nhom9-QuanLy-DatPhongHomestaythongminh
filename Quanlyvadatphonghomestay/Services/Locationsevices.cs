using Quanlyvadatphonghomestay.Models;
using System.Text.Json;

namespace Quanlyvadatphonghomestay.Services
{
    public class Locationsevices
    {
        private readonly HttpClient _httpClient;

        public Locationsevices()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5166/")
            };
        }

        public async Task<List<Locations>> GetLocationsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Locations");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();

                    throw new Exception(
                        $"API lỗi: {(int)response.StatusCode}\n{error}"
                    );
                    return new List<Locations>();
                }

                var json = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<List<Locations>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Locations>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi gọi API Locations: {ex.Message}");
                return new List<Locations>();
            }
        }
    }
}