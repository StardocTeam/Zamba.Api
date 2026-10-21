using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Zamba.Api.Data;

#nullable disable

namespace Zamba.Api.Migrations
{
    [DbContext(typeof(ZambaDbContext))]
    [Migration("20261007090000_AddScheduleEventSlackNotifications")]
    public partial class AddScheduleEventSlackNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var isOracle = ActiveProvider.Contains("Oracle", StringComparison.Ordinal);

            if (isOracle)
            {
                migrationBuilder.Sql(
                    @"
DECLARE
    v_count INTEGER;
BEGIN
    SELECT COUNT(*) INTO v_count FROM USER_TAB_COLUMNS WHERE UPPER(TABLE_NAME) = 'SCHEDULEEVENTS' AND UPPER(COLUMN_NAME) = 'NOTIFYSLACKONFAILURE';
    IF v_count = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE ""ScheduleEvents"" ADD ""NotifySlackOnFailure"" NUMBER(1) DEFAULT 0 NOT NULL';
    END IF;

    SELECT COUNT(*) INTO v_count FROM USER_TAB_COLUMNS WHERE UPPER(TABLE_NAME) = 'SCHEDULEEVENTS' AND UPPER(COLUMN_NAME) = 'NOTIFYSLACKONSUCCESS';
    IF v_count = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE ""ScheduleEvents"" ADD ""NotifySlackOnSuccess"" NUMBER(1) DEFAULT 0 NOT NULL';
    END IF;
END;"
                );
                return;
            }

            migrationBuilder.AddColumn<bool>(
                name: "NotifySlackOnFailure",
                table: "ScheduleEvents",
                nullable: false,
                defaultValue: false
            );

            migrationBuilder.AddColumn<bool>(
                name: "NotifySlackOnSuccess",
                table: "ScheduleEvents",
                nullable: false,
                defaultValue: false
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "NotifySlackOnFailure", table: "ScheduleEvents");
            migrationBuilder.DropColumn(name: "NotifySlackOnSuccess", table: "ScheduleEvents");
        }
    }
}
