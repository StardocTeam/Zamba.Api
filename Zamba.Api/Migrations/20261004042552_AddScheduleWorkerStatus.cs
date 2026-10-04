using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamba.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddScheduleWorkerStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScheduleWorkerStatuses",
                columns: table => new
                {
                    WorkerInstanceId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    HostName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastHeartbeatAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSyncAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegisteredJobCount = table.Column<int>(type: "int", nullable: false),
                    ActiveJobCount = table.Column<int>(type: "int", nullable: false),
                    LastError = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleWorkerStatuses", x => x.WorkerInstanceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWorkerStatuses_LastHeartbeatAtUtc",
                table: "ScheduleWorkerStatuses",
                column: "LastHeartbeatAtUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScheduleWorkerStatuses");
        }
    }
}
