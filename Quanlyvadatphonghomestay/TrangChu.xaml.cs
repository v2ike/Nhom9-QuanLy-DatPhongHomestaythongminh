using System.Text.Json;
using System.Collections.ObjectModel;
using Quanlyvadatphonghomestay.Services;
using Quanlyvadatphonghomestay.Models;

namespace Quanlyvadatphonghomestay;

public partial class TrangChu : ContentPage
{
    public ObservableCollection<Homestay> Homestays { get; set; } = new();
    private readonly HomestayService _homestayService;
    private readonly HttpClient _httpClient;
    public TrangChu()
	{
		InitializeComponent();
        _homestayService = new HomestayService();

        BindingContext = this;
    }
    private async Task LoadHomestays()
    {
        try
        {
            var data = await _homestayService.GetHomestays();

            Homestays.Clear();

            foreach (var homestay in data)
            {
                Homestays.Add(homestay);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Lỗi",
                ex.Message,
                "OK");
        }
    }
    private async void SearchEntry_Completed(
        object sender,
        EventArgs e)
    {
        try
        {
            string keyword = SearchEntry.Text;

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

            foreach (var homestay in data)
            {
                Homestays.Add(homestay);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Lỗi",
                ex.Message,
                "OK");
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

        ((CollectionView)sender).SelectedItem = null;

        await DisplayAlert(
            "Homestay",
            $"Bạn chọn: {homestay.Name}",
            "OK");
    }
}