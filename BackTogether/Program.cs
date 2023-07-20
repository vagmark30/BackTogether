using BackTogether.Data;
using BackTogether.Services;
using BackTogether.Services.api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

internal class Program {
    private static async Task Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        var dbManager = new DatabaseConnectionManager();
        string connectionstring = await dbManager.GetConnectionStringAsync();

        builder.Services.AddDbContext<BackTogetherContext>(options =>
            // WARNING!! This pulls the connection string from secrets.json file which is 
            // stored in local machines and not pushed to the repo for safety
            options.UseSqlServer(builder.Configuration.GetConnectionString("BackTogetherDatabase"))
        );

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        // For using and storing session info
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options => {
            // Time out the session after 10 minutes
            options.IdleTimeout = TimeSpan.FromMinutes(10);
        });
        builder.Services.AddScoped<ILogin, LoginService>();
        builder.Services.AddScoped<IDatabase, DatabaseService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        // Adding Middleware
        app.UseSession();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }
}