using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSEData.Web.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
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
                    LTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Open = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    High = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Low = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Volume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Companys",
                columns: new[] { "Id", "StockCodeName" },
                values: new object[] { 1, "1JANATAMF" });

            migrationBuilder.InsertData(
                table: "Prices",
                columns: new[] { "Id", "CompanyID", "High", "LTP", "Low", "Open", "Time", "Volume" },
                values: new object[] { 1, 1, "2.5", "6.0", "2.5", "6.3", new DateTime(2023, 8, 2, 0, 0, 0, 0, DateTimeKind.Local), "6534" });
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
