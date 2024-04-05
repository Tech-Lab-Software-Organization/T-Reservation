using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using T_Reservation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "Cookies";
})
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/Login/LoginC"; // Ruta de inicio de sesión para clientes
    options.AccessDeniedPath = "/Login/LoginC"; // Ruta de acceso denegado para clientes
})
.AddCookie("EmpleadoAuthenticationScheme", options =>
{
    options.LoginPath = "/Login/LoginE"; // Ruta de inicio de sesión para empleados
    options.AccessDeniedPath = "/Login/LoginE"; // Ruta de acceso denegado para empleados
});


builder.Services.AddHttpContextAccessor(); 
builder.Services.AddSession();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=IndexHome}/{id?}");

app.Run();


