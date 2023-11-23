using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 过程控制 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Base_ProcessLineList");

            migrationBuilder.DropTable(
                name: "Base_ProcessList");

            migrationBuilder.AddColumn<int>(
                name: "ProcessLine_Id1",
                table: "Base_Product",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BaseProcessLineList",
                columns: table => new
                {
                    ProcessLineListId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcessLineId = table.Column<int>(type: "integer", nullable: true),
                    ProcessLineType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Process_Id = table.Column<int>(type: "integer", nullable: true),
                    ProcessLineDown_Id = table.Column<int>(type: "integer", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    SubmitWorkMatch = table.Column<decimal>(type: "numeric", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseProcessLineList", x => x.ProcessLineListId);
                    table.ForeignKey(
                        name: "FK_BaseProcessLineList_Base_Process_Process_Id",
                        column: x => x.Process_Id,
                        principalTable: "Base_Process",
                        principalColumn: "Process_Id");
                    table.ForeignKey(
                        name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLineDown_Id",
                        column: x => x.ProcessLineDown_Id,
                        principalTable: "Base_ProcessLine",
                        principalColumn: "ProcessLine_Id");
                    table.ForeignKey(
                        name: "FK_BaseProcessLineList_Base_ProcessLine_ProcessLineId",
                        column: x => x.ProcessLineId,
                        principalTable: "Base_ProcessLine",
                        principalColumn: "ProcessLine_Id");
                });

            migrationBuilder.CreateTable(
                name: "BaseProcessList",
                columns: table => new
                {
                    ProcessListId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcessId = table.Column<int>(type: "integer", nullable: false),
                    DataPointType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DataPointName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateId = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseProcessList", x => x.ProcessListId);
                    table.ForeignKey(
                        name: "FK_BaseProcessList_Base_Process_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Base_Process",
                        principalColumn: "Process_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Production_ProductRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BarCode = table.Column<string>(type: "text", nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    CurrentProcessId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production_ProductRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Production_ProductRecord_Base_Process_CurrentProcessId",
                        column: x => x.CurrentProcessId,
                        principalTable: "Base_Process",
                        principalColumn: "Process_Id");
                    table.ForeignKey(
                        name: "FK_Production_ProductRecord_Base_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Base_Product",
                        principalColumn: "Product_Id");
                });

            migrationBuilder.CreateTable(
                name: "Production_ProcessRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnterTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LeaveTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProcessId = table.Column<int>(type: "integer", nullable: true),
                    ProductRecordId = table.Column<int>(type: "integer", nullable: true),
                    DataId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Production_ProcessRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Production_ProcessRecord_Base_Process_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Base_Process",
                        principalColumn: "Process_Id");
                    table.ForeignKey(
                        name: "FK_Production_ProcessRecord_Production_ProductRecord_ProductRe~",
                        column: x => x.ProductRecordId,
                        principalTable: "Production_ProductRecord",
                        principalColumn: "Id");
                });

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
                name: "IX_Base_Product_ProcessLine_Id1",
                table: "Base_Product",
                column: "ProcessLine_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProcessLineList_Process_Id",
                table: "BaseProcessLineList",
                column: "Process_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProcessLineList_ProcessLineDown_Id",
                table: "BaseProcessLineList",
                column: "ProcessLineDown_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProcessLineList_ProcessLineId",
                table: "BaseProcessLineList",
                column: "ProcessLineId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseProcessList_ProcessId",
                table: "BaseProcessList",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Production_ProcessRecord_ProcessId",
                table: "Production_ProcessRecord",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Production_ProcessRecord_ProductRecordId",
                table: "Production_ProcessRecord",
                column: "ProductRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Production_ProductRecord_CurrentProcessId",
                table: "Production_ProductRecord",
                column: "CurrentProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Production_ProductRecord_ProductId",
                table: "Production_ProductRecord",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Base_Product_Base_ProcessLine_ProcessLine_Id1",
                table: "Base_Product",
                column: "ProcessLine_Id1",
                principalTable: "Base_ProcessLine",
                principalColumn: "ProcessLine_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Base_Product_Base_ProcessLine_ProcessLine_Id1",
                table: "Base_Product");

            migrationBuilder.DropTable(
                name: "BaseProcessLineList");

            migrationBuilder.DropTable(
                name: "BaseProcessList");

            migrationBuilder.DropTable(
                name: "Production_ProcessRecord");

            migrationBuilder.DropTable(
                name: "Production_ProductRecord");

            migrationBuilder.DropIndex(
                name: "IX_Base_Product_ProcessLine_Id1",
                table: "Base_Product");

            migrationBuilder.DropColumn(
                name: "ProcessLine_Id1",
                table: "Base_Product");

            migrationBuilder.CreateTable(
                name: "Base_ProcessLineList",
                columns: table => new
                {
                    ProcessLineList_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true),
                    ProcessLineDown_Id = table.Column<int>(type: "integer", nullable: true),
                    ProcessLineType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ProcessLine_Id = table.Column<int>(type: "integer", nullable: false),
                    Process_Id = table.Column<int>(type: "integer", nullable: true),
                    Sequence = table.Column<int>(type: "integer", nullable: false),
                    SubmitWorkMatch = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_ProcessLineList", x => x.ProcessLineList_Id);
                    table.ForeignKey(
                        name: "FK_Base_ProcessLineList_Base_ProcessLine_ProcessLine_Id",
                        column: x => x.ProcessLine_Id,
                        principalTable: "Base_ProcessLine",
                        principalColumn: "ProcessLine_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Base_ProcessList",
                columns: table => new
                {
                    ProcessList_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DataPointName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DataPointType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true),
                    Process_Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_ProcessList", x => x.ProcessList_Id);
                    table.ForeignKey(
                        name: "FK_Base_ProcessList_Base_Process_Process_Id",
                        column: x => x.Process_Id,
                        principalTable: "Base_Process",
                        principalColumn: "Process_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 59, 49, 839, DateTimeKind.Local).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 59, 49, 840, DateTimeKind.Local).AddTicks(1136));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 59, 49, 840, DateTimeKind.Local).AddTicks(1141));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 59, 49, 840, DateTimeKind.Local).AddTicks(1143));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 11, 21, 9, 59, 49, 840, DateTimeKind.Local).AddTicks(7440));

            migrationBuilder.CreateIndex(
                name: "IX_Base_ProcessLineList_ProcessLine_Id",
                table: "Base_ProcessLineList",
                column: "ProcessLine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Base_ProcessList_Process_Id",
                table: "Base_ProcessList",
                column: "Process_Id");
        }
    }
}
