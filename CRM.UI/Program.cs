using CMR.Domain;
using CMR.Domain.Core;
using CMR.Domain.Data;
using CRM.UI.Models.IdenityUserAccess;
using CRM.UI.Service.Email;
using DeviceDetectorNET;
using DeviceDetectorNET.Cache;
using MGMTApp.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;
using WebEssentials.AspNetCore.Pwa;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region AddServices

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

// Add Email services to the container.
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();

builder.Services.AddDataAccessService();

builder.Services.AddProgressiveWebApp(new PwaOptions
{
    RegisterServiceWorker = true,
    RegisterWebmanifest = false,  // (Manually register in Layout file)
    Strategy = ServiceWorkerStrategy.NetworkFirst,
    OfflineRoute = "Offline.html"
});

#endregion AddServices

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.AllowedForNewUsers = true;
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.ConfigureApplicationCookie(option =>
{
    option.Cookie.Name = "CRMCookieName";
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    option.LoginPath = "/UsersAccount/SignIn";
    option.AccessDeniedPath = "/UsersAccount/AccessDenied";

    // ReturnUrlParameter requires
    //using Microsoft.AspNetCore.Authentication.Cookies;
    option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    option.SlidingExpiration = true;
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
    ILogger logger = loggerFactory.CreateLogger("app");
    try
    {
        UserManager<IdentityUser> userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
        await DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
        logger.LogInformation("Finished Seeding Default Data");
        logger.LogInformation("Application Starting");
    }
    catch (Exception ex)
    {
        logger.LogWarning(ex, "An error occurred seeding the DB");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseMigrationsEndPoint();
}
else
{
    _ = app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

app.Use(async (context, next) =>
{
    var detector = new DeviceDetector(context.Request.Headers["User-Agent"].ToString());
    detector.SetCache(new DictionaryCache());
    detector.Parse();

    if (detector.IsMobile())
    {
        context.Items.Remove("isMobile");
        context.Items.Add("isMobile", true);
    }
    else
    {
        context.Items.Remove("isMobile");
        context.Items.Add("isMobile", false);
    }

    context.Items.Remove("DeviceName");
    context.Items.Add("DeviceName", detector.GetDeviceName());

    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Dashboard}/{id?}");

    endpoints.MapAreaControllerRoute(
       name: "default",
       areaName: "Admin",
       pattern: "{controller=Categories}/{action=Index}/{id?}"
     );

    //endpoints.MapAreaControllerRoute(
    //        name: "SystemAdmin",
    //        areaName: "SystemAdmin",
    //        pattern: "{controller=Home}/{action=Index}/{id?}"
    //      );

    endpoints.MapRazorPages();
});

app.Run();