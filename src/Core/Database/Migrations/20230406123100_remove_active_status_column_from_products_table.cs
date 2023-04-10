using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class remove_active_status_column_from_products_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active_status",
                table: "products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "active_status",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
