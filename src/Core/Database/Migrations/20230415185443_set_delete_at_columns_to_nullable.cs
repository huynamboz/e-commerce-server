using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class set_delete_at_columns_to_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "delete_at",
                table: "users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "delete_at",
                table: "products",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                column: "delete_at",
                value: null);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "address", "avatar", "delete_at", "district_id", "gender", "name", "phone_number" },
                values: new object[] { "54 Nguyễn Lương Bằng", "https://res.cloudinary.com/dgtaa84en/image/upload/v1678819995/organizations/116/avatar/avatar.jpg", null, 550500, true, "Admin", "0812345678" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "address", "avatar", "birthday", "created_at", "delete_at", "district_id", "email", "gender", "name", "password", "phone_number", "refresh_token", "reset_token", "reset_token_expiration_date", "role_id", "update_at" },
                values: new object[] { 2, null, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "user2@example.com", null, "User", "$2a$04$GmL6XUWBFM9nSUzBynCNa.nvLo7pfiPK9sg1tdNiF3tKmhoMP1MIi", null, null, null, null, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.AlterColumn<DateTime>(
                name: "delete_at",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "delete_at",
                table: "products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                column: "delete_at",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "address", "avatar", "delete_at", "district_id", "gender", "name", "phone_number" },
                values: new object[] { null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "John Doe", null });
        }
    }
}
