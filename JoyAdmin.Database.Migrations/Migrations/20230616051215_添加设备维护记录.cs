using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 添加设备维护记录 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "ShellCodeBinding",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DeviceRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceName = table.Column<string>(type: "text", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RequestMessage = table.Column<string>(type: "text", nullable: true),
                    IsHandled = table.Column<bool>(type: "boolean", nullable: false),
                    CompletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CompletionMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRequest", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 16, 13, 12, 14, 777, DateTimeKind.Local).AddTicks(2410));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 16, 13, 12, 14, 777, DateTimeKind.Local).AddTicks(3680));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 16, 13, 12, 14, 777, DateTimeKind.Local).AddTicks(3690));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 16, 13, 12, 14, 777, DateTimeKind.Local).AddTicks(3690));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 16, 13, 12, 14, 777, DateTimeKind.Local).AddTicks(7870));

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRequest_RequestTime",
                table: "DeviceRequest",
                column: "RequestTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceRequest");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "ShellCodeBinding");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 448, DateTimeKind.Local).AddTicks(8428));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 448, DateTimeKind.Local).AddTicks(9781));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 448, DateTimeKind.Local).AddTicks(9784));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 448, DateTimeKind.Local).AddTicks(9786));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 449, DateTimeKind.Local).AddTicks(5701));
        }
    }
}
