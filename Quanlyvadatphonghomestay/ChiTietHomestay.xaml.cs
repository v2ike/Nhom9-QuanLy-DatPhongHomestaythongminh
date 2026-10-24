using System;
using System.Diagnostics;
using Microsoft.Maui.Controls;
using Quanlyvadatphonghomestay.Models;
using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay
{
    public partial class ChiTietHomestay : ContentPage
    {
        private readonly HomestayService _homestayService = new();

        public ChiTietHomestay()
        {
            InitializeComponent();
        }

        // Constructor nhận dữ liệu truyền từ TrangChu.xaml.cs
        public ChiTietHomestay(Homestay homestayData)
        {
            InitializeComponent();

            if (homestayData != null)
            {
                // 1. Gán ngay dữ liệu cơ bản để giao diện hiển thị tức thì (UI không bị trống)
                BindingContext = homestayData;

                // 2. Gọi API Backend để tải dữ liệu chi tiết (Rooms, Images, Amenities, Reviews từ SQL)
                if (homestayData.Id > 0)
                {
                    LoadHomestayDetailFromApi(homestayData.Id);
                }
            }
        }

        /// <summary>
        /// Hàm nạp chi tiết Homestay từ Web API Backend
        /// </summary>
        private async void LoadHomestayDetailFromApi(int homestayId)
        {
            try
            {
                var fullDetail = await _homestayService.GetHomestayDetailAsync(homestayId);
                if (fullDetail != null)
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        // Cập nhật lại BindingContext sau khi có dữ liệu đầy đủ từ Database SQL
                        BindingContext = fullDetail;
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi tải chi tiết Homestay từ API: {ex.Message}");
            }
        }

        /// <summary>
        /// Xử lý sự kiện bấm nút Quay lại (‹)
        /// </summary>
        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng nhấn nút "Chọn phòng" ở từng Item trong Danh sách phòng
        /// </summary>
        private async void OnSelectRoomClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var selectedRoom = button?.CommandParameter as Room;

            if (selectedRoom != null)
            {
                // TODO: Chuyển sang màn hình Đặt phòng / Chọn ngày Check-in Check-out cho Phòng đã chọn
                await DisplayAlert("Chọn phòng", $"Bạn đã chọn: {selectedRoom.RoomName}\nGiá: {selectedRoom.Price:N0}đ/đêm", "OK");

                // Mở trang Đặt phòng (khi tạo xong trang DatPhong):
                // await Navigation.PushAsync(new DatPhongPage(selectedRoom));
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi người dùng bấm nút "Đặt phòng" ở thanh cố định phía dưới (Bottom Bar)
        /// </summary>
        private async void OnBookingClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Thông báo", "Vui lòng chọn một phòng cụ thể ở danh sách \"Các phòng\" phía trên!", "OK");
        }
    }
}