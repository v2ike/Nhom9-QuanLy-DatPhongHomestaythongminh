namespace Quanlyvadatphonghomestay;

public partial class Modau : ContentPage
{
    private CancellationTokenSource _cts = new();

    public Modau()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _cts = new CancellationTokenSource();

        try
        {
            // 1. Hiệu ứng Logo: Phóng to & Fade In đồng thời
            LogoCard.FadeTo(1, 1200, Easing.CubicOut);
            LogoCard.ScaleTo(1.0, 1200, Easing.CubicOut);

            await Task.Delay(300, _cts.Token);

            // 2. Hiệu ứng Tên & Slogan: Trượt lên & Fade In
            TextContainer.FadeTo(1, 900, Easing.CubicOut);
            TextContainer.TranslateTo(0, 0, 900, Easing.CubicOut);

            await Task.Delay(200, _cts.Token);

            // 3. Hiệu ứng Khung Loading & Footer
            BottomContainer.FadeTo(1, 900, Easing.CubicOut);
            BottomContainer.TranslateTo(0, 0, 900, Easing.CubicOut);

            // 4. Thanh Progress Bar chạy từ 0% -> 100%
            await LoadingBar.ProgressTo(1.0, 2200, Easing.CubicInOut);

            // Hiệu ứng "Thở nhẹ" cho Logo
            StartBreathingAnimation();

            await Task.Delay(2000, _cts.Token);

            // Chuyển sang trang Đăng nhập an toàn
            NavigateToLogin();
        }
        catch (TaskCanceledException)
        {
            // Bỏ qua lỗi khi chuyển trang giữa chừng
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Hủy các tác vụ đang chạy và Animation để giải phóng bộ nhớ
        _cts?.Cancel();
        this.AbortAnimation("LogoBreathing");
    }

    private void StartBreathingAnimation()
    {
        var parentAnimation = new Animation();
        var scaleUp = new Animation(v => LogoCard.Scale = v, 1.0, 1.04, Easing.SinInOut);
        var scaleDown = new Animation(v => LogoCard.Scale = v, 1.04, 1.0, Easing.SinInOut);

        parentAnimation.Add(0, 0.5, scaleUp);
        parentAnimation.Add(0.5, 1, scaleDown);

        parentAnimation.Commit(this, "LogoBreathing", length: 4000, repeat: () => true);
    }

    private void NavigateToLogin()
    {
        if (Application.Current?.Windows.Count > 0)
        {
            Application.Current.Windows[0].Page = new NavigationPage(new DangNhap());
        }
    }
}