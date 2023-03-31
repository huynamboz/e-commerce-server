using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.Src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class create_users_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    password = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    gender = table.Column<bool>(type: "bit", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reset_token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reset_token_expiration_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "active_status", "address", "avatar", "birthday", "created_at", "email", "gender", "name", "password", "phone_number", "reset_token", "reset_token_expiration_date", "role_id" },
                values: new object[] { 1, false, null, null, null, new DateTime(2023, 3, 31, 9, 39, 41, 662, DateTimeKind.Local).AddTicks(7162), "string@gmail.com", null, "John Doe", "string", null, null, null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_users_id",
                table: "users",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
