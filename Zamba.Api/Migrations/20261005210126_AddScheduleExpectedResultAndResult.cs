using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamba.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleExpectedResultAndResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var isOracle = ActiveProvider.Contains("Oracle", StringComparison.Ordinal);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "ScheduleExecutionLogs",
                type: isOracle ? "NVARCHAR2(20)" : null,
                maxLength: 20,
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "ExpectedResult",
                table: "ScheduleEvents",
                type: isOracle ? "NCLOB" : null,
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Result", table: "ScheduleExecutionLogs");

            migrationBuilder.DropColumn(name: "ExpectedResult", table: "ScheduleEvents");
        }
    }
}
