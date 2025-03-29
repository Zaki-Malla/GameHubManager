    using GameHubManager.Models;
using GameHubManager.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceTypeRepository, DeviceTypeRepository>();
builder.Services.AddScoped<IDevicePriceRepository, DevicePriceRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddDbContext<DSContext>(options =>
{
    var wwwrootPath = Path.Combine(builder.Environment.WebRootPath, "DB");
    var dbPath = Path.Combine(wwwrootPath, "databaseByZakiMalla.db");

    Directory.CreateDirectory(wwwrootPath); 
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(240);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddIdentity<UserModel, RoleModel>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<DSContext>()
.AddDefaultTokenProviders();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DSContext>();
    context.Database.Migrate(); 
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var roleManager = services.GetRequiredService<RoleManager<RoleModel>>();
        var userManager = services.GetRequiredService<UserManager<UserModel>>();

        var requiredRoles = new[] { "Employer", "Employee" };
        foreach (var role in requiredRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new RoleModel(role));
            }
        }

        var employerRoleExists = await userManager.GetUsersInRoleAsync("Employer");

        if (!employerRoleExists.Any())
        {
            var defaultEmployer = new UserModel
            {
                FullName = "Zaki Malla",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(defaultEmployer, "Zaki.123");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(defaultEmployer, "Employer");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Initialization error: {ex.Message}");
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "login",
    pattern: "Login",
    defaults: new { controller = "Account", action = "Login" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}"
);
app.Run();