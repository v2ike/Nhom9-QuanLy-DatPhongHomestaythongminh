using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Quanlyvadatphonghomestay.Models;
using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay
{
    public partial class TrangChu : ContentPage
    {
        public ObservableCollection<Homestay> Homestays { get; set; } = new();
        public ObservableCollection<Locations> Locations { get; set; } = new();

        private readonly HomestayService _homestayService;
        private readonly Locationsevices _locationsevices;

        // Constructor dùng cho Dependency Injection
        public TrangChu(HomestayService homestayService, Locationsevices locationsevices)
        {
            InitializeComponent();

            _homestayService = homestayService;
            _locationsevices = locationsevices;

            BindingContext = this;
        }

        // Constructor mặc định dự phòng
        public TrangChu() : this(new HomestayService(), new Locationsevices())
        {
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Load đồng thời cả Homestays và Locations
            await Task.WhenAll(
                LoadHomestays(),
                LoadLocations()
            );
        }

        private async Task LoadHomestays()
        {
            try
            {
                var data = await _homestayService.GetHomestaysAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Homestays.Clear();
                    if (data != null && data.Any())
                    {
                        foreach (var homestay in data)
                        {
                            Homestays.Add(homestay);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi Homestay", ex.Message, "OK");
            }
        }

        private async Task LoadLocations()
        {
            try
            {
                var data = await _locationsevices.GetLocationsAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Locations.Clear();
                    if (data != null && data.Any())
                    {
                        // Chỉ định rõ kiểu Quanlyvadatphonghomestay.Models.Locations thay vì dùng 'var'
                        foreach (Quanlyvadatphonghomestay.Models.Locations location in data)
                        {
                            Locations.Add(location);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi Location", ex.Message, "OK");
            }
        }

        private async void SearchEntry_Completed(object sender, EventArgs e)
        {

        }

        // ĐÃ CẬP NHẬT: TRUYỀN ĐỐI TƯỢNG HOMESTAY SANG CHITIETHOMESTAY
        private async void Homestay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedHomestay = e.CurrentSelection.FirstOrDefault() as Homestay;

            if (selectedHomestay == null)
                return;

            if (sender is CollectionView collectionView)
            {
                collectionView.SelectedItem = null;
            }

            // Truyền dữ liệu sang ChiTietHomestay qua Constructor
            await Navigation.PushAsync(new ChiTietHomestay(selectedHomestay));
        }

        private void chuyentimkiem(object sender, TappedEventArgs e)
        {
            // Xử lý sự kiện khi bấm vào thanh tìm kiếm nếu cần
        }

        private async void chuyen_thongtincanhan(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new ThongTinCaNhan());
        }
    }
}