using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class init_tb_product_thumb_cate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    discount = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    product_status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    active_status = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "thumbnails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    thumbnail_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thumbnails", x => x.id);
                    table.ForeignKey(
                        name: "FK_thumbnails_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
                values: new object[] { 1, "Đồ điện tử" });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "id", "active_status", "category_id", "created_at", "description", "discount", "name", "price", "product_status", "updated_at", "user_id" },
                values: new object[] { 1, false, 1, new DateTime(2023, 4, 4, 13, 34, 32, 170, DateTimeKind.Local).AddTicks(7753), "Chuột Gaming Không Dây LOGITECH G304 Lightspeed Chuột Chơi Game\r\n❌❌  Lưu ý: Sản phẩm là OEM, không phải hàng chính hãng! Do đó không thể kết nối với APP Logitech (OEM, viết tắt của Original Equipment Manufacturer, được hiểu là Nhà sản xuất thiết bị gốc, dùng để chỉ công ty, đối tác gia công, lắp ráp sản phẩm cho một công ty sở hữu thương hiệu và công nghệ khác). Ngoài ra đèn led của chuột cũng không mượt như đèn led chính hàng, còn lại cảm giác khi chơi game, độ bền, thông số kỹ thuật và mọi thứ khác đều rất tốt, phù hợp cho các bạn không muốn bỏ ra một khoản tiền lớn mà vẫn được trải nghiệm cảm giác chơi game tuyệt vời của chuột G304.\r\n- Bảo Hành 12 tháng cho sản Phẩm nhằm tạo uy tín và chất lượng của shop\r\n- 1 ĐỔI 1 trong vòng 07 ngày nếu có lỗi từ NSX\r\n- G304 là chuột chơi game không dây LIGHTSPEED được thiết kế cho hiệu suất thực sự với các đột phá công nghệ mới nhất ở mức giá thành phù hợp. Đó là chơi game không dây thế hệ mới, hiện đã sẵn sàng cho mọi game thủ.\r\n-Các phím chính của G304, cả ở bên trái và phải, được đánh giá 10 triệu lần nhấp. G304 cũng có nút giữa,\r\n- Nó đem lại tới 250 giờ hoạt động chỉ trên một quả pin AA\r\n\r\nThông số kĩ thuật\r\n- Độ phân giải: 200 - 12.000 DPI\r\n- Làm mịn/tăng tốc/lọc\r\n- Tăng tốc tối đa: > 40 G5\r\n- Tốc độ tối đa: > 400 IPS6\r\n- Kết nối: USB\r\n- Tốc độ báo cáo không dây: 1000 Hz (1ms)\r\n- Công nghệ không dây: LIGHTSPEED không dây\r\n- Chiều cao: 116,6 mm\r\n- Chiều rộng: 62,15 mm\r\n- Chiều dày: 38,2 mm\r\n- Trọng lượng: 99 g\r\n- Tuổi thọ PIN: 250 giờ\r\n- Sản phẩm là Hàng Công ty loại 1 cần hỗ trợ liên hệ shop nhé !!\r\n-CAM KẾT--------> BẢO HÀNH 6 THÁNG - 1 ĐỔI 1 TRONG VÒNG 3 NGÀY NẾU DO LỖI CỦA NHÀ SẢN XUẤT\r\n-Chuột Gaming Không Dây LOGITECH G304 Lightspeed Chuột Chơi Game kiểm tra kỹ lưỡng trước khi gửi đi nhằm tránh sản phẩm lỗi đến tay khách hàng\r\n- Quy trình đóng gói cẩn thận bằng túi bóng khí chống sốc nhằm hạn chế tối đa trường hợp lỗi do quá trình vận chuyển.\r\nBộ phận Kĩ Thuật sẽ kiểm tra trước khi gửi đi cho quý khách \r\n\r\n#chuotgaming #chuotgame #gaming #game #chuotchoigame #chuotkhongday #chuotUSB #chuotchuyengame #chuotlogitech #chuotsieure #G304 #chuotlogitech304 #chuotkhongday #chuotUSB #chuotchuyengame #chuotlogitech #chuotsieure #chuotG304", 10, "Điện thoại iphone 5", 120000, "Đã qua sử dụng", new DateTime(2023, 4, 4, 13, 34, 32, 170, DateTimeKind.Local).AddTicks(7766), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_products_category_id",
                table: "products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_products_user_id",
                table: "products",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_thumbnails_product_id",
                table: "thumbnails",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "thumbnails");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
