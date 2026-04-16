using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AhmadHRMSBackend.Migrations
{
    /// <inheritdoc />
    public partial class migration13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ReportPeriods",
                columns: new[] { "ReportPeriodId", "IsDeleted", "ReportPeriodName" },
                values: new object[,]
                {
                    { 1, false, "Daily" },
                    { 2, false, "Weekly" },
                    { 3, false, "Monthly" },
                    { 4, false, "Quarterly" },
                    { 5, false, "Yearly" }
                });

            migrationBuilder.InsertData(
                table: "ReportStatus",
                columns: new[] { "ReportStatusId", "IsDeleted", "ReportStatusName" },
                values: new object[,]
                {
                    { 1, false, "Completed" },
                    { 2, false, "Scheduled" },
                    { 3, false, "Draft" }
                });

            migrationBuilder.InsertData(
                table: "ReportTypes",
                columns: new[] { "ReportTypeId", "IsDeleted", "ReportTypeName" },
                values: new object[,]
                {
                    { 1, false, "Attendance" },
                    { 2, false, "Payroll" },
                    { 3, false, "Performance" },
                    { 4, false, "Leave" },
                    { 5, false, "Timesheet" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReportPeriods",
                keyColumn: "ReportPeriodId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReportPeriods",
                keyColumn: "ReportPeriodId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReportPeriods",
                keyColumn: "ReportPeriodId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReportPeriods",
                keyColumn: "ReportPeriodId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ReportPeriods",
                keyColumn: "ReportPeriodId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ReportStatus",
                keyColumn: "ReportStatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReportStatus",
                keyColumn: "ReportStatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReportStatus",
                keyColumn: "ReportStatusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ReportTypes",
                keyColumn: "ReportTypeId",
                keyValue: 5);
        }
    }
}
