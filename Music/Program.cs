using Music.Application;
using Music.ExternalSystems.Cloudinary;
using Music.Infrastructure.SQLite.Extensions;
using Music.Repository.EfCore.Extensions;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("MusicDbConnectionSQLite");

builder.Services.AddDbContextSqLite(connection);
builder.Services.AddApplication();
builder.Services.AddRepositoryEfCore();
builder.Services.AddControllersWithViews();
builder.Services.AddCloudinary(builder.Configuration);

builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
