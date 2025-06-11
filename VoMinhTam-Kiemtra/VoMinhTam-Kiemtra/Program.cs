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

            builder.Services.AddSession(); // th�m d�ng n�y
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

            app.UseSession(); // th�m d�ng n�y
            app.UseAuthorization();

      
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
