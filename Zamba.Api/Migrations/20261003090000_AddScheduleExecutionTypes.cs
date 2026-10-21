using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Zamba.Api.Data;

#nullable disable

namespace Zamba.Api.Migrations;

[DbContext(typeof(ZambaDbContext))]
[Migration("20261003090000_AddScheduleExecutionTypes")]
public partial class AddScheduleExecutionTypes : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "ExecutionType",
            table: "ScheduleEvents",
            nullable: false,
            defaultValue: 0
        );

        migrationBuilder.AddColumn<string>(name: "Query", table: "ScheduleEvents", nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "EndpointUrl",
            table: "ScheduleEvents",
            maxLength: 2048,
            nullable: true
        );

        migrationBuilder.AddColumn<string>(
            name: "EndpointMethod",
            table: "ScheduleEvents",
            maxLength: 10,
            nullable: true
        );

        migrationBuilder.AddColumn<string>(
            name: "EndpointParameters",
            table: "ScheduleEvents",
            nullable: true
        );

        migrationBuilder.AddColumn<string>(
            name: "EndpointBody",
            table: "ScheduleEvents",
            nullable: true
        );
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "ExecutionType", table: "ScheduleEvents");
        migrationBuilder.DropColumn(name: "Query", table: "ScheduleEvents");
        migrationBuilder.DropColumn(name: "EndpointUrl", table: "ScheduleEvents");
        migrationBuilder.DropColumn(name: "EndpointMethod", table: "ScheduleEvents");
        migrationBuilder.DropColumn(name: "EndpointParameters", table: "ScheduleEvents");
        migrationBuilder.DropColumn(name: "EndpointBody", table: "ScheduleEvents");
    }
}
