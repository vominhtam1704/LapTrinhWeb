using Microsoft.EntityFrameworkCore;
using VoMinhTam_Kiemtra.Models;

namespace VoMinhTam_Kiemtra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<Test1Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Test1")));

            builder.Services.AddSession(); // thêm dòng này
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

    
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession(); // thêm dòng này
            app.UseAuthorization();

      
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
