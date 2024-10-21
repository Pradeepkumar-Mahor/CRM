using CMR.Domain;
using CMR.Domain.Core;
using CMR.Domain.Data;
using CRM.UI.Models.IdenityUserAccess;
using CRM.UI.Service.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region AddServices

//builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
//builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

// Add Email services to the container.
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

#endregion AddServices

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireNonAlphanumeric = true;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);

    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/Identity/SignIn";
    option.AccessDeniedPath = "/Home/AccessDenied";

    option.ExpireTimeSpan = TimeSpan.FromHours(2);
});

builder.Services.AddControllersWithViews();

//IHost host = Host.CreateApplicationBuilder(args).Build();

//using (IServiceScope scope = host.Services.CreateScope())
//{
//    IServiceProvider services = scope.ServiceProvider;
//    ILoggerFactory loggerFactory = services.GetRequiredService<ILoggerFactory>();
//    ILogger logger = loggerFactory.CreateLogger("app");
//    try
//    {
//        UserManager<IdentityUser> userManager = services.GetRequiredService<UserManager<IdentityUser>>();
//        RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
//        await DefaultRoles.SeedAsync(userManager, roleManager);
//        await DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
//        await DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
//        logger.LogInformation("Finished Seeding Default Data");
//        logger.LogInformation("Application Starting");
//    }
//    catch (Exception ex)
//    {
//        logger.LogWarning(ex, "An error occurred seeding the DB");
//    }
//}
WebApplication app = builder.Build();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();