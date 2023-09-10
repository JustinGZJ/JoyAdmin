using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 流水号添加备注 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Base_NumberRule",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(3020));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(8840));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Base_NumberRule");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 9, 10, 52, 34, 568, DateTimeKind.Local).AddTicks(2990));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 9, 10, 52, 34, 568, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 9, 10, 52, 34, 568, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 9, 10, 52, 34, 568, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 9, 10, 52, 34, 569, DateTimeKind.Local).AddTicks(2650));
        }
    }
}
