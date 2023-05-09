using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class updatedistrictstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "districts",
                keyColumn: "id",
                keyValue: 270500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "districts",
                columns: new[] { "id", "city_id", "name" },
                values: new object[] { 270500, 270000, "Huyện Trà Lĩnh" });
        }
    }
}
