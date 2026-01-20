using Dingo.Data;
using Dingo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 3;
    options.Password.RequireUppercase=true;
    options.Password.RequireLowercase=true;
    options.Password.RequireDigit=true;
    options.Lockout.MaxFailedAccessAttempts=3;
    options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromSeconds(15);
}).AddDefaultTokenProviders().AddEntityFrameworkStores<DingoDbContext>();

builder.Services.AddDbContext<DingoDbContext>(options =>
{
    options.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Database=Dingo;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"SQL Server Management Studio\";Command Timeout=0");
});
var app = builder.Build();

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
    );

app.Run();
