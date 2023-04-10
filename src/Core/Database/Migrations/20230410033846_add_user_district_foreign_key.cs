using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class add_user_district_foreign_key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_district_id",
                table: "users",
                column: "district_id");

            migrationBuilder.AddForeignKey(
                name: "FK_users_districts_district_id",
                table: "users",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_districts_district_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_district_id",
                table: "users");
        }
    }
}
