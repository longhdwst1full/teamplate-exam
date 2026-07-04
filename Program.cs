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
    defaults: new { controller = "MinishopHome", action = "About" });

app.MapControllerRoute(
    name: "contact",
    pattern: "Contact",
    defaults: new { controller = "MinishopHome", action = "Contact" });

app.MapControllerRoute(
    name: "shop",
    pattern: "Shop",
    defaults: new { controller = "MinishopShop", action = "Index" });

app.MapControllerRoute(
    name: "product-single",
    pattern: "Shop/ProductSingle",
    defaults: new { controller = "MinishopShop", action = "ProductSingle" });

app.MapControllerRoute(
    name: "cart",
    pattern: "Shop/Cart",
    defaults: new { controller = "MinishopShop", action = "Cart" });

app.MapControllerRoute(
    name: "checkout",
    pattern: "Shop/Checkout",
    defaults: new { controller = "MinishopShop", action = "Checkout" });

app.MapControllerRoute(
    name: "blog",
    pattern: "Blog",
    defaults: new { controller = "MinishopBlog", action = "Index" });

app.MapControllerRoute(
    name: "blog-single",
    pattern: "Blog/Single",
    defaults: new { controller = "MinishopBlog", action = "Single" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MinishopHome}/{action=Index}/{id?}");

app.Run();
