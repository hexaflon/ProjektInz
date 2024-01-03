using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestTest.Models.Db;

namespace TestTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var AppName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            // Add services to the container.
            builder.Services.AddRazorPages();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<IdentityDatabaseContext>(options =>
            options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<Osoba>(options=>
            options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDatabaseContext>();
            builder.Services.AddDbContext<DatabaseContext>(options => 
            options.UseSqlServer(AppName.GetSection("ConnectionStrings")["DB_Login"]));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            
            
            
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}