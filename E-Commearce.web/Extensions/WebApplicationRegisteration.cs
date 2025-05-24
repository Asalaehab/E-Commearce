using DomainLayer.Contracts;
using E_Commearce.web.CustomeMiddleWare;
using Peristance;

namespace E_Commearce.web.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var ObjectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();

            
        }

        public static IApplicationBuilder UseCustomeMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionhandlerMiddleWare>();
            return app;
        }
    }
}
