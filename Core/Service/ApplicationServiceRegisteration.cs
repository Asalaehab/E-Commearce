using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public  static class ApplicationServiceRegisteration
    {

        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductProfile).Assembly);
            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IAuthentaction, Authentaction>();
            services.AddScoped<IOrderService, OrderService>();



            services.AddScoped<Func<IProductService>>(Provider=>
            ()=> Provider.GetRequiredService<IProductService>()
            );

            services.AddScoped<Func<IOrderService>>(provider=>
                ()=>provider.GetRequiredService<IOrderService>()
            );

            services.AddScoped<Func<IBasketService>>(Provider =>
            () =>Provider.GetRequiredService<IBasketService>()
            );

            services.AddScoped<Func<IAuthentaction>>(Provider =>
            ()=>Provider.GetRequiredService<IAuthentaction>()
            );

            services.AddScoped<ICashingService, CacheService>();
            return services;
        }

    }
}
