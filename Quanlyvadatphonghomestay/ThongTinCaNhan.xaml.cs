using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay;

public partial class ThongTinCaNhan : ContentPage
{
    private readonly AuthService _authService;

    public ThongTinCaNhan()
    {
        InitializeComponent();
        _authService = new AuthService();
        BindingContext = new ProfileViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. Lấy UserId người dùng vừa đăng nhập
        int currentUserId = Preferences.Default.Get("UserId", 0);

        if (currentUserId <= 0)
        {
            await DisplayAlert("Thông báo", "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!", "OK");
            Application.Current.MainPage = new NavigationPage(new DangNhap());
            return;
        }

        var viewModel = (ProfileViewModel)BindingContext;

        // 2. Hiển thị thông tin nhanh đã lưu từ Preferences
        viewModel.FullName = Preferences.Default.Get("FullName", "Người dùng");
        viewModel.Email = Preferences.Default.Get("Email", "Chưa cập nhật");
        viewModel.MemberTier = Preferences.Default.Get("Role", "CUSTOMER").ToUpper();

        // 3. Tải thông tin mới nhất từ API theo UserId
        try
        {
            var user = await _authService.GetUserByIdAsync(currentUserId);
            if (user != null)
            {
                viewModel.FullName = user.FullName;
                viewModel.Email = user.Email;
                viewModel.MemberTier = string.IsNullOrEmpty(user.Role) ? "CUSTOMER" : user.Role.ToUpper();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Lỗi tải thông tin profile: {ex.Message}");
        }
    }

    private async void OnEditProfileClicked(object sender, EventArgs e)
    {
        await DisplayAlert("Thông báo", "Chức năng chỉnh sửa thông tin đang được phát triển!", "OK");
    }

    private void OnNotificationToggled(object sender, ToggledEventArgs e)
    {
        Preferences.Default.Set("IsNotificationOn", e.Value);
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Xác nhận", "Bạn có chắc chắn muốn đăng xuất?", "Đăng xuất", "Hủy");
        if (confirm)
        {
            _authService.Logout();
            Application.Current.MainPage = new NavigationPage(new DangNhap());
        }
    }

    private async void OnAppSettingsClicked(object sender, TappedEventArgs e)
    {
        string action = await DisplayActionSheet(
            "Chọn ngôn ngữ / Select Language",
            "Hủy",
            null,
            "Tiếng Việt",
            "English"
        );

        if (action == "Tiếng Việt")
        {
            SetAppLanguage("vi", "Tiếng Việt • Tiền tệ VNĐ");
        }
        else if (action == "English")
        {
            SetAppLanguage("en", "English • Currency USD");
        }
    }

    private void SetAppLanguage(string langCode, string displayText)
    {
        Preferences.Default.Set("AppLanguage", langCode);
        Preferences.Default.Set("AppLanguageText", displayText);

        var viewModel = (ProfileViewModel)BindingContext;
        if (viewModel != null)
        {
            viewModel.SelectedLanguageText = displayText;
        }

        var culture = new System.Globalization.CultureInfo(langCode);
        System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
        System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

        DisplayAlert("Thông báo", $"Đã chuyển ngôn ngữ sang: {(langCode == "vi" ? "Tiếng Việt" : "English")}", "OK");
    }
}

// Class ViewModel quản lý dữ liệu hiển thị lên giao diện XAML
public class ProfileViewModel : BindableObject
{
    private string _fullName = "Chưa cập nhật";
    public string FullName
    {
        get => _fullName;
        set { _fullName = value; OnPropertyChanged(); }
    }

    private string _email = "Chưa cập nhật";
    public string Email
    {
        get => _email;
        set { _email = value; OnPropertyChanged(); }
    }

    private string _phone = "0905 *** ***";
    public string Phone
    {
        get => _phone;
        set { _phone = value; OnPropertyChanged(); }
    }

    private string _memberTier = "CUSTOMER";
    public string MemberTier
    {
        get => _memberTier;
        set { _memberTier = value; OnPropertyChanged(); }
    }
    private string _selectedLanguageText = Preferences.Default.Get("AppLanguageText", "Tiếng Việt • Tiền tệ VNĐ");
    public string SelectedLanguageText
    {
        get => _selectedLanguageText;
        set { _selectedLanguageText = value; OnPropertyChanged(); }
    }
    public int BookingCount { get; set; } = 0;
    public int FavoriteCount { get; set; } = 0;
    public bool IsNotificationOn { get; set; } = true;
}