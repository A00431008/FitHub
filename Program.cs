using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FitHub.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AmenityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AmenityContext") ?? throw new InvalidOperationException("Connection string 'AmenityContext' not found.")));
builder.Services.AddDbContext<SwimmingPoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SwimmingPoolContext") ?? throw new InvalidOperationException("Connection string 'SwimmingPoolContext' not found.")));
builder.Services.AddDbContext<SpaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SpaContext") ?? throw new InvalidOperationException("Connection string 'SpaContext' not found.")));
builder.Services.AddDbContext<SaunaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SaunaContext") ?? throw new InvalidOperationException("Connection string 'SaunaContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
