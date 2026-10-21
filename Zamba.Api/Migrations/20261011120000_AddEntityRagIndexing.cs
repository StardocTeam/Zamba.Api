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
        var isOracle = ActiveProvider.Contains("Oracle", StringComparison.Ordinal);

        migrationBuilder.AddColumn<decimal>(
            name: "RagDocumentTypeId",
            table: "ScheduleEvents",
            nullable: true
        );

        migrationBuilder.CreateTable(
            name: "EntityRagIndexingConfigs",
            columns: table => new
            {
                DocumentTypeId = table.Column<decimal>(nullable: false),
                IsEnabled = table.Column<bool>(
                    type: isOracle ? "NUMBER(1)" : null,
                    nullable: false
                ),
                IndexFromDate = table.Column<DateTime>(nullable: true),
                IndexMode = table.Column<int>(nullable: false),
                CreatedAtUtc = table.Column<DateTime>(nullable: false),
                UpdatedAtUtc = table.Column<DateTime>(nullable: false),
            },
            constraints: table =>
                table.PrimaryKey("PK_EntityRagIndexingConfigs", config => config.DocumentTypeId)
        );

        migrationBuilder.CreateTable(
            name: "EntityRagDocumentStates",
            columns: table => new
            {
                DocumentTypeId = table.Column<decimal>(nullable: false),
                DocumentId = table.Column<decimal>(nullable: false),
                Status = table.Column<int>(nullable: false),
                VectorDocumentId = table.Column<string>(maxLength: 64, nullable: true),
                FileName = table.Column<string>(maxLength: 512, nullable: true),
                UpdatedAtUtc = table.Column<DateTime>(nullable: false),
                IndexedAtUtc = table.Column<DateTime>(nullable: true),
                LastError = table.Column<string>(
                    type: isOracle ? "NCLOB" : null,
                    maxLength: 4000,
                    nullable: true
                ),
            },
            constraints: table =>
                table.PrimaryKey(
                    "PK_EntityRagDocumentStates",
                    state => new { state.DocumentTypeId, state.DocumentId }
                )
        );

        migrationBuilder.CreateIndex(
            name: "IX_EntityRagIndexingConfigs_IsEnabled_IndexMode",
            table: "EntityRagIndexingConfigs",
            columns: new[] { "IsEnabled", "IndexMode" }
        );

        migrationBuilder.CreateIndex(
            name: "IX_EntityRagDocumentStates_DocumentTypeId_Status_UpdatedAtUtc",
            table: "EntityRagDocumentStates",
            columns: new[] { "DocumentTypeId", "Status", "UpdatedAtUtc" }
        );

        if (ActiveProvider.Contains("SqlServer", StringComparison.Ordinal))
        {
            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_ExecutionType_RagDocumentTypeId",
                table: "ScheduleEvents",
                columns: new[] { "ExecutionType", "RagDocumentTypeId" },
                unique: true,
                filter: "[RagDocumentTypeId] IS NOT NULL"
            );
        }
        else
        {
            migrationBuilder.CreateIndex(
                name: "IX_ScheduleEvents_ExecutionType_RagDocumentTypeId",
                table: "ScheduleEvents",
                columns: new[] { "ExecutionType", "RagDocumentTypeId" },
                unique: true
            );
        }
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_ScheduleEvents_ExecutionType_RagDocumentTypeId",
            table: "ScheduleEvents"
        );
        migrationBuilder.DropTable(name: "EntityRagDocumentStates");
        migrationBuilder.DropTable(name: "EntityRagIndexingConfigs");
        migrationBuilder.DropColumn(name: "RagDocumentTypeId", table: "ScheduleEvents");
    }
}
