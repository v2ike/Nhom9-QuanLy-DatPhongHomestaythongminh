using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay;

public partial class DangNhap : ContentPage
{
    private readonly AuthService _authService = new();

    public DangNhap()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void TogglePassword_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        if (!PasswordEntry.IsPassword)
        {
            await Task.Delay(3000);
            PasswordEntry.IsPassword = true;
        }
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string email = UsernameEntry.Text?.Trim();
        string password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Lỗi", "Vui lòng nhập email.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Lỗi", "Vui lòng nhập mật khẩu.", "OK");
            return;
        }

        try
        {
            var result = await _authService.LoginAsync(email, password);

            if (result == null)
            {
                await DisplayAlert("Đăng nhập thất bại", "Email hoặc mật khẩu không đúng.", "OK");
                return;
            }

            await DisplayAlert("Đăng nhập thành công", $"Xin chào {result.FullName}!", "OK");

            if (Application.Current?.Windows.Count > 0)
            {
                Application.Current.Windows[0].Page = new NavigationPage(new TrangChu());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", "Không thể kết nối đến máy chủ:\n" + ex.Message, "OK");
        }
    }

    private async void Register_Tapped(object sender, TappedEventArgs e)
    {
        
    }

    private async void ForgotPassword_Tapped(object sender, TappedEventArgs e)
    {
      
    }
}