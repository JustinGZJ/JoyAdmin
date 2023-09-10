using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 更新统计表单关联添加遥测数据表 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NgQuantity",
                table: "ProductionWorkOrder",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "Production",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkOrderNo",
                table: "Production",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Telemetry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityName = table.Column<string>(type: "text", nullable: true),
                    DataType = table.Column<int>(type: "integer", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    ValueNum = table.Column<double>(type: "double precision", nullable: true),
                    ValueBool = table.Column<bool>(type: "boolean", nullable: true),
                    ValueStr = table.Column<string>(type: "text", nullable: true),
                    ValueJson = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telemetry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TelemetryData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemetryData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelemetryData_Telemetry_Id",
                        column: x => x.Id,
                        principalTable: "Telemetry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TelemetryLatest",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemetryLatest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelemetryLatest_Telemetry_Id",
                        column: x => x.Id,
                        principalTable: "Telemetry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Telemetry_EntityName",
                table: "Telemetry",
                column: "EntityName");

            migrationBuilder.CreateIndex(
                name: "IX_Telemetry_Key",
                table: "Telemetry",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TelemetryData");

            migrationBuilder.DropTable(
                name: "TelemetryLatest");

            migrationBuilder.DropTable(
                name: "Telemetry");

            migrationBuilder.DropColumn(
                name: "NgQuantity",
                table: "ProductionWorkOrder");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "Production");

            migrationBuilder.DropColumn(
                name: "WorkOrderNo",
                table: "Production");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(1520));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(3010));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(3020));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 9, 10, 15, 49, 38, 292, DateTimeKind.Local).AddTicks(8840));
        }
    }
}
