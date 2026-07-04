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
    name: "fashion",
    pattern: "Fashion",
    defaults: new { controller = "EflyerHome", action = "Fashion" });

app.MapControllerRoute(
    name: "electronic",
    pattern: "Electronic",
    defaults: new { controller = "EflyerHome", action = "Electronic" });

app.MapControllerRoute(
    name: "jewellery",
    pattern: "Jewellery",
    defaults: new { controller = "EflyerHome", action = "Jewellery" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EflyerHome}/{action=Index}/{id?}");

app.Run();
