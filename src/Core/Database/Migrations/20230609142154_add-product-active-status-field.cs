using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class addproductactivestatusfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active_status",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                column: "active_status",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active_status",
                table: "products");
        }
    }
}
