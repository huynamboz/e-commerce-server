﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_commerce_server.Src.Core.Database.Data;

#nullable disable

namespace e_commerce_server.Src.Core.Database.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230331023941_create_users_table")]
    partial class create_users_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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

                    b.HasIndex("id")
                        .IsUnique();

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            id = 1,
                            active_status = false,
                            created_at = new DateTime(2023, 3, 31, 9, 39, 41, 662, DateTimeKind.Local).AddTicks(7162),
                            email = "test@gmail.com",
                            name = "John Doe",
                            password = "string",
                            role_id = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}