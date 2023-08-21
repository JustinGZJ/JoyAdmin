using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JoyAdmin.Database.Migrations.Migrations
{
    public partial class 添加工艺路线 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Base_DefectItem",
                columns: table => new
                {
                    DefectItem_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DefectItemCode = table.Column<string>(type: "text", nullable: true),
                    DefectItemName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Attachment = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "text", nullable: true),
                    Modifier = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_DefectItem", x => x.DefectItem_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_DesktopMenu",
                columns: table => new
                {
                    DesktopMenu_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuId = table.Column<int>(type: "integer", nullable: false),
                    MenuName = table.Column<string>(type: "text", nullable: false),
                    MenuUrl = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Enable = table.Column<byte>(type: "smallint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_DesktopMenu", x => x.DesktopMenu_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_MaterialDetail",
                columns: table => new
                {
                    MaterialDetail_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentProduct_Id = table.Column<int>(type: "integer", nullable: false),
                    ChildProduct_Id = table.Column<int>(type: "integer", nullable: false),
                    QuantityPer = table.Column<int>(type: "integer", nullable: false),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "text", nullable: true),
                    Modifier = table.Column<string>(type: "text", nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_MaterialDetail", x => x.MaterialDetail_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_MeritPay",
                columns: table => new
                {
                    MeritPay_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Process_Id = table.Column<int>(type: "integer", nullable: true),
                    Unit_Id = table.Column<int>(type: "integer", nullable: true),
                    Product_Id = table.Column<int>(type: "integer", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    StandardNumber = table.Column<int>(type: "integer", nullable: true),
                    StandardHour = table.Column<int>(type: "integer", nullable: true),
                    StandardMin = table.Column<int>(type: "integer", nullable: true),
                    StandardSec = table.Column<int>(type: "integer", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_MeritPay", x => x.MeritPay_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_Notice",
                columns: table => new
                {
                    Notice_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NoticeType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NoticeTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    NoticeContent = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_Notice", x => x.Notice_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_NumberRule",
                columns: table => new
                {
                    NumberRule_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FormCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Prefix = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SubmitTime = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SerialNumber = table.Column<int>(type: "integer", nullable: false),
                    GenerativeRule = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_NumberRule", x => x.NumberRule_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_Process",
                columns: table => new
                {
                    Process_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcessCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProcessName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SubmitWorkLimit = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    SubmitWorkMatch = table.Column<decimal>(type: "numeric", nullable: false),
                    DefectItem = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_Process", x => x.Process_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_ProcessLine",
                columns: table => new
                {
                    ProcessLine_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcessLineCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProcessLineName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_ProcessLine", x => x.ProcessLine_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_Product",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Unit_Id = table.Column<int>(type: "integer", nullable: false),
                    ProductStandard = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ProductAttribute = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Process_Id = table.Column<int>(type: "integer", nullable: true),
                    MaxInventory = table.Column<int>(type: "integer", nullable: true),
                    MinInventory = table.Column<int>(type: "integer", nullable: true),
                    SafeInventory = table.Column<int>(type: "integer", nullable: true),
                    InventoryQty = table.Column<int>(type: "integer", nullable: true),
                    FinishedProduct = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_Product", x => x.Product_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_WorkShop",
                columns: table => new
                {
                    WorkShopId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkShopName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WorkShopCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MainPerson = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Area = table.Column<int>(type: "integer", nullable: true),
                    Enable = table.Column<int>(type: "integer", nullable: false),
                    Remark = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true),
                    Modifier = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Base_WorkShop", x => x.WorkShopId);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Unit",
                columns: table => new
                {
                    Unit_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnitName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Remark = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Unit", x => x.Unit_Id);
                });

            migrationBuilder.CreateTable(
                name: "View_Base_MaterialDetail",
                columns: table => new
                {
                    MaterialDetail_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentProduct_Id = table.Column<int>(type: "integer", nullable: false),
                    PProductCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PProductName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PProductStandard = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    PUnit_Id = table.Column<int>(type: "integer", nullable: true),
                    ChildProduct_Id = table.Column<int>(type: "integer", nullable: false),
                    CProductCode = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CProductName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CProductStandard = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CUnit_Id = table.Column<int>(type: "integer", nullable: true),
                    QuantityPer = table.Column<int>(type: "integer", nullable: false),
                    Remark = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_View_Base_MaterialDetail", x => x.MaterialDetail_Id);
                });

            migrationBuilder.CreateTable(
                name: "Base_ProcessList",
                columns: table => new
                {
                    ProcessList_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Process_Id = table.Column<int>(type: "integer", nullable: false),
                    DataPointType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DataPointName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreateID = table.Column<int>(type: "integer", nullable: true),
                    Creator = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Modifier = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    ModifyDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifyID = table.Column<int>(type: "integer", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Base_ProcessLineList",
                columns: table => new
                {
                    ProcessLineList_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProcessLine_Id = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Base_ProcessLineList", x => x.ProcessLineList_Id);
                    table.ForeignKey(
                        name: "FK_Base_ProcessLineList_Base_ProcessLine_ProcessLine_Id",
                        column: x => x.ProcessLine_Id,
                        principalTable: "Base_ProcessLine",
                        principalColumn: "ProcessLine_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 17, 16, 47, 17, 616, DateTimeKind.Local).AddTicks(5900));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 17, 16, 47, 17, 616, DateTimeKind.Local).AddTicks(7120));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 17, 16, 47, 17, 616, DateTimeKind.Local).AddTicks(7120));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 17, 16, 47, 17, 616, DateTimeKind.Local).AddTicks(7120));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 8, 17, 16, 47, 17, 617, DateTimeKind.Local).AddTicks(1290));

            migrationBuilder.CreateIndex(
                name: "IX_UploadData_Name",
                table: "UploadData",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Base_ProcessLineList_ProcessLine_Id",
                table: "Base_ProcessLineList",
                column: "ProcessLine_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Base_ProcessList_Process_Id",
                table: "Base_ProcessList",
                column: "Process_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Base_DefectItem");

            migrationBuilder.DropTable(
                name: "Base_DesktopMenu");

            migrationBuilder.DropTable(
                name: "Base_MaterialDetail");

            migrationBuilder.DropTable(
                name: "Base_MeritPay");

            migrationBuilder.DropTable(
                name: "Base_Notice");

            migrationBuilder.DropTable(
                name: "Base_NumberRule");

            migrationBuilder.DropTable(
                name: "Base_ProcessLineList");

            migrationBuilder.DropTable(
                name: "Base_ProcessList");

            migrationBuilder.DropTable(
                name: "Base_Product");

            migrationBuilder.DropTable(
                name: "Base_WorkShop");

            migrationBuilder.DropTable(
                name: "Sys_Unit");

            migrationBuilder.DropTable(
                name: "View_Base_MaterialDetail");

            migrationBuilder.DropTable(
                name: "Base_ProcessLine");

            migrationBuilder.DropTable(
                name: "Base_Process");

            migrationBuilder.DropIndex(
                name: "IX_UploadData_Name",
                table: "UploadData");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(7980));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9910));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "Security",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 336, DateTimeKind.Local).AddTicks(9930));

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreatedTime",
                value: new DateTime(2023, 6, 19, 15, 57, 42, 337, DateTimeKind.Local).AddTicks(6590));
        }
    }
}
