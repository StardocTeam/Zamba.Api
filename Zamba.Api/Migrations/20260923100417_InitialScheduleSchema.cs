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
            var isOracle = ActiveProvider.Contains("Oracle", StringComparison.Ordinal);
            var utcNowSql = ActiveProvider.Contains("Oracle", StringComparison.Ordinal)
                ? "SYS_EXTRACT_UTC(SYSTIMESTAMP)"
                : "GETUTCDATE()";

            if (isOracle)
            {
                migrationBuilder.Sql(
                    @"
BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE ""ScheduleExecutionLogs"" CASCADE CONSTRAINTS PURGE';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;"
                );

                migrationBuilder.Sql(
                    @"
BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE ""ScheduleConfigs"" CASCADE CONSTRAINTS PURGE';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;"
                );

                migrationBuilder.Sql(
                    @"
BEGIN
    EXECUTE IMMEDIATE 'DROP TABLE ""ScheduleEvents"" CASCADE CONSTRAINTS PURGE';
EXCEPTION
    WHEN OTHERS THEN
        IF SQLCODE != -942 THEN
            RAISE;
        END IF;
END;"
                );
            }

            migrationBuilder.CreateTable(
                name: "ScheduleEvents",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: isOracle ? "NUMBER(10)" : null, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(255)" : null,
                        maxLength: 255,
                        nullable: false
                    ),
                    Description = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(1000)" : null,
                        maxLength: 1000,
                        nullable: true
                    ),
                    RuleId = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(255)" : null,
                        maxLength: 255,
                        nullable: false
                    ),
                    EndpointOverride = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(500)" : null,
                        maxLength: 500,
                        nullable: true
                    ),
                    IsActive = table.Column<bool>(
                        type: isOracle ? "NUMBER(1)" : null,
                        nullable: false
                    ),
                    CreatedAt = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: false,
                        defaultValueSql: utcNowSql
                    ),
                    UpdatedAt = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: false,
                        defaultValueSql: utcNowSql
                    ),
                    CreatedBy = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(255)" : null,
                        maxLength: 255,
                        nullable: true
                    ),
                    UpdatedBy = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(255)" : null,
                        maxLength: 255,
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleEvents", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "ScheduleConfigs",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: isOracle ? "NUMBER(10)" : null, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleEventId = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    RecurrenceType = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    IntervalValue = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: true
                    ),
                    DaysOfWeek = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(50)" : null,
                        maxLength: 50,
                        nullable: true
                    ),
                    DayOfMonth = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: true
                    ),
                    TimeOfDay = table.Column<TimeSpan>(
                        type: isOracle ? "INTERVAL DAY(8) TO SECOND(7)" : null,
                        nullable: true
                    ),
                    StartDate = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: true
                    ),
                    EndDate = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: true
                    ),
                    CronExpression = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(255)" : null,
                        maxLength: 255,
                        nullable: true
                    ),
                    MisfireInstruction = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    TimeZone = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(100)" : null,
                        maxLength: 100,
                        nullable: false,
                        defaultValue: "Argentina Standard Time"
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleConfigs_ScheduleEvents_ScheduleEventId",
                        column: x => x.ScheduleEventId,
                        principalTable: "ScheduleEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "ScheduleExecutionLogs",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: isOracle ? "NUMBER(10)" : null, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleEventId = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    ScheduledAt = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(6)" : null,
                        precision: 6,
                        nullable: false
                    ),
                    StartedAt = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(6)" : null,
                        precision: 6,
                        nullable: false
                    ),
                    FinishedAt = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(6)" : null,
                        precision: 6,
                        nullable: true
                    ),
                    Status = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    HttpStatusCode = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: true
                    ),
                    ResponseBody = table.Column<string>(
                        type: isOracle ? "NCLOB" : null,
                        maxLength: 4000,
                        nullable: true
                    ),
                    ErrorMessage = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(2000)" : null,
                        maxLength: 2000,
                        nullable: true
                    ),
                    DurationMs = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    RetryCount = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleExecutionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleExecutionLogs_ScheduleEvents_ScheduleEventId",
                        column: x => x.ScheduleEventId,
                        principalTable: "ScheduleEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleConfigs_ScheduleEventId",
                table: "ScheduleConfigs",
                column: "ScheduleEventId",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_CreatedAt",
                table: "ScheduleEvents",
                column: "CreatedAt"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_IsActive",
                table: "ScheduleEvents",
                column: "IsActive"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_RuleId",
                table: "ScheduleEvents",
                column: "RuleId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExecutionLogs_ScheduledAt",
                table: "ScheduleExecutionLogs",
                column: "ScheduledAt"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExecutionLogs_ScheduleEventId",
                table: "ScheduleExecutionLogs",
                column: "ScheduleEventId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExecutionLogs_ScheduleEventId_ScheduledAt",
                table: "ScheduleExecutionLogs",
                columns: new[] { "ScheduleEventId", "ScheduledAt" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleExecutionLogs_Status",
                table: "ScheduleExecutionLogs",
                column: "Status"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ScheduleConfigs");

            migrationBuilder.DropTable(name: "ScheduleExecutionLogs");

            migrationBuilder.DropTable(name: "ScheduleEvents");
        }
    }
}
