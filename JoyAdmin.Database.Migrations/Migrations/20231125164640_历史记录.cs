using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 历史记录 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseProcessList_Base_Process_Process_Id1",
                table: "BaseProcessList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseProcessList",
                table: "BaseProcessList");

            migrationBuilder.RenameTable(
                name: "BaseProcessList",
                newName: "Base_ProcessList");

            migrationBuilder.RenameColumn(
                name: "Process_Id1",
                table: "Base_ProcessList",
                newName: "Base_ProcessProcess_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BaseProcessList_Process_Id1",
                table: "Base_ProcessList",
                newName: "IX_Base_ProcessList_Base_ProcessProcess_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Base_ProcessList",
                table: "Base_ProcessList",
                column: "ProcessList_Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_Base_ProcessList_Process_Id",
                table: "Base_ProcessList",
                column: "Process_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_ProcessList_Base_Process_Base_ProcessProcess_Id",
                table: "Base_ProcessList",
                column: "Base_ProcessProcess_Id",
                principalTable: "Base_Process",
                principalColumn: "Process_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_ProcessList_Base_Process_Process_Id",
                table: "Base_ProcessList",
                column: "Process_Id",
                principalTable: "Base_Process",
                principalColumn: "Process_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_ProcessList_Base_Process_Base_ProcessProcess_Id",
                table: "Base_ProcessList");

            migrationBuilder.DropForeignKey(
                name: "FK_Base_ProcessList_Base_Process_Process_Id",
                table: "Base_ProcessList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Base_ProcessList",
                table: "Base_ProcessList");

            migrationBuilder.DropIndex(
                name: "IX_Base_ProcessList_Process_Id",
                table: "Base_ProcessList");

            migrationBuilder.RenameTable(
                name: "Base_ProcessList",
                newName: "BaseProcessList");

            migrationBuilder.RenameColumn(
                name: "Base_ProcessProcess_Id",
                table: "BaseProcessList",
                newName: "Process_Id1");

            migrationBuilder.RenameIndex(
                name: "IX_Base_ProcessList_Base_ProcessProcess_Id",
                table: "BaseProcessList",
                newName: "IX_BaseProcessList_Process_Id1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseProcessList",
                table: "BaseProcessList",
                column: "ProcessList_Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_BaseProcessList_Base_Process_Process_Id1",
                table: "BaseProcessList",
                column: "Process_Id1",
                principalTable: "Base_Process",
                principalColumn: "Process_Id");
        }
    }
}
