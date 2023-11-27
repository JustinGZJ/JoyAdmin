using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 过程控制修改字段 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Production_ProductRecord",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Result",
                table: "Production_ProcessRecord",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "ProcessLineType",
                table: "BaseProcessLineList",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 23, 7, 25, 126, DateTimeKind.Local).AddTicks(8450));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 23, 7, 25, 127, DateTimeKind.Local).AddTicks(3700));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 23, 7, 25, 127, DateTimeKind.Local).AddTicks(3710));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 23, 7, 25, 127, DateTimeKind.Local).AddTicks(3720));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 23, 7, 25, 128, DateTimeKind.Local).AddTicks(5060));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Production_ProductRecord");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Production_ProcessRecord");

            migrationBuilder.AlterColumn<string>(
                name: "ProcessLineType",
                table: "BaseProcessLineList",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 0, 46, 39, 890, DateTimeKind.Local).AddTicks(8510));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 0, 46, 39, 891, DateTimeKind.Local).AddTicks(540));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 0, 46, 39, 891, DateTimeKind.Local).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 0, 46, 39, 891, DateTimeKind.Local).AddTicks(550));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 26, 0, 46, 39, 891, DateTimeKind.Local).AddTicks(4980));
        }
    }
}
