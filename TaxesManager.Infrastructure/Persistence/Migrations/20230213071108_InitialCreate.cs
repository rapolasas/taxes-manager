using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaxesManager.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Taxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "Decimal(3,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "Date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "Date", nullable: false),
                    Schedule = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MunicipalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxes_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("2b24d1aa-0761-4843-99b9-e3a93bc2561e"), "Copenhagen" });

            migrationBuilder.InsertData(
                table: "Taxes",
                columns: new[] { "Id", "Amount", "EndDate", "MunicipalityId", "Schedule", "StartDate" },
                values: new object[,]
                {
                    { new Guid("216209cc-eed7-4225-a3c1-c1af62ee0606"), 0.1m, new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b24d1aa-0761-4843-99b9-e3a93bc2561e"), "Daily", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5cbbf5e3-d533-4958-94a0-ddcc03142b4e"), 0.1m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b24d1aa-0761-4843-99b9-e3a93bc2561e"), "Daily", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e473a265-911e-4cb7-8fdb-330cc6e8277a"), 0.4m, new DateTime(2023, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b24d1aa-0761-4843-99b9-e3a93bc2561e"), "Monthly", new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f3003643-1945-4787-8779-83913f213f64"), 0.2m, new DateTime(2023, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("2b24d1aa-0761-4843-99b9-e3a93bc2561e"), "Yearly", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Taxes_MunicipalityId",
                table: "Taxes",
                column: "MunicipalityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taxes");

            migrationBuilder.DropTable(
                name: "Municipalities");
        }
    }
}
