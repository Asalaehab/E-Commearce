using E_Commearce.web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.Xml;
using System.Text;

namespace E_Commearce.web.Extensions
{
    public  static class ServiceRegisteration
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(Options =>
            {
                Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In=ParameterLocation.Header,
                    Name="AUthorization",
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="Bearer",
                    Description="Enter 'bearer ' follwed by space and your token",

                });
                Options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });

            });
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


        public static IServiceCollection AddJWTService(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                //Options.SaveToken = true;//it will save token

                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTOptions")["SecretKey"]!))
                };
            });

            return Services; //i will add things on it and return it back
        }
    }
}
