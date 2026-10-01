using Quanlyvadatphonghomestay.Models;

namespace Quanlyvadatphonghomestay.Components;

public partial class SuggestionCard : ContentView
{
	public SuggestionCard()
	{
		InitializeComponent();
	}

    private async Task SuggestionCard_Tappe()
    {
		if(BindingContext is Homestay homestay)
		{
			await Navigation.PushAsync(new ChiTietHomestay());
		}
    }

    private void SuggestionCard_Tapped(object sender, TappedEventArgs e)
    {
		        _ = SuggestionCard_Tappe();
    }
}