using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 添加用户操作操作员 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "DeviceRequest",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9930));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 337, DateTimeKind.Local).AddTicks(6590));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operator",
                table: "DeviceRequest");

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
        }
    }
}
