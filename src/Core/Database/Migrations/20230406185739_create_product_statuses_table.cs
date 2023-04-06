using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace e_commerce_server.Src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class create_product_statuses_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product_status",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "status_id",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "product_statuses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_statuses", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "product_statuses",
                columns: new[] { "id", "status" },
                values: new object[,]
                {
                    { 1, "Mới" },
                    { 2, "Như mới" },
                    { 3, "Đã qua sử dụng" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_status_id",
                table: "products",
                column: "status_id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_product_statuses_status_id",
                table: "products",
                column: "status_id",
                principalTable: "product_statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_product_statuses_status_id",
                table: "products");

            migrationBuilder.DropTable(
                name: "product_statuses");

            migrationBuilder.DropIndex(
                name: "IX_products_status_id",
                table: "products");

            migrationBuilder.DropColumn(
                name: "status_id",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "product_status",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
