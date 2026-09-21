using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Json;
using Quanlyvadatphonghomestay.Services;
namespace Quanlyvadatphonghomestay.Services
{
    internal class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://10.0.2.2:5166/")
            };
        }

        public async Task<LoginResponse?> LoginAsync(
    string email,
    string password)
        {
            var request = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync(
                "api/Auth/login",
                request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Status: {response.StatusCode}\nAPI: {content}");
            }

            return System.Text.Json.JsonSerializer
                .Deserialize<LoginResponse>(content);
        }
    }
}
