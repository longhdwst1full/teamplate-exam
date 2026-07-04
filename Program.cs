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
    name: "about",
    pattern: "About",
    defaults: new { controller = "ShoppersHome", action = "About" });

app.MapControllerRoute(
    name: "contact",
    pattern: "Contact",
    defaults: new { controller = "ShoppersHome", action = "Contact" });

app.MapControllerRoute(
    name: "shop",
    pattern: "Shop",
    defaults: new { controller = "ShoppersShop", action = "Index" });

app.MapControllerRoute(
    name: "shop-single",
    pattern: "Shop/ShopSingle",
    defaults: new { controller = "ShoppersShop", action = "ShopSingle" });

app.MapControllerRoute(
    name: "cart",
    pattern: "Shop/Cart",
    defaults: new { controller = "ShoppersShop", action = "Cart" });

app.MapControllerRoute(
    name: "checkout",
    pattern: "Shop/Checkout",
    defaults: new { controller = "ShoppersShop", action = "Checkout" });

app.MapControllerRoute(
    name: "thankyou",
    pattern: "Shop/ThankYou",
    defaults: new { controller = "ShoppersShop", action = "ThankYou" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ShoppersHome}/{action=Index}/{id?}");

app.Run();
