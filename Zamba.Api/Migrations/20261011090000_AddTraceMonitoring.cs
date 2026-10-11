using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Zamba.Api.Data;

#nullable disable

namespace Zamba.Api.Migrations;

[DbContext(typeof(ZambaDbContext))]
[Migration("20261011090000_AddTraceMonitoring")]
public partial class AddTraceMonitoring : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TraceSources",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ApplicationName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Environment = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                FolderPath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_TraceSources", source => source.Id));

        migrationBuilder.CreateTable(
            name: "TraceRecords",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TraceSourceId = table.Column<int>(type: "int", nullable: false),
                RelativePath = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                FileName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                TraceTimestampUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                UserId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                TraceConfig = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Title = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                RegisteredAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TraceRecords", trace => trace.Id);
                table.ForeignKey(
                    name: "FK_TraceRecords_TraceSources_TraceSourceId",
                    column: trace => trace.TraceSourceId,
                    principalTable: "TraceSources",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_TraceSources_ApplicationName_Environment_FolderPath",
            table: "TraceSources",
            columns: new[] { "ApplicationName", "Environment", "FolderPath" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_TraceRecords_TraceSourceId_RelativePath",
            table: "TraceRecords",
            columns: new[] { "TraceSourceId", "RelativePath" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_TraceRecords_TraceSourceId_TraceTimestampUtc",
            table: "TraceRecords",
            columns: new[] { "TraceSourceId", "TraceTimestampUtc" });

        migrationBuilder.CreateIndex(
            name: "IX_TraceRecords_UserId",
            table: "TraceRecords",
            column: "UserId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "TraceRecords");
        migrationBuilder.DropTable(name: "TraceSources");
    }
}