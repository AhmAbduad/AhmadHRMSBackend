using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmadHRMSBackend.Migrations
{
    /// <inheritdoc />
    public partial class migration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayrollStatus",
                columns: table => new
                {
                    PayrollStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollStatus", x => x.PayrollStatusId);
                });

            migrationBuilder.CreateTable(
                name: "PayrollRequests",
                columns: table => new
                {
                    PayrollRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollRequests", x => x.PayrollRequestId);
                    table.ForeignKey(
                        name: "FK_PayrollRequests_EmployeeList_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeList",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_PayrollRequests_PayrollStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "PayrollStatus",
                        principalColumn: "PayrollStatusId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayrollRequests_EmployeeId",
                table: "PayrollRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollRequests_StatusId",
                table: "PayrollRequests",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayrollRequests");

            migrationBuilder.DropTable(
                name: "PayrollStatus");
        }
    }
}
