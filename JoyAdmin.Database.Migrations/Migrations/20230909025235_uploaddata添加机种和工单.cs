using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class uploaddata添加机种和工单 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "UploadData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkOrder",
                table: "UploadData",
                type: "text",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "UploadData");

            migrationBuilder.DropColumn(
                name: "WorkOrder",
                table: "UploadData");

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
    }
}
