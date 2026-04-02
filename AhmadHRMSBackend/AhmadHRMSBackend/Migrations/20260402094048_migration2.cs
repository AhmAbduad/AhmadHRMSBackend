using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmadHRMSBackend.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeList_Departments_DepartmentID",
                table: "EmployeeList");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeList_Position_PositionID",
                table: "EmployeeList");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeList_Status_StatusID",
                table: "EmployeeList");

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Status",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Position",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "EmployeeList",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AttendanceInfo",
                columns: table => new
                {
                    AttendanceInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInTime = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CheckOutTime = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TotalHours = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceInfo", x => x.AttendanceInfoId);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckOut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceRecords_EmployeeList_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeList",
                        principalColumn: "EmployeeID");
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSummary",
                columns: table => new
                {
                    AttendanceSummaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Present = table.Column<int>(type: "int", nullable: false),
                    Absent = table.Column<int>(type: "int", nullable: false),
                    Late = table.Column<int>(type: "int", nullable: false),
                    Leave = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSummary", x => x.AttendanceSummaryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecords_EmployeeId",
                table: "AttendanceRecords",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeList_Departments_DepartmentID",
                table: "EmployeeList",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentsID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeList_Position_PositionID",
                table: "EmployeeList",
                column: "PositionID",
                principalTable: "Position",
                principalColumn: "PositionID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeList_Status_StatusID",
                table: "EmployeeList",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeList_Departments_DepartmentID",
                table: "EmployeeList");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeList_Position_PositionID",
                table: "EmployeeList");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeList_Status_StatusID",
                table: "EmployeeList");

            migrationBuilder.DropTable(
                name: "AttendanceInfo");

            migrationBuilder.DropTable(
                name: "AttendanceRecords");

            migrationBuilder.DropTable(
                name: "AttendanceSummary");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "EmployeeList");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Departments");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeList_Departments_DepartmentID",
                table: "EmployeeList",
                column: "DepartmentID",
                principalTable: "Departments",
                principalColumn: "DepartmentsID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeList_Position_PositionID",
                table: "EmployeeList",
                column: "PositionID",
                principalTable: "Position",
                principalColumn: "PositionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeList_Status_StatusID",
                table: "EmployeeList",
                column: "StatusID",
                principalTable: "Status",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
