﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Security.Claims;
using System.Text;
using WebWaterPaintStore.Core.Identity;
using WebWaterPaintStore.Data.Contexts;
using WebWaterPaintStore.Data.Seeders;
using WebWaterPaintStore.Services.Timing;
using WebWaterPaintStore.Services.WaterPaints;
using WebWaterPaintStore.WebApi.Media;

namespace WebWaterPaintStore.WebApi.Extensions {
    public static class WebApplicationExtensions {
        public static WebApplicationBuilder ConfigureServices(
            this WebApplicationBuilder builder) {

            builder.Services.AddMemoryCache();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]))
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole(ClaimTypes.Role, "Admin"));
                options.AddPolicy("RequireManagerRole", policy => policy.RequireRole(ClaimTypes.Role, "Manager"));
            });


            builder.Services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IDataSeeder, DataSeeder>();
            builder.Services.AddScoped<ITimeProvider, LocalTimeProvider>();
            builder.Services.AddScoped<IMediaManager, LocalFileSystemMediaManager>();
            builder.Services.AddScoped<IStoreRepository, StoreRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>(); 


            return builder;
        }

        public static WebApplicationBuilder ConfigureCors(
            this WebApplicationBuilder builder) {
            builder.Services.AddCors(options => {
                options.AddPolicy("WebStoreApp", policyBuilder =>
                    policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            return builder;
        }

        public static WebApplicationBuilder ConfigureNLog(
            this WebApplicationBuilder builder) {

            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            return builder;
        }


        public static IApplicationBuilder UseDataSeeder(
            this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            try
            {
                scope.ServiceProvider.GetRequiredService<IDataSeeder>().Initialize();
            }
            catch (Exception e)
            {
                scope.ServiceProvider.GetRequiredService<ILogger<Program>>()
                    .LogError(e, "Count not insert data into database");
            }

            return app;
        }

        public static WebApplicationBuilder ConfigureSwaggerOpenApi(
            this WebApplicationBuilder builder) {

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }


        public static WebApplication SetupRequestPipeline(
            this WebApplication app) {

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("WebStoreApp");

            return app;
        }

    }
}
