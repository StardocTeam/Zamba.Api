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
            var isOracle = ActiveProvider.Contains("Oracle", StringComparison.Ordinal);

            migrationBuilder.CreateTable(
                name: "ScheduleWorkerStatuses",
                columns: table => new
                {
                    WorkerInstanceId = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(128)" : null,
                        maxLength: 128,
                        nullable: false
                    ),
                    HostName = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(255)" : null,
                        maxLength: 255,
                        nullable: false
                    ),
                    StartedAtUtc = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: false
                    ),
                    LastHeartbeatAtUtc = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: false
                    ),
                    LastSyncAtUtc = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: true
                    ),
                    RegisteredJobCount = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    ActiveJobCount = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    LastError = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(2000)" : null,
                        maxLength: 2000,
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleWorkerStatuses", x => x.WorkerInstanceId);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWorkerStatuses_LastHeartbeatAtUtc",
                table: "ScheduleWorkerStatuses",
                column: "LastHeartbeatAtUtc"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ScheduleWorkerStatuses");
        }
    }
}
