using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamba.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialScheduleSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    RuleId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EndpointOverride = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleConfigs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleEventId = table.Column<int>(type: "int", nullable: false),
                    RecurrenceType = table.Column<int>(type: "int", nullable: false),
                    IntervalValue = table.Column<int>(type: "int", nullable: true),
                    DaysOfWeek = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DayOfMonth = table.Column<int>(type: "int", nullable: true),
                    TimeOfDay = table.Column<TimeSpan>(type: "time", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CronExpression = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MisfireInstruction = table.Column<int>(type: "int", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "Argentina Standard Time")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleConfigs_ScheduleEvents_ScheduleEventId",
                        column: x => x.ScheduleEventId,
                        principalTable: "ScheduleEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleExecutionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleEventId = table.Column<int>(type: "int", nullable: false),
                    ScheduledAt = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    HttpStatusCode = table.Column<int>(type: "int", nullable: true),
                    ResponseBody = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DurationMs = table.Column<long>(type: "bigint", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleExecutionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleExecutionLogs_ScheduleEvents_ScheduleEventId",
                        column: x => x.ScheduleEventId,
                        principalTable: "ScheduleEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleConfigs_ScheduleEventId",
                table: "ScheduleConfigs",
                column: "ScheduleEventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_CreatedAt",
                table: "ScheduleEvents",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_IsActive",
                table: "ScheduleEvents",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_RuleId",
                table: "ScheduleEvents",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExecutionLogs_ScheduledAt",
                table: "ScheduleExecutionLogs",
                column: "ScheduledAt");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExecutionLogs_ScheduleEventId",
                table: "ScheduleExecutionLogs",
                column: "ScheduleEventId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExecutionLogs_ScheduleEventId_ScheduledAt",
                table: "ScheduleExecutionLogs",
                columns: new[] { "ScheduleEventId", "ScheduledAt" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExecutionLogs_Status",
                table: "ScheduleExecutionLogs",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleConfigs");

            migrationBuilder.DropTable(
                name: "ScheduleExecutionLogs");

            migrationBuilder.DropTable(
                name: "ScheduleEvents");
        }
    }
}
