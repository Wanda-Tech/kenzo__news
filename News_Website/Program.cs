using Microsoft.EntityFrameworkCore;
using News_Website.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NewsWebsiteContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<INewsService, NewsService>();
// auto-mapper
builder.Services.AddAutoMapper(o => { }, AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();



app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=News}/{action=Index}/{id?}");

// ✅ Run migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<NewsWebsiteContext>();

    DbInitializer.Seed(dbContext);
}

app.Run();