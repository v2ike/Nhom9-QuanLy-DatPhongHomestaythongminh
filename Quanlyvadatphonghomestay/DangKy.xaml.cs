using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay
{
    public partial class DangKy : ContentPage
    {
        private readonly AuthService _authService;

        public DangKy(AuthService authService)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            _authService = authService;
        }

        public DangKy()
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

        private async void Login_Tapped(object sender, EventArgs e)
        {
            await Task.WhenAll(
                this.FadeTo(0, 250, Easing.CubicOut),
                this.ScaleTo(0.95, 250, Easing.CubicOut)
            );
            await Navigation.PopAsync(false);
        }

        private void TogglePassword_Clicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        }

        private void ToggleConfirmPassword_Clicked(object sender, EventArgs e)
        {
            ConfirmPasswordEntry.IsPassword = !ConfirmPasswordEntry.IsPassword;
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            string fullName = FullNameEntry.Text?.Trim();
            string email = UsernameEntry.Text?.Trim();
            string password = PasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(fullName))
            {
                await DisplayAlert("Thông báo", "Vui lòng nhập họ và tên!", "OK");
                FullNameEntry.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Thông báo", "Vui lòng nhập tên tài khoản/email!", "OK");
                UsernameEntry.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Thông báo", "Vui lòng nhập mật khẩu!", "OK");
                PasswordEntry.Focus();
                return;
            }

            if (password.Length < 6)
            {
                await DisplayAlert("Thông báo", "Mật khẩu phải chứa ít nhất 6 ký tự!", "OK");
                PasswordEntry.Focus();
                return;
            }

            if (password != confirmPassword)
            {
                await DisplayAlert("Thông báo", "Mật khẩu xác nhận không khớp!", "OK");
                ConfirmPasswordEntry.Focus();
                return;
            }

            RegisterButton.IsEnabled = false;
            RegisterButton.Text = "Đang xử lý...";

            try
            {
                bool isSuccess = await _authService.RegisterAsync(fullName, email, password);

                if (isSuccess)
                {
                    await DisplayAlert("Thành công", "Đăng ký tài khoản thành công!", "OK");

                    await Task.WhenAll(
                        this.FadeTo(0, 250, Easing.CubicOut),
                        this.ScaleTo(0.95, 250, Easing.CubicOut)
                    );
                    await Navigation.PopAsync(false);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Lỗi đăng ký", ex.Message, "OK");
            }
            finally
            {
                RegisterButton.IsEnabled = true;
                RegisterButton.Text = "Đăng ký";
            }
        }
    }
}