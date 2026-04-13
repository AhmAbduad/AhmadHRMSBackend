using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AhmadHRMSBackend.Migrations
{
    /// <inheritdoc />
    public partial class migration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PerformancePeriod",
                columns: table => new
                {
                    PerformancePeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformancePeriod", x => x.PerformancePeriodId);
                });

            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    PerformanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextReview = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.PerformanceID);
                    table.ForeignKey(
                        name: "FK_Performance_EmployeeList_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeList",
                        principalColumn: "EmployeeID");
                    table.ForeignKey(
                        name: "FK_Performance_PerformancePeriod_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "PerformancePeriod",
                        principalColumn: "PerformancePeriodId");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceAchievement",
                columns: table => new
                {
                    AchievementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceId = table.Column<int>(type: "int", nullable: false),
                    AchievementText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceAchievement", x => x.AchievementId);
                    table.ForeignKey(
                        name: "FK_PerformanceAchievement_Performance_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performance",
                        principalColumn: "PerformanceID");
                });

            migrationBuilder.CreateTable(
                name: "PerformanceGoal",
                columns: table => new
                {
                    PerformanceGoalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PerformanceId = table.Column<int>(type: "int", nullable: false),
                    GoalText = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceGoal", x => x.PerformanceGoalId);
                    table.ForeignKey(
                        name: "FK_PerformanceGoal_Performance_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performance",
                        principalColumn: "PerformanceID");
                });

            migrationBuilder.InsertData(
                table: "PerformancePeriod",
                columns: new[] { "PerformancePeriodId", "IsDeleted", "PeriodName" },
                values: new object[,]
                {
                    { 1, false, "Q1 2026" },
                    { 2, false, "Q2 2026" },
                    { 3, false, "Q3 2026" },
                    { 4, false, "Q4 2026" },
                    { 5, false, "Q1 2027" },
                    { 6, false, "Q2 2027" },
                    { 7, false, "Q3 2027" },
                    { 8, false, "Q4 2027" },
                    { 9, false, "Q1 2028" },
                    { 10, false, "Q2 2028" },
                    { 11, false, "Q3 2028" },
                    { 12, false, "Q4 2028" },
                    { 13, false, "Q1 2029" },
                    { 14, false, "Q2 2029" },
                    { 15, false, "Q3 2029" },
                    { 16, false, "Q4 2029" },
                    { 17, false, "Q1 2030" },
                    { 18, false, "Q2 2030" },
                    { 19, false, "Q3 2030" },
                    { 20, false, "Q4 2030" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Performance_EmployeeId",
                table: "Performance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Performance_PeriodId",
                table: "Performance",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceAchievement_PerformanceId",
                table: "PerformanceAchievement",
                column: "PerformanceId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceGoal_PerformanceId",
                table: "PerformanceGoal",
                column: "PerformanceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PerformanceAchievement");

            migrationBuilder.DropTable(
                name: "PerformanceGoal");

            migrationBuilder.DropTable(
                name: "Performance");

            migrationBuilder.DropTable(
                name: "PerformancePeriod");
        }
    }
}
