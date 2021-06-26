using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsuranceApp.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarInsurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TCKN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicencePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenceCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenceSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarInsurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfferDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LicencePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EffectedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyOffers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CarInsurances",
                columns: new[] { "Id", "CreatedDate", "EffectedDate", "LicenceCode", "LicencePlate", "LicenceSerialNumber", "TCKN" },
                values: new object[] { 1, new DateTime(2021, 6, 25, 14, 54, 11, 341, DateTimeKind.Local).AddTicks(7096), null, "AA", "06NNE67", "111111", "11111111111" });

            migrationBuilder.InsertData(
                table: "CarInsurances",
                columns: new[] { "Id", "CreatedDate", "EffectedDate", "LicenceCode", "LicencePlate", "LicenceSerialNumber", "TCKN" },
                values: new object[] { 2, new DateTime(2021, 6, 25, 14, 54, 11, 343, DateTimeKind.Local).AddTicks(1130), null, "AB", "06BHY724", "111112", "11111111112" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarInsurances");

            migrationBuilder.DropTable(
                name: "CompanyOffers");
        }
    }
}
