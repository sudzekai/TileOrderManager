using BLL.Services;
using BLL.Services.Interfaces;
using DAL.EfCore.Data;
using DAL.EfCore.UOW;
using DAL.EfCore.UOW.Interface;
using Scalar.AspNetCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<TileOrderManagerDbContext>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<ITileService, TileService>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Маршрут OpenAPI JSON
                app.MapOpenApi();

                // Scalar UI
                app.MapScalarApiReference(options =>
                {
                    options.OpenApiRoutePattern = "/openapi/v1.json"; // Указываем путь к OpenAPI
                    options.Title = "Tile Order API";
                });
            }

            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
