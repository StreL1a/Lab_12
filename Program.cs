using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.MapControllerRoute(
    name: "main",
    pattern: "{controller=Main}/{action=Index}/");
app.Run();