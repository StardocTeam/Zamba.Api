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
            migrationBuilder.AddColumn<bool>(
                name: "NotifySlackOnFailure",
                table: "ScheduleEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NotifySlackOnSuccess",
                table: "ScheduleEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "NotifySlackOnFailure", table: "ScheduleEvents");
            migrationBuilder.DropColumn(name: "NotifySlackOnSuccess", table: "ScheduleEvents");
        }
    }
}