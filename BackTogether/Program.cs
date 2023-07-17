using BackTogether.Data;
using BackTogether.Services;
using BackTogether.Services.api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;

internal class Program {
    private static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<BackTogetherContext>(options =>
            // WARNING!! This pulls the connection string from secrets.json file which is 
            // stored in local machines and not pushed to the repo for safety
            options.UseSqlServer(builder.Configuration.GetConnectionString("BackTogetherDatabase"))
        );

        //// This solves this issue: 
        //// https://stackoverflow.com/questions/73215256/foreign-key-reference-object-being-required-in-entity-framework
        //builder.Services.AddControllers(options =>
        //    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

        builder.Services.AddSession(options => {
            // Time out the session after 10 minutes
            options.IdleTimeout = TimeSpan.FromMinutes(10);
        });

        // Add services to the container.
        builder.Services.AddControllersWithViews();
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
            pattern: "{controller=Home}/{action=Index}/{id?}");

        //var optionsBuilder = new DbContextOptionsBuilder();
        //if (HostingEnvironment.IsDevelopment()) {
        //    optionsBuilder.UseSqlServer(Configuration["Data:dev:DataContext"]);
        //} else if (HostingEnvironment.IsStaging()) {
        //    optionsBuilder.UseSqlServer(Configuration["Data:staging:DataContext"]);
        //} else if (HostingEnvironment.IsProduction()) {
        //    optionsBuilder.UseSqlServer(Configuration["Data:live:DataContext"]);
        //}

        //var context = new ApplicationContext(optionsBuilder.Options);
        //context.Database.EnsureCreated();

        //optionsBuilder = new DbContextOptionsBuilder();
        //if (HostingEnvironment.IsDevelopment())
        //    optionsBuilder.UseSqlServer(Configuration["Data:dev:TransientContext"]);
        //else if (HostingEnvironment.IsStaging())
        //    optionsBuilder.UseSqlServer(Configuration["Data:staging:TransientContext"]);
        //else if (HostingEnvironment.IsProduction())
        //    optionsBuilder.UseSqlServer(Configuration["Data:live:TransientContext"]);
        //new TransientContext(optionsBuilder.Options).Database.EnsureCreated();

        app.Run();
    }
}