using BLL.Services.ActorService;
using BLL.Services.CinemaService;
using BLL.Services.FileService;
using BLL.Services.Movies;
using BLL.Services.TimingService;
using DAL.Data;
using DAL.Data.Repositories.Calsses;
using DAL.Data.Repositories.GenericRepositories;
using DAL.Data.Repositories.Intrfaces;
using Microsoft.EntityFrameworkCore;

namespace E_Cenima
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddScoped<IMovieRepo, MovieRepo>();
            builder.Services.AddScoped<IMovieService,MovieService>();
            builder.Services.AddScoped<ICinemaRepo, CinemaRepo>();
            builder.Services.AddScoped<ICenimaService, CinemaService>();
            builder.Services.AddScoped<IActorRepo, ActorRepo>();
            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<ITimeRepo, TimeRepo>();
            builder.Services.AddScoped<ITimeService, TimeService>();

            builder.Services.AddScoped<IFileService, FileService>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
