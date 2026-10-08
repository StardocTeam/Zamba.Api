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
            migrationBuilder.AddColumn<string>(
                name: "NotificationEmail",
                table: "ScheduleEvents",
                type: "nvarchar(320)",
                maxLength: 320,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOnFailure",
                table: "ScheduleEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifyOnSuccess",
                table: "ScheduleEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "NotificationEmail", table: "ScheduleEvents");
            migrationBuilder.DropColumn(name: "NotifyOnFailure", table: "ScheduleEvents");
            migrationBuilder.DropColumn(name: "NotifyOnSuccess", table: "ScheduleEvents");
        }
    }
}