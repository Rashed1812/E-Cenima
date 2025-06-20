using System.Text.Json.Serialization;
using DAL.Data;
<<<<<<< HEAD
using DAL.Data.Repositories.Calsses;
=======
 Demo
using DAL.Data.Repositories.Calsses;
using DAL.Data.Repositories.Intrfaces;
using Microsoft.AspNetCore.Identity;

>>>>>>> master
using DAL.Data.Repositories.GenericRepositories;
 master
using Microsoft.EntityFrameworkCore;

namespace E_Cenima
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // Add DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

<<<<<<< HEAD
            builder.Services.AddScoped<IMovieRepo,MovieRepo>();
=======
 Demo
            // Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Register seeding service
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();

master

>>>>>>> master
            var app = builder.Build();

            // Seed initial data
            await app.SeedDatabaseAsync();

            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }


}
