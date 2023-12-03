using MediSynthFinals.Data;
using MediSynthFinals.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext for DB
// Uses DefaultConnection variable string in appsettings.json
builder.Services.AddDbContext<MediDbContext>(
    options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("DefaultConnection")));

// Service for Identity Login
builder.Services.AddDefaultIdentity<UserCredentials>
    (options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true; // Ensure unique email addresses
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MediDbContext>();

// Redirect Cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "YourAppCookieName";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = "/User/Login";
    // ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<MediDbContext>();
//context.Database.EnsureDeleted();
context.Database.EnsureCreated();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var roles = new[] { "ADMIN", "PATIENT", "DOCTOR" };
   
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserCredentials>>();

    // Check if the user with the specified email exists
    var existingUser = userManager.FindByEmailAsync("test@example.com").Result;
    if (existingUser == null)
    {
        // Create a new user
        var newUser = new UserCredentials();

        newUser.UserName = "admin";
        newUser.Email = "test@example.com";
        newUser.fName = "Group 5";
        newUser.lName = "3ITE";
        newUser.PhoneNumber = "09567052824";
        newUser.department = "ADMIN";
        newUser.userRole = "ADMIN";

        await userManager.CreateAsync(newUser, "admin123");

        await userManager.AddToRoleAsync(newUser, "ADMIN");
    }
}

app.Run();
