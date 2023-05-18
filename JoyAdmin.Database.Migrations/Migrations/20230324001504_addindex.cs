using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class addindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 24, 8, 15, 3, 981, DateTimeKind.Local).AddTicks(201));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 24, 8, 15, 3, 981, DateTimeKind.Local).AddTicks(1865));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 24, 8, 15, 3, 981, DateTimeKind.Local).AddTicks(1869));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 24, 8, 15, 3, 981, DateTimeKind.Local).AddTicks(1870));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 24, 8, 15, 3, 981, DateTimeKind.Local).AddTicks(7842));

            migrationBuilder.CreateIndex(
                name: "IX_ShellCodeBinding_RotorCode",
                table: "ShellCodeBinding",
                column: "RotorCode");

            migrationBuilder.CreateIndex(
                name: "IX_ShellCodeBinding_StatorCode",
                table: "ShellCodeBinding",
                column: "StatorCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShellCodeBinding_RotorCode",
                table: "ShellCodeBinding");

            migrationBuilder.DropIndex(
                name: "IX_ShellCodeBinding_StatorCode",
                table: "ShellCodeBinding");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 837, DateTimeKind.Local).AddTicks(4134));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 837, DateTimeKind.Local).AddTicks(5727));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 837, DateTimeKind.Local).AddTicks(5731));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 837, DateTimeKind.Local).AddTicks(5733));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 838, DateTimeKind.Local).AddTicks(2369));
        }
    }
}
