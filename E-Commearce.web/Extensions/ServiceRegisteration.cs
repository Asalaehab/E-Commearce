using E_Commearce.web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commearce.web.Extensions
{
    public  static class ServiceRegisteration
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            return Services;
        }


        public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiErrorReponse;

            });

            return Services;
        }
    }
}
