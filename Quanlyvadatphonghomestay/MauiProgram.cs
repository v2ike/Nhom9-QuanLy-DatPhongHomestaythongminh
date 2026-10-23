using Microsoft.Extensions.Logging;
using Quanlyvadatphonghomestay.Services;

namespace Quanlyvadatphonghomestay
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // 1. Đăng ký HttpClient dùng chung cho toàn hệ thống (Đã đổi sang cổng 49700)
            builder.Services.AddSingleton<HttpClient>(sp =>
            {
                return new HttpClient
                {
                    BaseAddress = new Uri("http://10.0.2.2:49700/")
                };
            });

            // 2. Đăng ký Services (Thêm AuthService vào đây)
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<Locationsevices>();
            builder.Services.AddSingleton<HomestayService>();

            // 3. Đăng ký Views/Pages để hỗ trợ Dependency Injection (Thêm DangNhap vào đây)
            builder.Services.AddTransient<DangNhap>();
            builder.Services.AddTransient<TrangChu>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}