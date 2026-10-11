using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Zamba.Api.Data;

#nullable disable

namespace Zamba.Api.Migrations;

[DbContext(typeof(ZambaDbContext))]
[Migration("20261011120000_AddEntityRagIndexing")]
public partial class AddEntityRagIndexing : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "RagDocumentTypeId",
            table: "ScheduleEvents",
            type: "decimal(18,0)",
            nullable: true);

        migrationBuilder.CreateTable(
            name: "EntityRagIndexingConfigs",
            columns: table => new
            {
                DocumentTypeId = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                IndexFromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                IndexMode = table.Column<int>(type: "int", nullable: false),
                CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_EntityRagIndexingConfigs", config => config.DocumentTypeId));

        migrationBuilder.CreateTable(
            name: "EntityRagDocumentStates",
            columns: table => new
            {
                DocumentTypeId = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                DocumentId = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                VectorDocumentId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                FileName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                UpdatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                IndexedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastError = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_EntityRagDocumentStates", state => new { state.DocumentTypeId, state.DocumentId }));

        migrationBuilder.CreateIndex(
            name: "IX_EntityRagIndexingConfigs_IsEnabled_IndexMode",
            table: "EntityRagIndexingConfigs",
            columns: new[] { "IsEnabled", "IndexMode" });

        migrationBuilder.CreateIndex(
            name: "IX_EntityRagDocumentStates_DocumentTypeId_Status_UpdatedAtUtc",
            table: "EntityRagDocumentStates",
            columns: new[] { "DocumentTypeId", "Status", "UpdatedAtUtc" });

        migrationBuilder.CreateIndex(
            name: "IX_ScheduleEvents_ExecutionType_RagDocumentTypeId",
            table: "ScheduleEvents",
            columns: new[] { "ExecutionType", "RagDocumentTypeId" },
            unique: true,
            filter: "[RagDocumentTypeId] IS NOT NULL");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(name: "IX_ScheduleEvents_ExecutionType_RagDocumentTypeId", table: "ScheduleEvents");
        migrationBuilder.DropTable(name: "EntityRagDocumentStates");
        migrationBuilder.DropTable(name: "EntityRagIndexingConfigs");
        migrationBuilder.DropColumn(name: "RagDocumentTypeId", table: "ScheduleEvents");
    }
}