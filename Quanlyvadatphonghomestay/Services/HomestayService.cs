using Quanlyvadatphonghomestay.Models;
using System.Diagnostics;
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

        // Lấy danh sách Homestay cho Trang Chủ
        public async Task<List<Homestay>?> GetHomestaysAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Homestay>>("Homestays");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi lấy danh sách Homestay: {ex.Message}");
                return new List<Homestay>();
            }
        }

        // BỔ SUNG: Lấy chi tiết Homestay kèm Rooms, Amenities, Reviews, Images theo ID
        public async Task<Homestay?> GetHomestayDetailAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Homestay>($"Homestays/{id}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi lấy chi tiết Homestay ID {id}: {ex.Message}");
                return null;
            }
        }

        // BỔ SUNG: Tìm kiếm Homestay theo từ khóa
        public async Task<List<Homestay>?> SearchHomestaysAsync(string keyword)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Homestay>>($"Homestays/search?keyword={Uri.EscapeDataString(keyword)}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi tìm kiếm Homestay: {ex.Message}");
                return new List<Homestay>();
            }
        }
    }
}