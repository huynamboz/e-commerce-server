using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace e_commerce_server.src.Core.Database.Migrations
{
    /// <inheritdoc />
    public partial class addsheetcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "thumbnail",
                table: "categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "name", "thumbnail" },
                values: new object[] { "Điện thoại", "https://cdn11.dienmaycholon.vn/filewebdmclnew/public/userupload/files/mtsp/di-dong/lua-chon-mua-dien-thoai-di-dong-phu-hop.jpg" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name", "thumbnail" },
                values: new object[,]
                {
                    { 2, "Máy tính & laptop", "https://phuonghoangtv.com/wp-content/uploads/2020/09/nen-mua-lap-top-hang-nao-tot.jpg" },
                    { 3, "Máy ảnh & máy quay", "https://images2.thanhnien.vn/528068263637045248/2023/5/25/canon-eos-r100-1684979537624432755196.jpg" },
                    { 4, "Đồng hồ", "https://product.hstatic.net/1000223154/product/z3618907692798_de826ce083ece5970076a2f86e1fd7f8__1__095576c58e414d51aca229831f895982_master.jpg" },
                    { 5, "Giày dép nam", "https://salt.tikicdn.com/cache/w1200/ts/product/56/96/c3/a8037ee5af3afe4df0ed8fabbde729d7.jpg" },
                    { 6, "Tablet", "https://cdn.mos.cms.futurecdn.net/LTkjzWfU6NNdMd4EibvpDV.jpg" },
                    { 7, "Đồ thể thao & du lịch", "https://media3.scdn.vn/img4/2023/03_24/JdIylfqMqfcmckdhhhzT.jpg" },
                    { 8, "Ô tô & xe máy & xe đạp", "https://cdnimg.vietnamplus.vn/uploaded/qrndqxjwp/2022_01_15/5c13378d057c480a8e98c64a413e20613682161636517973.jpg" },
                    { 9, "Balo & túi ví nam", "https://agiare.vn/wp-content/uploads/2020/01/vi-da-bo-nam.jpg" },
                    { 10, "Đồ chơi", "https://www.moby.com.vn/data/bt8/do-choi-xep-hinh-tuoi-tho-110-chi-tiet-4_1644893165.png" },
                    { 11, "Đồ cho thú cưng", "https://vuongquocloaivat.com/wp-content/uploads/2020/06/banh-donut-cho-thu-cung-compressed.jpg" },
                    { 12, "Dụng cụ và thiết bị", "https://nikatei.com.vn/image/catalog/do-dien-tu.jpg" },
                    { 13, "Thời trang nữ", "https://image-us.eva.vn/upload/1-2021/images/2021-02-26/min-house---shop-thoi-trang-nu-duoc-yeu-thich-thoi-trang-nu-1-1614307605-24-width600height600.jpg" },
                    { 14, "Mẹ và bé", "https://thuthuatnhanh.com/wp-content/uploads/2022/06/Anh-me-va-be.jpg" },
                    { 15, "Nhà cửa và đời sống", "https://thietkenoithat.com/Portals/0/xNews/uploads/2018/6/20/nha-dep-1.jpg" },
                    { 16, "Mỹ phẩm", "https://cdn.tgdd.vn/Files/2021/10/08/1388863/my-pham-thuan-chay-my-pham-sach-va-my-pham-huu-co-co-phai-la-mot-202110082321000878.jpg" },
                    { 17, "Sức khỏe", "https://suckhoedoisong.qltns.mediacdn.vn/324455921873985536/2022/8/30/uong-thuoc-thoi-diem-nao-16618466465071926930931.png" },
                    { 18, "Giày dép nữ", "https://img.mwc.com.vn/giay-thoi-trang?w=480&h=510&FileInput=//Upload/2022/08/z3682162219081-b6287f86835b1a448ea12308528ea450.jpg" },
                    { 19, "Túi ví nữ", "https://mikaystore.r.worldssl.net/wp-content/uploads/2021/03/tui-xach-nu-thich-hop-mang-di-su-kien.png" },
                    { 20, "Sách báo", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQVaOQolKo7VhPR_egYxV_QQxgLko8BBZnfEQ&usqp=CAU" },
                    { 21, "Thời trang cho bé", "https://salt.tikicdn.com/cache/w1200/ts/product/8a/a4/19/22f686157264d6a2b8cdc1511997c5ca.jpg" },
                    { 22, "Giặt giũ", "https://cdn.tgdd.vn/Files/2015/10/17/722583/su-dung-bot-giat-cho-may-giat-nhu-the-nao-dung-cach-8.jpg" },
                    { 23, "Thiết bị điện gia dụng", "https://www.dienlamhong.com/wp-content/uploads/2021/02/cong-ty-thiet-bi-dien-lam-hong.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "id",
                keyValue: 23);

            migrationBuilder.DropColumn(
                name: "thumbnail",
                table: "categories");

            migrationBuilder.UpdateData(
                table: "categories",
                keyColumn: "id",
                keyValue: 1,
                column: "name",
                value: "Đồ điện tử");
        }
    }
}
