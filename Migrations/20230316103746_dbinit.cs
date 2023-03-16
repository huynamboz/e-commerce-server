using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.Migrations
{
    /// <inheritdoc />
    public partial class dbinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "FileModel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "created_at",
                table: "FileModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "deleted_at",
                table: "FileModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
