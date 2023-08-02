using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSEData.Web.Migrations
{
    /// <inheritdoc />
    public partial class ceateCompanyandPriceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockCodeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyID = table.Column<int>(type: "int", nullable: true),
                    LTP = table.Column<double>(type: "float", nullable: false),
                    Open = table.Column<double>(type: "float", nullable: false),
                    High = table.Column<double>(type: "float", nullable: false),
                    Low = table.Column<double>(type: "float", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "StockCodeName" },
                values: new object[] { 1, "test" });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "CompanyID", "High", "LTP", "Low", "Open", "Time" },
                values: new object[] { 1, null, 2.5, 2.7000000000000002, 2.2999999999999998, 5.2999999999999998, new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companys");

            migrationBuilder.DropTable(
                name: "Prices");
        }
    }
}
