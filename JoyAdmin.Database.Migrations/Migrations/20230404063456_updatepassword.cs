using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class updatepassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 4, 14, 34, 56, 151, DateTimeKind.Local).AddTicks(2745));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 4, 14, 34, 56, 151, DateTimeKind.Local).AddTicks(4815));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 4, 14, 34, 56, 151, DateTimeKind.Local).AddTicks(4819));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 4, 14, 34, 56, 151, DateTimeKind.Local).AddTicks(4821));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedTime", "Password" },
                values: new object[] { new DateTime(2023, 4, 4, 14, 34, 56, 152, DateTimeKind.Local).AddTicks(5260), "e10adc3949ba59abbe56e057f20f883e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 3, 16, 37, 5, 168, DateTimeKind.Local).AddTicks(1313));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 3, 16, 37, 5, 168, DateTimeKind.Local).AddTicks(3944));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 3, 16, 37, 5, 168, DateTimeKind.Local).AddTicks(3950));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 3, 16, 37, 5, 168, DateTimeKind.Local).AddTicks(3953));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedTime", "Password" },
                values: new object[] { new DateTime(2023, 4, 3, 16, 37, 5, 169, DateTimeKind.Local).AddTicks(4454), "c33367701511b4f6020ec61ded352059" });
        }
    }
}
