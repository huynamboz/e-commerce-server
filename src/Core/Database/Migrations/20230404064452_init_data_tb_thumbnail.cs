using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace e_commerce_server.Src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class init_data_tb_thumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 4, 4, 13, 44, 51, 908, DateTimeKind.Local).AddTicks(4085), new DateTime(2023, 4, 4, 13, 44, 51, 908, DateTimeKind.Local).AddTicks(4093) });

            migrationBuilder.InsertData(
                table: "thumbnails",
                columns: new[] { "id", "product_id", "thumbnail_url" },
                values: new object[,]
                {
                    { 1, 1, "https://cdn.chotot.com/9Z0aQLnCvRxd1GweQgBNy_cmjRMix_mM54sVi9Aazzs/preset:view/plain/ccd2bbdb8b1a2ec046d397c7202c7052-2819561496186255487.jpg" },
                    { 2, 1, "https://cdn.chotot.com/9Z0aQLnCvRxd1GweQgBNy_cmjRMix_mM54sVi9Aazzs/preset:view/plain/ccd2bbdb8b1a2ec046d397c7202c7052-2819561496186255487.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "thumbnails",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "thumbnails",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2023, 4, 4, 13, 34, 32, 170, DateTimeKind.Local).AddTicks(7753), new DateTime(2023, 4, 4, 13, 34, 32, 170, DateTimeKind.Local).AddTicks(7766) });
        }
    }
}
