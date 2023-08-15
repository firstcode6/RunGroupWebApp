using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RunGroupWebApp.Data;
using RunGroupWebApp.Helpers;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;
using RunGroupWebApp.Repository;
using RunGroupWebApp.Services;

namespace RunGroupWebApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IClubRepository, ClubRepository>();
            builder.Services.AddScoped<IRaceRepository, RaceRepository>();
            builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();

            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

            //// local runs
            //builder.Services.AddDbContext<AppDbContext>(options =>
            //{
            //    // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));

            //});

            //docker runs
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST"); //"localhost";
            var dbName = Environment.GetEnvironmentVariable("DB_NAME"); //"RunGroups";
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD"); //"data#123";
            var connectionString = $"Server={dbHost};Database={dbName}; User ID=sa;Password={dbPassword}; TrustServerCertificate=True";
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));


            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
           
            var app = builder.Build();

            //// In Developer PowerShell: dotnet run seeddata
            //if (args.Length == 1 && args[0].ToLower() == "seeddata")
            //{
            //    Seed.SeedData(app);
            //    await Seed.SeedUsersAndRolesAsync(app);
            //}

            // seeding data and users
            using (var scope = app.Services.CreateScope())
            {
                var salesContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                salesContext.Database.EnsureCreated();
                // salesContext.SeedUsersAndRolesAsyncNew();

                Seeder.SeedDataNew(salesContext);
                await Seeder.SeedUsersAndRolesAsyncNew(scope);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); // Authentication determines whether the person is user or not.
            app.UseAuthorization(); // While it determines What permission does the user have?

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}