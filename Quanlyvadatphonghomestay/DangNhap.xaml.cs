using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay;

public partial class DangNhap : ContentPage
{
    private readonly AuthService _authService = new AuthService();

    public DangNhap()
    {
        InitializeComponent();
    }

    // Toggle ẩn/hiện mật khẩu trong 3 giây
    private async void TogglePassword_Clicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = false;
        await Task.Delay(3000);
        PasswordEntry.IsPassword = true;
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
                await DisplayAlert(
                    "Đăng nhập thất bại",
                    "Email hoặc mật khẩu không đúng.",
                    "OK"
                );
                return;
            }

            await DisplayAlert(
                "Đăng nhập thành công",
                $"Xin chào {result.FullName}!",
                "OK"
            );

            // SỬA TẠI ĐÂY: Thay đổi MainPage hoàn toàn sang TrangChu
            if (Application.Current != null)
            {
                Application.Current.MainPage = new NavigationPage(new TrangChu());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Lỗi",
                "Không thể kết nối đến máy chủ:\n" + ex.Message,
                "OK"
            );
        }
    }

    // Chuyển sang trang Đăng ký
    private async void Register_Tapped(object sender, TappedEventArgs e)
    {
        // Giả sử bạn có trang DangKy
        // Application.Current.MainPage = new NavigationPage(new DangKy());
    }

    // Chuyển sang trang Quên mật khẩu
    private async void ForgotPassword_Tapped(object sender, TappedEventArgs e)
    {
        // Giả sử bạn có trang QuenMatKhau
        // await Navigation.PushAsync(new QuenMatKhau());
    }
}