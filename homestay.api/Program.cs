using homestay.api.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

public partial class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Dịch vụ Controller & Swagger UI
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // 2. Cấu hình kết nối SQL Server
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        var app = builder.Build();

        // 3. Kích hoạt Swagger UI cho tất cả môi trường
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Homestay API V1");
            c.RoutePrefix = "swagger"; // Đường dẫn truy cập UI: /swagger
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();

        // 4. Tự động chuyển hướng từ trang gốc (/) sang (/swagger)
        app.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger");
            return Task.CompletedTask;
        });

        // 5. Bọc MapControllers trong try-catch để kiểm tra lỗi Controller
        try
        {
            app.MapControllers();
        }
        catch (ReflectionTypeLoadException ex)
        {
            Console.WriteLine("\n================ CHI TIẾT LỖI CONTROLLER ================");
            foreach (var le in ex.LoaderExceptions)
            {
                if (le != null)
                {
                    Console.WriteLine($"- {le.Message}");
                }
            }
            Console.WriteLine("=========================================================\n");
            throw;
        }

        app.Run();
    }
}