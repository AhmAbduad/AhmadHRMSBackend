using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmadHRMSBackend.Migrations
{
    /// <inheritdoc />
    public partial class migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "AttendanceRecords");

            migrationBuilder.DropColumn(
                name: "TotalHours",
                table: "AttendanceInfo");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "AttendanceSummary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "AttendanceSummary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "AttendanceSummary",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOut",
                table: "AttendanceRecords",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckIn",
                table: "AttendanceRecords",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckOutTime",
                table: "AttendanceInfo",
                type: "datetime2",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CheckInTime",
                table: "AttendanceInfo",
                type: "datetime2",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSummary_EmployeeId_Month_Year",
                table: "AttendanceSummary",
                columns: new[] { "EmployeeId", "Month", "Year" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceSummary_EmployeeList_EmployeeId",
                table: "AttendanceSummary",
                column: "EmployeeId",
                principalTable: "EmployeeList",
                principalColumn: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceSummary_EmployeeList_EmployeeId",
                table: "AttendanceSummary");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceSummary_EmployeeId_Month_Year",
                table: "AttendanceSummary");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "AttendanceSummary");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "AttendanceSummary");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "AttendanceSummary");

            migrationBuilder.AlterColumn<string>(
                name: "CheckOut",
                table: "AttendanceRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "CheckIn",
                table: "AttendanceRecords",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "TotalHours",
                table: "AttendanceRecords",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "CheckOutTime",
                table: "AttendanceInfo",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "CheckInTime",
                table: "AttendanceInfo",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "TotalHours",
                table: "AttendanceInfo",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
