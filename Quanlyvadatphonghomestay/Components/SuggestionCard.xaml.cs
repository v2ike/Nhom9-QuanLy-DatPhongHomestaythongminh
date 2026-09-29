using Quanlyvadatphonghomestay.Models;

namespace Quanlyvadatphonghomestay.Components;

public partial class SuggestionCard : ContentView
{
	public SuggestionCard()
	{
		InitializeComponent();
	}

    private async Task SuggestionCard_Tapped(object sender, TappedEventArgs e)
    {
		if(BindingContext is Homestay homestay)
		{
			await Navigation.PushAsync(new ChiTietHomestay());
		}
    }
}