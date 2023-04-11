using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class create_favorite_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "delete_at",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "update_at",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "delete_at",
                table: "products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "favorites",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    create_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorites", x => new { x.product_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_favorites_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_favorites_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "delete_at", "description", "updated_at" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "string", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "delete_at", "update_at" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_favorites_user_id",
                table: "favorites",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "favorites");

            migrationBuilder.DropColumn(
                name: "delete_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "update_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "delete_at",
                table: "products");

            migrationBuilder.UpdateData(
                table: "products",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "description", "updated_at" },
                values: new object[] { new DateTime(2023, 4, 10, 11, 38, 44, 28, DateTimeKind.Local).AddTicks(6271), "Chuột Gaming Không Dây LOGITECH G304 Lightspeed Chuột Chơi Game\r\n❌❌  Lưu ý: Sản phẩm là OEM, không phải hàng chính hãng! Do đó không thể kết nối với APP Logitech (OEM, viết tắt của Original Equipment Manufacturer, được hiểu là Nhà sản xuất thiết bị gốc, dùng để chỉ công ty, đối tác gia công, lắp ráp sản phẩm cho một công ty sở hữu thương hiệu và công nghệ khác). Ngoài ra đèn led của chuột cũng không mượt như đèn led chính hàng, còn lại cảm giác khi chơi game, độ bền, thông số kỹ thuật và mọi thứ khác đều rất tốt, phù hợp cho các bạn không muốn bỏ ra một khoản tiền lớn mà vẫn được trải nghiệm cảm giác chơi game tuyệt vời của chuột G304.\r\n- Bảo Hành 12 tháng cho sản Phẩm nhằm tạo uy tín và chất lượng của shop\r\n- 1 ĐỔI 1 trong vòng 07 ngày nếu có lỗi từ NSX\r\n- G304 là chuột chơi game không dây LIGHTSPEED được thiết kế cho hiệu suất thực sự với các đột phá công nghệ mới nhất ở mức giá thành phù hợp. Đó là chơi game không dây thế hệ mới, hiện đã sẵn sàng cho mọi game thủ.\r\n-Các phím chính của G304, cả ở bên trái và phải, được đánh giá 10 triệu lần nhấp. G304 cũng có nút giữa,\r\n- Nó đem lại tới 250 giờ hoạt động chỉ trên một quả pin AA\r\n\r\nThông số kĩ thuật\r\n- Độ phân giải: 200 - 12.000 DPI\r\n- Làm mịn/tăng tốc/lọc\r\n- Tăng tốc tối đa: > 40 G5\r\n- Tốc độ tối đa: > 400 IPS6\r\n- Kết nối: USB\r\n- Tốc độ báo cáo không dây: 1000 Hz (1ms)\r\n- Công nghệ không dây: LIGHTSPEED không dây\r\n- Chiều cao: 116,6 mm\r\n- Chiều rộng: 62,15 mm\r\n- Chiều dày: 38,2 mm\r\n- Trọng lượng: 99 g\r\n- Tuổi thọ PIN: 250 giờ\r\n- Sản phẩm là Hàng Công ty loại 1 cần hỗ trợ liên hệ shop nhé !!\r\n-CAM KẾT--------> BẢO HÀNH 6 THÁNG - 1 ĐỔI 1 TRONG VÒNG 3 NGÀY NẾU DO LỖI CỦA NHÀ SẢN XUẤT\r\n-Chuột Gaming Không Dây LOGITECH G304 Lightspeed Chuột Chơi Game kiểm tra kỹ lưỡng trước khi gửi đi nhằm tránh sản phẩm lỗi đến tay khách hàng\r\n- Quy trình đóng gói cẩn thận bằng túi bóng khí chống sốc nhằm hạn chế tối đa trường hợp lỗi do quá trình vận chuyển.\r\nBộ phận Kĩ Thuật sẽ kiểm tra trước khi gửi đi cho quý khách \r\n\r\n#chuotgaming #chuotgame #gaming #game #chuotchoigame #chuotkhongday #chuotUSB #chuotchuyengame #chuotlogitech #chuotsieure #G304 #chuotlogitech304 #chuotkhongday #chuotUSB #chuotchuyengame #chuotlogitech #chuotsieure #chuotG304", new DateTime(2023, 4, 10, 11, 38, 44, 28, DateTimeKind.Local).AddTicks(6279) });
        }
    }
}
