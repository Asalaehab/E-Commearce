
using DomainLayer.Contracts;
using E_Commearce.web.CustomeMiddleWare;
using E_Commearce.web.Extensions;
using E_Commearce.web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Peristance;
using Peristance.Data;
using Peristance.Repositiories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using shared.ErrorModels;
using System.Threading.Tasks;

namespace E_Commearce.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Add services to the container.



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           


            builder.Services.AddSwaggerService();
            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationService();
            builder.Services.AddWebApplicationServices(); 
           
            
            #endregion


            var app = builder.Build();


            #region DataSeeding

            await app.SeedDataBaseAsync();
            #endregion



            #region Configure the HTTP request pipeline.

            // Configure the HTTP request pipeline.
            //app.Use(async(RequestContext,NextMiddleWare)=>
            //{
            //    Console.WriteLine("Request Under Processing");
            //    await NextMiddleWare.Invoke();

            //    Console.WriteLine("Waiting Repsponse");

            //    Console.WriteLine(RequestContext.Response.Body);

            //});




            app.UseCustomeMiddleWare();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
