using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class telemetryjson属性 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValueBool",
                table: "Telemetry");

            migrationBuilder.DropColumn(
                name: "ValueJson",
                table: "Telemetry");

            migrationBuilder.DropColumn(
                name: "ValueNum",
                table: "Telemetry");

            migrationBuilder.DropColumn(
                name: "ValueStr",
                table: "Telemetry");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "Telemetry",
                type: "jsonb",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 43, 42, 868, DateTimeKind.Local).AddTicks(8599));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 43, 42, 868, DateTimeKind.Local).AddTicks(9976));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 43, 42, 868, DateTimeKind.Local).AddTicks(9980));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 43, 42, 868, DateTimeKind.Local).AddTicks(9981));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 43, 42, 869, DateTimeKind.Local).AddTicks(5561));

            migrationBuilder.CreateIndex(
                name: "IX_Production_Model",
                table: "Production",
                column: "Model");

            migrationBuilder.CreateIndex(
                name: "IX_Production_WorkOrderNo",
                table: "Production",
                column: "WorkOrderNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Production_Model",
                table: "Production");

            migrationBuilder.DropIndex(
                name: "IX_Production_WorkOrderNo",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Telemetry");

            migrationBuilder.AddColumn<bool>(
                name: "ValueBool",
                table: "Telemetry",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValueJson",
                table: "Telemetry",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ValueNum",
                table: "Telemetry",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValueStr",
                table: "Telemetry",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 22, 9, 32, 27, DateTimeKind.Local).AddTicks(1070));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 22, 9, 32, 27, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 22, 9, 32, 27, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 22, 9, 32, 27, DateTimeKind.Local).AddTicks(2780));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 22, 9, 32, 27, DateTimeKind.Local).AddTicks(9020));
        }
    }
}
