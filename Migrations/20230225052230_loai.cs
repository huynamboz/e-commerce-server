using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.Migrations
{
    /// <inheritdoc />
    public partial class loai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "FileModel");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "FileModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "FileModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "Size",
                table: "FileModel",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "FileModel");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "FileModel",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
