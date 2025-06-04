using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VoMinhTam_Tuan3.Models;
using VoMinhTam_Tuan3.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Kết nối CSDL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Tích hợp Identity với giao diện Razor UI sẵn có
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// Giao diện Razor Pages để xử lý các trang của Identity (Login/Register)
builder.Services.AddRazorPages();

// Đăng ký các dịch vụ xử lý dữ liệu
builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();

// Thêm MVC Controller
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình Middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Phải có khi dùng Identity
app.UseAuthentication();
app.UseAuthorization();

// Kích hoạt Razor Pages (cho Identity UI)
app.MapRazorPages();

// Cấu hình định tuyến mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(name: "Admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
