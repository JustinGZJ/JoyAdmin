using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class updateproducttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Process_Id",
                table: "Base_Product",
                newName: "ProcessLine_Id");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 27, 21, 35, 20, 368, DateTimeKind.Local).AddTicks(2820));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 27, 21, 35, 20, 368, DateTimeKind.Local).AddTicks(4020));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 27, 21, 35, 20, 368, DateTimeKind.Local).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 27, 21, 35, 20, 368, DateTimeKind.Local).AddTicks(4030));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 27, 21, 35, 20, 368, DateTimeKind.Local).AddTicks(8010));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProcessLine_Id",
                table: "Base_Product",
                newName: "Process_Id");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 11, 4, 2, 198, DateTimeKind.Local).AddTicks(430));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 11, 4, 2, 198, DateTimeKind.Local).AddTicks(1630));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 11, 4, 2, 198, DateTimeKind.Local).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 11, 4, 2, 198, DateTimeKind.Local).AddTicks(1640));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 18, 11, 4, 2, 198, DateTimeKind.Local).AddTicks(5610));
        }
    }
}
