using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class create_cities_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 100000, "Hà Nội" },
                    { 160000, "Hưng Yên" },
                    { 170000, "Hải Dương" },
                    { 180000, "Hải Phòng" },
                    { 200000, "Quảng Ninh" },
                    { 220000, "Bắc Ninh" },
                    { 230000, "Bắc Giang" },
                    { 240000, "Lạng Sơn" },
                    { 250000, "Thái Nguyên" },
                    { 260000, "Bắc Cạn" },
                    { 270000, "Cao Bằng" },
                    { 280000, "Vĩnh Phúc" },
                    { 290000, "Phú Thọ" },
                    { 300000, "Tuyên Quang" },
                    { 310000, "Hà Giang" },
                    { 320000, "Yên Bái" },
                    { 330000, "Lào Cai" },
                    { 350000, "Hòa Bình" },
                    { 360000, "Sơn La" },
                    { 380000, "Điện Biên" },
                    { 390000, "Lai Châu" },
                    { 400000, "Hà Nam" },
                    { 410000, "Thái Bình" },
                    { 420000, "Nam Định" },
                    { 430000, "Ninh Bình" },
                    { 440000, "Thanh Hóa" },
                    { 460000, "Nghệ An" },
                    { 480000, "Hà Tĩnh" },
                    { 510000, "Quảng Bình" },
                    { 520000, "Quảng Trị" },
                    { 530000, "Thừa Thiên Huế" },
                    { 550000, "Đà Nẵng" },
                    { 560000, "Quảng Nam" },
                    { 570000, "Quảng Ngãi" },
                    { 580000, "Kon Tum" },
                    { 590000, "Bình Định" },
                    { 600000, "Gia Lai" },
                    { 620000, "Phú Yên" },
                    { 630000, "Đắk Lắk" },
                    { 640000, "Đắk Nông" },
                    { 650000, "Khánh Hòa" },
                    { 660000, "Ninh Thuận" },
                    { 670000, "Lâm Đồng" },
                    { 700000, "Hồ Chí Minh" },
                    { 790000, "Bà Rịa - Vũng Tàu" },
                    { 800000, "Bình Thuận" },
                    { 810000, "Đồng Nai" },
                    { 820000, "Bình Dương" },
                    { 830000, "Bình Phước" },
                    { 840000, "Tây Ninh" },
                    { 850000, "Long An" },
                    { 860000, "Tiền Giang" },
                    { 870000, "Đồng Tháp" },
                    { 880000, "An Giang" },
                    { 890000, "Vĩnh Long" },
                    { 900000, "Cần Thơ" },
                    { 910000, "Hậu Giang" },
                    { 920000, "Kiên Giang" },
                    { 930000, "Bến Tre" },
                    { 940000, "Trà Vinh" },
                    { 950000, "Sóc Trăng" },
                    { 960000, "Bạc Liêu" },
                    { 970000, "Cà Mau" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
