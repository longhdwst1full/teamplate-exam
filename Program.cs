using Microsoft.EntityFrameworkCore;
using ServerWeb;
using ServerWeb.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Thêm DbContext để test connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Test database connection
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // Kiểm tra connection
        if (context.Database.CanConnect())
        {
            Console.WriteLine("✓ Kết nối database thành công!");
        }
        else
        {
            Console.WriteLine("✗ Không thể kết nối database!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Lỗi kết nối database: {ex.Message}");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "home2",
    pattern: "Home2",
    defaults: new { controller = "CozaHome", action = "Index2" });

app.MapControllerRoute(
    name: "home3",
    pattern: "Home3",
    defaults: new { controller = "CozaHome", action = "Index3" });

app.MapControllerRoute(
    name: "about",
    pattern: "Home/About",
    defaults: new { controller = "CozaHome", action = "About" });

app.MapControllerRoute(
    name: "contact",
    pattern: "Home/Contact",
    defaults: new { controller = "CozaHome", action = "Contact" });

app.MapControllerRoute(
    name: "shop",
    pattern: "Shop/{action=Index}/{id?}",
    defaults: new { controller = "CozaShop" });

app.MapControllerRoute(
    name: "blog",
    pattern: "Blog/{action=Index}/{id?}",
    defaults: new { controller = "CozaBlog" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CozaHome}/{action=Index}/{id?}");

app.Run();
