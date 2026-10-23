using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay;

public partial class DangNhap : ContentPage
{
    private readonly AuthService _authService;

    // Tiêm AuthService trực tiếp từ Constructor


    // Trường hợp chưa đăng ký DI trong MauiProgram, bạn có thể tạm dùng constructor mặc định này:
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

    private async void TogglePassword_Clicked(object sender, EventArgs e)
    {
        string email = UsernameEntry.Text?.Trim();
        string password = PasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Thông báo", "Vui lòng nhập Email và Mật khẩu!", "OK");
            return;
        }

        try
        {
            var result = await _authService.LoginAsync(email, password);

            if (result != null)
            {
                await DisplayAlert("Thành công", $"Chào mừng {result.FullName}!", "OK");
                Application.Current.MainPage = new NavigationPage(new TrangChu());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi kết nối", ex.Message, "OK");
        }
    }

    private async void Register_Tapped(object sender, TappedEventArgs e)
    {

    }

    private async void ForgotPassword_Tapped(object sender, TappedEventArgs e)
    {

    }

    private void LoginButton_Clicked(object sender, EventArgs e)
    {

    }
}