using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 修复一些表结构的问题 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_Product_Base_ProcessLine_ProcessLine_Id1",
                table: "Base_Product");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLineId",
                table: "BaseProcessLineList");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseProcessList_Base_Process_ProcessId",
                table: "BaseProcessList");

            migrationBuilder.DropIndex(
                name: "IX_BaseProcessList_ProcessId",
                table: "BaseProcessList");

            migrationBuilder.DropIndex(
                name: "IX_Base_Product_ProcessLine_Id1",
                table: "Base_Product");

            migrationBuilder.DropColumn(
                name: "ProcessLine_Id1",
                table: "Base_Product");

            migrationBuilder.RenameColumn(
                name: "ProcessId",
                table: "BaseProcessList",
                newName: "Process_Id");

            migrationBuilder.RenameColumn(
                name: "ProcessListId",
                table: "BaseProcessList",
                newName: "ProcessList_Id");

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

            migrationBuilder.AddColumn<int>(
                name: "Process_Id1",
                table: "BaseProcessList",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 25, 22, 34, 10, 117, DateTimeKind.Local).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 25, 22, 34, 10, 117, DateTimeKind.Local).AddTicks(9760));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 25, 22, 34, 10, 117, DateTimeKind.Local).AddTicks(9770));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 25, 22, 34, 10, 117, DateTimeKind.Local).AddTicks(9770));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 25, 22, 34, 10, 118, DateTimeKind.Local).AddTicks(4330));

            migrationBuilder.CreateIndex(
                name: "IX_BaseProcessList_Process_Id1",
                table: "BaseProcessList",
                column: "Process_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Base_Product_ProcessLine_Id",
                table: "Base_Product",
                column: "ProcessLine_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Product_Base_ProcessLine_ProcessLine_Id",
                table: "Base_Product",
                column: "ProcessLine_Id",
                principalTable: "Base_ProcessLine",
                principalColumn: "ProcessLine_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLine_Id",
                table: "BaseProcessLineList",
                column: "ProcessLine_Id",
                principalTable: "Base_ProcessLine",
                principalColumn: "ProcessLine_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProcessList_Base_Process_Process_Id1",
                table: "BaseProcessList",
                column: "Process_Id1",
                principalTable: "Base_Process",
                principalColumn: "Process_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_Product_Base_ProcessLine_ProcessLine_Id",
                table: "Base_Product");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLine_Id",
                table: "BaseProcessLineList");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseProcessList_Base_Process_Process_Id1",
                table: "BaseProcessList");

            migrationBuilder.DropIndex(
                name: "IX_BaseProcessList_Process_Id1",
                table: "BaseProcessList");

            migrationBuilder.DropIndex(
                name: "IX_Base_Product_ProcessLine_Id",
                table: "Base_Product");

            migrationBuilder.DropColumn(
                name: "Process_Id1",
                table: "BaseProcessList");

            migrationBuilder.RenameColumn(
                name: "Process_Id",
                table: "BaseProcessList",
                newName: "ProcessId");

            migrationBuilder.RenameColumn(
                name: "ProcessList_Id",
                table: "BaseProcessList",
                newName: "ProcessListId");

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

            migrationBuilder.AddColumn<int>(
                name: "ProcessLine_Id1",
                table: "Base_Product",
                type: "integer",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 23, 23, 46, 3, 809, DateTimeKind.Local).AddTicks(9050));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 23, 23, 46, 3, 810, DateTimeKind.Local).AddTicks(910));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 23, 23, 46, 3, 810, DateTimeKind.Local).AddTicks(910));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 23, 23, 46, 3, 810, DateTimeKind.Local).AddTicks(920));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 23, 23, 46, 3, 810, DateTimeKind.Local).AddTicks(6690));

            migrationBuilder.CreateIndex(
                name: "IX_BaseProcessList_ProcessId",
                table: "BaseProcessList",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Base_Product_ProcessLine_Id1",
                table: "Base_Product",
                column: "ProcessLine_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Product_Base_ProcessLine_ProcessLine_Id1",
                table: "Base_Product",
                column: "ProcessLine_Id1",
                principalTable: "Base_ProcessLine",
                principalColumn: "ProcessLine_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLineId",
                table: "BaseProcessLineList",
                column: "ProcessLineId",
                principalTable: "Base_ProcessLine",
                principalColumn: "ProcessLine_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProcessList_Base_Process_ProcessId",
                table: "BaseProcessList",
                column: "ProcessId",
                principalTable: "Base_Process",
                principalColumn: "Process_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
