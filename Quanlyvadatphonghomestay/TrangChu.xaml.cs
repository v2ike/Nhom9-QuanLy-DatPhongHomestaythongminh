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

        public ObservableCollection<Locations> Locations { get; set; } = new ObservableCollection<Locations>();

        private readonly HomestayService _homestayService;
        private readonly Locationsevices _locationsevices;

        public TrangChu()
        {
            InitializeComponent();

            _homestayService = new HomestayService();
            _locationsevices = new Locationsevices();

            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.WhenAll(
                LoadHomestays(),
                LoadLocations()
            );
        }

        private async Task LoadHomestays()
        {
            try
            {
                var data = await _homestayService.GetHomestays();

                Homestays.Clear();

                if (data != null)
                {
                    foreach (var homestay in data)
                    {
                        Homestays.Add(homestay);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(
                    "Lỗi Homestay",
                    ex.Message,
                    "OK"
                );
            }
        }

        private async Task LoadLocations() { 
            try 
            { var data = await _locationsevices.GetLocationsAsync();
                await DisplayAlert(
            "Kiểm tra",
            $"Số location nhận được: {data?.Count ?? 0}",
            "OK"
        );
                Locations.Clear(); 
                if (data != null) 
                { 
                    foreach (Quanlyvadatphonghomestay.Models.Locations locatio in data) 
                    { 
                        Locations.Add(locatio); 
                    } 
                } 
            } 
            catch (Exception ex) 
            { 
                await DisplayAlert("Lỗi Location", ex.Message, "OK"); 
            } 
        }

        private async void SearchEntry_Completed(
            object sender,
            EventArgs e)
        {
            try
            {
                string keyword = (sender as Entry)?.Text;

                List<Homestay> data;

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    data = await _homestayService.GetHomestays();
                }
                else
                {
                    data = await _homestayService.SearchHomestays(keyword);
                }

                Homestays.Clear();

                if (data != null)
                {
                    foreach (var homestay in data)
                    {
                        Homestays.Add(homestay);
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert(
                    "Lỗi Tìm Kiếm",
                    ex.Message,
                    "OK"
                );
            }
        }

        private async void Homestay_SelectionChanged(
            object sender,
            SelectionChangedEventArgs e)
        {
            var homestay =
                e.CurrentSelection.FirstOrDefault() as Homestay;

            if (homestay == null)
                return;

            if (sender is CollectionView collectionView)
            {
                collectionView.SelectedItem = null;
            }

            await DisplayAlert(
                "Homestay",
                $"Bạn chọn: {homestay.Name}",
                "OK"
            );
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD

        

        private void chuyentimkiem(object sender, TappedEventArgs e)
        {

        }
=======
>>>>>>> e14679408b0c635b74f8737b80017ff0a103a869
>>>>>>> 726fc91e1a92d2ba766659457a8a3be28a60e6e8
    }
}