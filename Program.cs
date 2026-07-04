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
    defaults: new { controller = "FootwearHome", action = "About" });

app.MapControllerRoute(
    name: "contact",
    pattern: "Contact",
    defaults: new { controller = "FootwearHome", action = "Contact" });

app.MapControllerRoute(
    name: "men",
    pattern: "Men",
    defaults: new { controller = "FootwearShop", action = "Men" });

app.MapControllerRoute(
    name: "women",
    pattern: "Women",
    defaults: new { controller = "FootwearShop", action = "Women" });

app.MapControllerRoute(
    name: "product-detail",
    pattern: "Shop/ProductDetail",
    defaults: new { controller = "FootwearShop", action = "ProductDetail" });

app.MapControllerRoute(
    name: "cart",
    pattern: "Shop/Cart",
    defaults: new { controller = "FootwearShop", action = "Cart" });

app.MapControllerRoute(
    name: "checkout",
    pattern: "Shop/Checkout",
    defaults: new { controller = "FootwearShop", action = "Checkout" });

app.MapControllerRoute(
    name: "order-complete",
    pattern: "Shop/OrderComplete",
    defaults: new { controller = "FootwearShop", action = "OrderComplete" });

app.MapControllerRoute(
    name: "wishlist",
    pattern: "Shop/Wishlist",
    defaults: new { controller = "FootwearShop", action = "Wishlist" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=FootwearHome}/{action=Index}/{id?}");

app.Run();
