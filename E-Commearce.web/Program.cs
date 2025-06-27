
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
using StackExchange.Redis;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

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
           

            builder.Services.AddCors(Options=>
            {
                Options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader ();
                    builder.AllowAnyMethod ();
                    builder.AllowAnyOrigin ();
                });
            });
            builder.Services.AddSwaggerService();
            builder.Services.AddInfrastructureService(builder.Configuration);
            builder.Services.AddApplicationService();
            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTService(builder.Configuration);

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
                app.UseSwaggerUI(Options =>
                {
                    ////Options.ConfigObject
                    //Options.ConfigObject = new ConfigObject()
                    //{
                    //     DisplayRequestDuration=true
                    //};

                    //Options.DocumentTitle = "My E-Commerace API";

                    //Options.JsonSerializerOptions = new JsonSerializerOptions()
                    //{
                    //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    //};


                    Options.DocExpansion(DocExpansion.Full);
                    Options.EnableFilter(); 
                    Options.EnablePersistAuthorization();
                   
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
