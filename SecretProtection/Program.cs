var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(opts =>
{
    //opts.AddDefaultPolicy(policy =>
    //{
    //    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    //});

    opts.AddPolicy("AllowSites", policy =>
    {
        // policy.WithOrigins(new[] { "http://localhost:4200" }).AllowAnyHeader().AllowAnyMethod();

        policy.WithOrigins("http://*.example.com").SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration["SqlServerConnectionString"];

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
//app.UseCors();
app.UseCors("AllowSites");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
