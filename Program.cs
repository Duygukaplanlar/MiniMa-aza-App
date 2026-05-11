using Microsoft.EntityFrameworkCore;
using MiniMagaza.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı Servisi (SQL Server Bağlantısı)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Session (Oturum) Ayarları - Sepetin çalışması için şart
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 3. MVC ve HttpContext Yardımı
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// 4. Hata ve Güvenlik Yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 5. Önemli Sıralama: Routing -> Session -> Authorization
app.UseRouting();

app.UseSession(); // Sepet için bu satır tam burada olmalı

app.UseAuthorization();

// 6. Varsayılan Sayfa Yönlendirmesi
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();