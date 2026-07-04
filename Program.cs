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
    defaults: new { controller = "Home", action = "About" });

app.MapControllerRoute(
    name: "contact",
    pattern: "Contact",
    defaults: new { controller = "Home", action = "Contact" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
