using Microsoft.EntityFrameworkCore.Migrations;

namespace VOMINHTAM_KIEMTRA.Migrations
{
    public partial class ThemDuLieuMau : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Thêm dữ liệu mẫu cho bảng Categories
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Lập trình" },
                    { 2, "Sức khỏe" },
                    { 3, "Văn học" },
                    { 4, "Tâm lý - Kỹ năng sống" },
                    { 5, "Thiếu nhi" },
                    { 6, "Kinh tế" }
                });

            // Thêm dữ liệu mẫu cho bảng Books
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Title", "Author", "Year", "ImagePath", "CategoryId" },
                values: new object[,]
                {
                    { 1, "C# Căn Bản", "Nguyễn Văn A", 2022, "csharp.jpg", 1 },
                    { 2, "ASP.NET Core MVC", "Lê Minh Trí", 2023, "aspnetcore.jpg", 1 },
                    { 3, "Sống Khỏe Mỗi Ngày", "Trần Thị B", 2021, "songkhoe.jpg", 2 },
                    { 4, "Bí Quyết Ăn Uống Khoa Học", "Ngô Hằng", 2020, "dinhduong.jpg", 2 },
                    { 5, "Tuổi Thơ Dữ Dội", "Phạm Văn C", 2018, "tuoitho.jpg", 3 },
                    { 6, "Cho Tôi Xin Một Vé Đi Tuổi Thơ", "Nguyễn Nhật Ánh", 2017, "vetuoitho.jpg", 3 },
                    { 7, "Đắc Nhân Tâm", "Dale Carnegie", 1936, "dacnhantam.jpg", 4 },
                    { 8, "Totto-Chan: Cô Bé Bên Cửa Sổ", "Tetsuko Kuroyanagi", 1981, "tottochan.jpg", 5 },
                    { 9, "Tư Duy Nhanh Và Chậm", "Daniel Kahneman", 2011, "tuduy.jpg", 6 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 9; i++)
            {
                migrationBuilder.DeleteData("Books", "BookId", i);
            }

            for (int i = 1; i <= 6; i++)
            {
                migrationBuilder.DeleteData("Categories", "CategoryId", i);
            }
        }
    }
}
