using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class updatedatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLine_Id",
                table: "BaseProcessLineList");

            migrationBuilder.RenameColumn(
                name: "ProcessLine_Id",
                table: "BaseProcessLineList",
                newName: "ProcessLineId");

            migrationBuilder.RenameColumn(
                name: "ProcessLineList_Id",
                table: "BaseProcessLineList",
                newName: "ProcessLineListId");

            migrationBuilder.RenameIndex(
                name: "IX_BaseProcessLineList_ProcessLine_Id",
                table: "BaseProcessLineList",
                newName: "IX_BaseProcessLineList_ProcessLineId");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2024, 1, 25, 23, 37, 56, 812, DateTimeKind.Local).AddTicks(1230));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2024, 1, 25, 23, 37, 56, 812, DateTimeKind.Local).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2024, 1, 25, 23, 37, 56, 812, DateTimeKind.Local).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2024, 1, 25, 23, 37, 56, 812, DateTimeKind.Local).AddTicks(4050));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2024, 1, 25, 23, 37, 56, 813, DateTimeKind.Local).AddTicks(720));

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLineId",
                table: "BaseProcessLineList",
                column: "ProcessLineId",
                principalTable: "Base_ProcessLine",
                principalColumn: "ProcessLine_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLineId",
                table: "BaseProcessLineList");

            migrationBuilder.RenameColumn(
                name: "ProcessLineId",
                table: "BaseProcessLineList",
                newName: "ProcessLine_Id");

            migrationBuilder.RenameColumn(
                name: "ProcessLineListId",
                table: "BaseProcessLineList",
                newName: "ProcessLineList_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BaseProcessLineList_ProcessLineId",
                table: "BaseProcessLineList",
                newName: "IX_BaseProcessLineList_ProcessLine_Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLine_Id",
                table: "BaseProcessLineList",
                column: "ProcessLine_Id",
                principalTable: "Base_ProcessLine",
                principalColumn: "ProcessLine_Id");
        }
    }
}
