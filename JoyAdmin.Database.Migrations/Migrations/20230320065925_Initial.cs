using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RealIp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserAgent = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Referer = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<long>(type: "bigint", maxLength: 50, nullable: false),
                    UserAccount = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedUserId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Remark = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedUserId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Security",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    UniqueCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UniqueName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedUserId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Security", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Account = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LinkPost = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Remark = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedUserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ModifiedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedUserId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedUser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleSecurity",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    SecurityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleSecurity", x => new { x.RoleId, x.SecurityId });
                    table.ForeignKey(
                        name: "FK_RoleSecurity_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleSecurity_Security_SecurityId",
                        column: x => x.SecurityId,
                        principalTable: "Security",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "CreatedTime", "CreatedUser", "CreatedUserId", "IsDeleted", "ModifiedTime", "ModifiedUser", "ModifiedUserId", "Name", "Remark", "Sort" },
                values: new object[] { 1L, new DateTime(2023, 3, 20, 14, 59, 25, 574, DateTimeKind.Local).AddTicks(6401), null, 0L, false, null, null, 0L, "管理员", "拥有系统管理权限", 0 });

            migrationBuilder.InsertData(
                table: "Security",
                columns: new[] { "Id", "CreatedTime", "CreatedUser", "CreatedUserId", "IsDeleted", "ModifiedTime", "ModifiedUser", "ModifiedUserId", "Sort", "UniqueCode", "UniqueName" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 3, 20, 14, 59, 25, 574, DateTimeKind.Local).AddTicks(7783), null, 0L, false, null, null, 0L, 0, "role", "角色管理" },
                    { 2L, new DateTime(2023, 3, 20, 14, 59, 25, 574, DateTimeKind.Local).AddTicks(7787), null, 0L, false, null, null, 0L, 0, "auth", "权限管理" },
                    { 3L, new DateTime(2023, 3, 20, 14, 59, 25, 574, DateTimeKind.Local).AddTicks(7789), null, 0L, false, null, null, 0L, 0, "employee", "员工管理" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Account", "CreatedTime", "CreatedUser", "CreatedUserId", "IsDeleted", "LinkPost", "ModifiedTime", "ModifiedUser", "ModifiedUserId", "Name", "Password", "Phone", "Remark" },
                values: new object[] { 1L, "admin", new DateTime(2023, 3, 20, 14, 59, 25, 575, DateTimeKind.Local).AddTicks(4730), null, 0L, false, null, null, null, 0L, null, "c33367701511b4f6020ec61ded352059", null, null });

            migrationBuilder.InsertData(
                table: "RoleSecurity",
                columns: new[] { "RoleId", "SecurityId" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 1L, 2L },
                    { 1L, 3L }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.CreateIndex(
                name: "IX_RoleSecurity_SecurityId",
                table: "RoleSecurity",
                column: "SecurityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPLog");

            migrationBuilder.DropTable(
                name: "RoleSecurity");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Security");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
