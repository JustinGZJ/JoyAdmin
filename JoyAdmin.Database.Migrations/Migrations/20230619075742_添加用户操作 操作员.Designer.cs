﻿// <auto-generated />
using System;
using JoyAdmin.EntityFramework.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    [DbContext(typeof(DefaultDbContext))]
    [Migration("20230619075742_添加用户操作 操作员")]
    partial class 添加用户操作操作员
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JoyAdmin.Core.Entities.Storage.AlarmHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Station")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("EndTime");

                    b.HasIndex("StartTime");

                    b.HasIndex("Station");

                    b.ToTable("AlarmHistory", (string)null);
                });

            modelBuilder.Entity("JoyAdmin.Core.Entities.Storage.DeviceRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CompletionMessage")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CompletionTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DeviceName")
                        .HasColumnType("text");

                    b.Property<bool>("IsHandled")
                        .HasColumnType("boolean");

                    b.Property<string>("Operator")
                        .HasColumnType("text");

                    b.Property<string>("RequestMessage")
                        .HasColumnType("text");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("RequestTime");

                    b.ToTable("DeviceRequest", (string)null);
                });

            modelBuilder.Entity("JoyAdmin.Core.Entities.Storage.Production", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<string>("Device")
                        .HasColumnType("text");

                    b.Property<int>("ProductionType")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Device");

                    b.HasIndex("ProductionType");

                    b.ToTable("Production", (string)null);
                });

            modelBuilder.Entity("JoyAdmin.Core.Entities.Storage.ShellCodeBinding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("RotorCode")
                        .HasColumnType("text");

                    b.Property<string>("ShellCode")
                        .HasColumnType("text");

                    b.Property<string>("StatorCode")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RotorCode");

                    b.HasIndex("ShellCode");

                    b.HasIndex("StatorCode");

                    b.ToTable("ShellCodeBinding", (string)null);
                });

            modelBuilder.Entity("JoyAdmin.Core.Entities.Storage.UploadData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("Result")
                        .HasColumnType("text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Code");

                    b.ToTable("UploadData", (string)null);
                });

            modelBuilder.Entity("JoyAdmin.Core.IPLog", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Ip")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedUser")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("ModifiedUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("RealIp")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Referer")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("UserAccount")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("UserAgent")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<long>("UserId")
                        .HasMaxLength(50)
                        .HasColumnType("bigint");

                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("IPLog", (string)null);
                });

            modelBuilder.Entity("JoyAdmin.Core.Role", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedUser")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("ModifiedUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Remark")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<int>("Sort")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Role", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(7980),
                            CreatedUserId = 0L,
                            IsDeleted = false,
                            ModifiedUserId = 0L,
                            Name = "管理员",
                            Remark = "拥有系统管理权限",
                            Sort = 0
                        });
                });

            modelBuilder.Entity("JoyAdmin.Core.RoleSecurity", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("SecurityId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "SecurityId");

                    b.HasIndex("SecurityId");

                    b.ToTable("RoleSecurity", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1L,
                            SecurityId = 1L
                        },
                        new
                        {
                            RoleId = 1L,
                            SecurityId = 2L
                        },
                        new
                        {
                            RoleId = 1L,
                            SecurityId = 3L
                        });
                });

            modelBuilder.Entity("JoyAdmin.Core.Security", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedUser")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("ModifiedUserId")
                        .HasColumnType("bigint");

                    b.Property<int>("Sort")
                        .HasColumnType("integer");

                    b.Property<string>("UniqueCode")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("UniqueName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Security", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedTime = new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9910),
                            CreatedUserId = 0L,
                            IsDeleted = false,
                            ModifiedUserId = 0L,
                            Sort = 0,
                            UniqueCode = "role",
                            UniqueName = "角色管理"
                        },
                        new
                        {
                            Id = 2L,
                            CreatedTime = new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9920),
                            CreatedUserId = 0L,
                            IsDeleted = false,
                            ModifiedUserId = 0L,
                            Sort = 0,
                            UniqueCode = "auth",
                            UniqueName = "权限管理"
                        },
                        new
                        {
                            Id = 3L,
                            CreatedTime = new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9930),
                            CreatedUserId = 0L,
                            IsDeleted = false,
                            ModifiedUserId = 0L,
                            Sort = 0,
                            UniqueCode = "employee",
                            UniqueName = "员工管理"
                        });
                });

            modelBuilder.Entity("JoyAdmin.Core.User", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("Account")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedUser")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("CreatedUserId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LinkPost")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedUser")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<long>("ModifiedUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Account = "admin",
                            CreatedTime = new DateTime(2023, 6, 19, 15, 57, 42, 337, DateTimeKind.Local).AddTicks(6590),
                            CreatedUserId = 0L,
                            IsDeleted = false,
                            ModifiedUserId = 0L,
                            Password = "e10adc3949ba59abbe56e057f20f883e"
                        });
                });

            modelBuilder.Entity("JoyAdmin.Core.UserRole", b =>
                {
                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1L,
                            RoleId = 1L
                        });
                });

            modelBuilder.Entity("JoyAdmin.Core.RoleSecurity", b =>
                {
                    b.HasOne("JoyAdmin.Core.Role", "Role")
                        .WithMany("RoleSecurities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JoyAdmin.Core.Security", "Security")
                        .WithMany("RoleSecurities")
                        .HasForeignKey("SecurityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Security");
                });

            modelBuilder.Entity("JoyAdmin.Core.UserRole", b =>
                {
                    b.HasOne("JoyAdmin.Core.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JoyAdmin.Core.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JoyAdmin.Core.Role", b =>
                {
                    b.Navigation("RoleSecurities");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("JoyAdmin.Core.Security", b =>
                {
                    b.Navigation("RoleSecurities");
                });

            modelBuilder.Entity("JoyAdmin.Core.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
