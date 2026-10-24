using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace Quanlyvadatphonghomestay.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://10.0.2.2:49700/api/"),
                Timeout = TimeSpan.FromSeconds(10)
            };
        }

        public async Task<LoginResponse?> LoginAsync(string email, string password)
        {
            try
            {
                var request = new LoginRequest
                {
                    Email = email,
                    Password = password
                };

                var response = await _httpClient.PostAsJsonAsync("Auth/login", request);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Lỗi Server ({response.StatusCode}): {content}");
                }

                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Lưu thông tin người dùng vào Preferences sau khi đăng nhập thành công
                if (loginResponse != null && loginResponse.UserId > 0)
                {
                    Preferences.Default.Set("UserId", loginResponse.UserId);
                    Preferences.Default.Set("FullName", loginResponse.FullName ?? "");
                    Preferences.Default.Set("Email", loginResponse.Email ?? "");
                    Preferences.Default.Set("Role", loginResponse.Role ?? "");
                }

                return loginResponse;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Lỗi mạng HTTP: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> RegisterAsync(string fullName, string email, string password)
        {
            try
            {
                var request = new RegisterRequest
                {
                    FullName = fullName,
                    Email = email,
                    Password = password,
                    Role = "customer"
                };

                var response = await _httpClient.PostAsJsonAsync("Auth/register", request);
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Lỗi Server ({response.StatusCode}): {content}");
                }

                return true;
            }
            catch (HttpRequestException httpEx)
            {
                throw new Exception($"Lỗi mạng HTTP: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Users/{userId}");
                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch
            {
                return null;
            }
        }

        public void Logout()
        {
            Preferences.Default.Remove("UserId");
            Preferences.Default.Remove("FullName");
            Preferences.Default.Remove("Email");
            Preferences.Default.Remove("Role");
        }

        public int GetCurrentUserId()
        {
            return Preferences.Default.Get("UserId", 0);
        }
    }

    // Chỉ giữ lại các class chưa có file riêng
    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "customer";
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}