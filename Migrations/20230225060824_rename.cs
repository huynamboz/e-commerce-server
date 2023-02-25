using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "idFile",
                table: "HangHoa",
                newName: "thubnail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "thubnail",
                table: "HangHoa",
                newName: "idFile");
        }
    }
}
