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
            LogoCard.FadeTo(1, 1200, Easing.CubicOut);
            LogoCard.ScaleTo(1.0, 1200, Easing.CubicOut);

            await Task.Delay(300, _cts.Token);

            TextContainer.FadeTo(1, 900, Easing.CubicOut);
            TextContainer.TranslateTo(0, 0, 900, Easing.CubicOut);

            await Task.Delay(200, _cts.Token);

            BottomContainer.FadeTo(1, 900, Easing.CubicOut);
            BottomContainer.TranslateTo(0, 0, 900, Easing.CubicOut);

            await LoadingBar.ProgressTo(1.0, 2200, Easing.CubicInOut);

            StartBreathingAnimation();

            await Task.Delay(2000, _cts.Token);

            NavigateToLogin();
        }
        catch (TaskCanceledException)
        {
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

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