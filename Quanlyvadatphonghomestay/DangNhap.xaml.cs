using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay;

public partial class DangNhap : ContentPage
{
    private readonly AuthService _authService;

    public DangNhap(AuthService authService)
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _authService = authService;
    }

    public DangNhap()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _authService = new AuthService();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        this.Opacity = 0;
        await this.FadeTo(1, 400, Easing.CubicOut);
    }

    private void TogglePassword_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string email = UsernameEntry.Text?.Trim();
        string password = PasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Thông báo", "Vui lòng nhập Email và Mật khẩu!", "OK");
            return;
        }

        LoginButton.IsEnabled = false;
        LoginButton.Text = "Đang xử lý...";

        try
        {
            var result = await _authService.LoginAsync(email, password);

            if (result != null)
            {
                // LƯU THÔNG TIN USER VÀO PREFERENCES THEO ID
                Preferences.Default.Set("UserId", result.UserId);
                Preferences.Default.Set("FullName", result.FullName ?? "");
                Preferences.Default.Set("Email", result.Email ?? "");
                Preferences.Default.Set("Role", result.Role ?? "");

                await DisplayAlert("Thành công", $"Chào mừng {result.FullName}!", "OK");

                // Chuyển sang Trang Chủ
                Application.Current.MainPage = new NavigationPage(new TrangChu());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi kết nối", ex.Message, "OK");
        }
        finally
        {
            LoginButton.IsEnabled = true;
            LoginButton.Text = "Đăng nhập";
        }
    }

    private async void Register_Tapped(object sender, TappedEventArgs e)
    {
        await Task.WhenAll(
            this.FadeTo(0, 250, Easing.CubicOut),
            this.ScaleTo(0.95, 250, Easing.CubicOut)
        );

        await Navigation.PushAsync(new DangKy(), false);
    }

    private async void ForgotPassword_Tapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Thông báo", "Chức năng quên mật khẩu đang được phát triển!", "OK");
    }
}