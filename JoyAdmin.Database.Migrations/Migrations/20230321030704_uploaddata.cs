using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class uploaddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShellCodeBinding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShellCode = table.Column<string>(type: "text", nullable: true),
                    StatorCode = table.Column<string>(type: "text", nullable: true),
                    RotorCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShellCodeBinding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UploadData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Result = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadData", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 837, DateTimeKind.Local).AddTicks(4134));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 837, DateTimeKind.Local).AddTicks(5727));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 837, DateTimeKind.Local).AddTicks(5731));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 837, DateTimeKind.Local).AddTicks(5733));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 21, 11, 7, 3, 838, DateTimeKind.Local).AddTicks(2369));

            migrationBuilder.CreateIndex(
                name: "IX_ShellCodeBinding_ShellCode",
                table: "ShellCodeBinding",
                column: "ShellCode");

            migrationBuilder.CreateIndex(
                name: "IX_UploadData_Code",
                table: "UploadData",
                column: "Code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShellCodeBinding");

            migrationBuilder.DropTable(
                name: "UploadData");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 20, 15, 22, 31, 267, DateTimeKind.Local).AddTicks(5766));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 20, 15, 22, 31, 267, DateTimeKind.Local).AddTicks(8201));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 20, 15, 22, 31, 267, DateTimeKind.Local).AddTicks(8207));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 20, 15, 22, 31, 267, DateTimeKind.Local).AddTicks(8210));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 3, 20, 15, 22, 31, 269, DateTimeKind.Local).AddTicks(2169));
        }
    }
}
