using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Zamba.Api.Data;

#nullable disable

namespace Zamba.Api.Migrations
{
    [DbContext(typeof(ZambaDbContext))]
    [Migration("20261006090000_AddScheduleEventEmailNotifications")]
    public partial class AddScheduleEventEmailNotifications : Migration
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
    SELECT COUNT(*) INTO v_count FROM USER_TAB_COLUMNS WHERE UPPER(TABLE_NAME) = 'SCHEDULEEVENTS' AND UPPER(COLUMN_NAME) = 'NOTIFICATIONEMAIL';
    IF v_count = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE ""ScheduleEvents"" ADD ""NotificationEmail"" NVARCHAR2(320)';
    END IF;

    SELECT COUNT(*) INTO v_count FROM USER_TAB_COLUMNS WHERE UPPER(TABLE_NAME) = 'SCHEDULEEVENTS' AND UPPER(COLUMN_NAME) = 'NOTIFYONFAILURE';
    IF v_count = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE ""ScheduleEvents"" ADD ""NotifyOnFailure"" NUMBER(1) DEFAULT 0 NOT NULL';
    END IF;

    SELECT COUNT(*) INTO v_count FROM USER_TAB_COLUMNS WHERE UPPER(TABLE_NAME) = 'SCHEDULEEVENTS' AND UPPER(COLUMN_NAME) = 'NOTIFYONSUCCESS';
    IF v_count = 0 THEN
        EXECUTE IMMEDIATE 'ALTER TABLE ""ScheduleEvents"" ADD ""NotifyOnSuccess"" NUMBER(1) DEFAULT 0 NOT NULL';
    END IF;
END;"
                );
                return;
            }

            migrationBuilder.AddColumn<string>(
                name: "NotificationEmail",
                table: "ScheduleEvents",
                maxLength: 320,
                nullable: true
            );

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOnFailure",
                table: "ScheduleEvents",
                nullable: false,
                defaultValue: false
            );

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOnSuccess",
                table: "ScheduleEvents",
                nullable: false,
                defaultValue: false
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "NotificationEmail", table: "ScheduleEvents");
            migrationBuilder.DropColumn(name: "NotifyOnFailure", table: "ScheduleEvents");
            migrationBuilder.DropColumn(name: "NotifyOnSuccess", table: "ScheduleEvents");
        }
    }
}
