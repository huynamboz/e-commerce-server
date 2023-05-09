﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_commerce_server.src.Core.Database;

#nullable disable

namespace e_commerce_server.src.Core.Database.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230401075455_add_district_id_column_to_users_table")]
    partial class add_district_id_column_to_users_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("e_commerce_server.src.Core.Database.Data.CityData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("id");

                    b.ToTable("cities");

                    b.HasData(
                        new
                        {
                            id = 100000,
                            name = "Hà Nội"
                        },
                        new
                        {
                            id = 700000,
                            name = "Hồ Chí Minh"
                        },
                        new
                        {
                            id = 880000,
                            name = "An Giang"
                        },
                        new
                        {
                            id = 790000,
                            name = "Bà Rịa - Vũng Tàu"
                        },
                        new
                        {
                            id = 220000,
                            name = "Bắc Ninh"
                        },
                        new
                        {
                            id = 230000,
                            name = "Bắc Giang"
                        },
                        new
                        {
                            id = 820000,
                            name = "Bình Dương"
                        },
                        new
                        {
                            id = 590000,
                            name = "Bình Định"
                        },
                        new
                        {
                            id = 830000,
                            name = "Bình Phước"
                        },
                        new
                        {
                            id = 800000,
                            name = "Bình Thuận"
                        },
                        new
                        {
                            id = 960000,
                            name = "Bạc Liêu"
                        },
                        new
                        {
                            id = 930000,
                            name = "Bến Tre"
                        },
                        new
                        {
                            id = 260000,
                            name = "Bắc Cạn"
                        },
                        new
                        {
                            id = 900000,
                            name = "Cần Thơ"
                        },
                        new
                        {
                            id = 650000,
                            name = "Khánh Hòa"
                        },
                        new
                        {
                            id = 530000,
                            name = "Thừa Thiên Huế"
                        },
                        new
                        {
                            id = 330000,
                            name = "Lào Cai"
                        },
                        new
                        {
                            id = 200000,
                            name = "Quảng Ninh"
                        },
                        new
                        {
                            id = 810000,
                            name = "Đồng Nai"
                        },
                        new
                        {
                            id = 420000,
                            name = "Nam Định"
                        },
                        new
                        {
                            id = 970000,
                            name = "Cà Mau"
                        },
                        new
                        {
                            id = 270000,
                            name = "Cao Bằng"
                        },
                        new
                        {
                            id = 600000,
                            name = "Gia Lai"
                        },
                        new
                        {
                            id = 310000,
                            name = "Hà Giang"
                        },
                        new
                        {
                            id = 400000,
                            name = "Hà Nam"
                        },
                        new
                        {
                            id = 480000,
                            name = "Hà Tĩnh"
                        },
                        new
                        {
                            id = 170000,
                            name = "Hải Dương"
                        },
                        new
                        {
                            id = 180000,
                            name = "Hải Phòng"
                        },
                        new
                        {
                            id = 350000,
                            name = "Hòa Bình"
                        },
                        new
                        {
                            id = 160000,
                            name = "Hưng Yên"
                        },
                        new
                        {
                            id = 920000,
                            name = "Kiên Giang"
                        },
                        new
                        {
                            id = 580000,
                            name = "Kon Tum"
                        },
                        new
                        {
                            id = 390000,
                            name = "Lai Châu"
                        },
                        new
                        {
                            id = 670000,
                            name = "Lâm Đồng"
                        },
                        new
                        {
                            id = 240000,
                            name = "Lạng Sơn"
                        },
                        new
                        {
                            id = 850000,
                            name = "Long An"
                        },
                        new
                        {
                            id = 460000,
                            name = "Nghệ An"
                        },
                        new
                        {
                            id = 430000,
                            name = "Ninh Bình"
                        },
                        new
                        {
                            id = 660000,
                            name = "Ninh Thuận"
                        },
                        new
                        {
                            id = 290000,
                            name = "Phú Thọ"
                        },
                        new
                        {
                            id = 620000,
                            name = "Phú Yên"
                        },
                        new
                        {
                            id = 510000,
                            name = "Quảng Bình"
                        },
                        new
                        {
                            id = 560000,
                            name = "Quảng Nam"
                        },
                        new
                        {
                            id = 570000,
                            name = "Quảng Ngãi"
                        },
                        new
                        {
                            id = 520000,
                            name = "Quảng Trị"
                        },
                        new
                        {
                            id = 950000,
                            name = "Sóc Trăng"
                        },
                        new
                        {
                            id = 360000,
                            name = "Sơn La"
                        },
                        new
                        {
                            id = 840000,
                            name = "Tây Ninh"
                        },
                        new
                        {
                            id = 410000,
                            name = "Thái Bình"
                        },
                        new
                        {
                            id = 250000,
                            name = "Thái Nguyên"
                        },
                        new
                        {
                            id = 440000,
                            name = "Thanh Hóa"
                        },
                        new
                        {
                            id = 860000,
                            name = "Tiền Giang"
                        },
                        new
                        {
                            id = 940000,
                            name = "Trà Vinh"
                        },
                        new
                        {
                            id = 300000,
                            name = "Tuyên Quang"
                        },
                        new
                        {
                            id = 890000,
                            name = "Vĩnh Long"
                        },
                        new
                        {
                            id = 280000,
                            name = "Vĩnh Phúc"
                        },
                        new
                        {
                            id = 320000,
                            name = "Yên Bái"
                        },
                        new
                        {
                            id = 630000,
                            name = "Đắk Lắk"
                        },
                        new
                        {
                            id = 870000,
                            name = "Đồng Tháp"
                        },
                        new
                        {
                            id = 550000,
                            name = "Đà Nẵng"
                        },
                        new
                        {
                            id = 640000,
                            name = "Đắk Nông"
                        },
                        new
                        {
                            id = 910000,
                            name = "Hậu Giang"
                        },
                        new
                        {
                            id = 380000,
                            name = "Điện Biên"
                        });
                });

            modelBuilder.Entity("e_commerce_server.src.Core.Database.Data.UserData", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<bool>("active_status")
                        .HasColumnType("bit");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("birthday")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime2");

                    b.Property<int?>("district_id")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool?>("gender")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("phone_number")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("reset_token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("reset_token_expiration_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("role_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            id = 1,
                            active_status = false,
                            created_at = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            email = "string@gmail.com",
                            name = "John Doe",
                            password = "string",
                            role_id = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
