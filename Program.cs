using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using ProductCategories.Models;
using ProductCategories.MVCServices;
using ProductCategories.Services;
using System;

namespace ProductCategories
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Configure DbContext for Entity Framework
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ProductCategoryDB"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                )
            );

            // Add HttpClient with named client configuration
            

            // Register services
            builder.Services.AddTransient<ICategoryService, CategoryService>();
            builder.Services.AddTransient<ICategoryServices, CategoryServices>();
            builder.Services.AddTransient<IProductServices, ProductServices>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient("CategoryServices", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7295/"); // Replace with your API base address
                // Additional HttpClient configuration here
            });
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint(); // EF Core migrations endpoint
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

      
           // Apply migrations at startup

            app.Run();
        }
    }
}
