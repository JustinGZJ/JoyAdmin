using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class updatesntable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubmitTime",
                table: "Base_NumberRule",
                newName: "DateRule");

            migrationBuilder.RenameColumn(
                name: "SerialNumber",
                table: "Base_NumberRule",
                newName: "SNLength");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 11, 0, 58, 732, DateTimeKind.Local).AddTicks(7307));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 11, 0, 58, 732, DateTimeKind.Local).AddTicks(8765));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 11, 0, 58, 732, DateTimeKind.Local).AddTicks(8770));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 11, 0, 58, 732, DateTimeKind.Local).AddTicks(8772));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 8, 11, 0, 58, 733, DateTimeKind.Local).AddTicks(4906));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SNLength",
                table: "Base_NumberRule",
                newName: "SerialNumber");

            migrationBuilder.RenameColumn(
                name: "DateRule",
                table: "Base_NumberRule",
                newName: "SubmitTime");

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
    }
}
