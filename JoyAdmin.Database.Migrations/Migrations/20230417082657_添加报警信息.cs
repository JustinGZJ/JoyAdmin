using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 添加报警信息 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Station = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmHistory", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 448, DateTimeKind.Local).AddTicks(8428));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 448, DateTimeKind.Local).AddTicks(9781));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 448, DateTimeKind.Local).AddTicks(9784));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 448, DateTimeKind.Local).AddTicks(9786));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 4, 17, 16, 26, 57, 449, DateTimeKind.Local).AddTicks(5701));

            migrationBuilder.CreateIndex(
                name: "IX_AlarmHistory_EndTime",
                table: "AlarmHistory",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmHistory_StartTime",
                table: "AlarmHistory",
                column: "StartTime");

            migrationBuilder.CreateIndex(
                name: "IX_AlarmHistory_Station",
                table: "AlarmHistory",
                column: "Station");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmHistory");

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
                column: "CreatedTime",
                value: new DateTime(2023, 4, 4, 14, 34, 56, 152, DateTimeKind.Local).AddTicks(5260));
        }
    }
}
