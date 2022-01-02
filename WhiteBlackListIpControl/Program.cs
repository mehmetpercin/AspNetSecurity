using WhiteBlackListIpControl.Filters;
using WhiteBlackListIpControl.Middlewares;
using WhiteBlackListIpControl.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<IpList>(configuration.GetSection("IpList"));
builder.Services.AddScoped<CheckWhiteList>();

var app = builder.Build();

//app.UseMiddleware<IpSafeMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
