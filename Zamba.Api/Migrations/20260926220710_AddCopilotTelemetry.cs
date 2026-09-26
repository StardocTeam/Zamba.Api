using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zamba.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCopilotTelemetry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CopilotRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Prompt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DocumentId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DocumentTypeCode = table.Column<int>(type: "int", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    EmbeddingProvider = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    ChatModel = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DocumentAlreadyIndexed = table.Column<bool>(type: "bit", nullable: true),
                    ChunksIndexed = table.Column<int>(type: "int", nullable: false),
                    ChunksUsed = table.Column<int>(type: "int", nullable: false),
                    EmbeddingDurationMs = table.Column<long>(type: "bigint", nullable: true),
                    RetrievalDurationMs = table.Column<long>(type: "bigint", nullable: true),
                    ChatDurationMs = table.Column<long>(type: "bigint", nullable: true),
                    TotalDurationMs = table.Column<long>(type: "bigint", nullable: true),
                    InputTokens = table.Column<long>(type: "bigint", nullable: true),
                    OutputTokens = table.Column<long>(type: "bigint", nullable: true),
                    CacheReadTokens = table.Column<long>(type: "bigint", nullable: true),
                    CacheWriteTokens = table.Column<long>(type: "bigint", nullable: true),
                    EstimatedCost = table.Column<double>(type: "float", nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopilotRuns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CopilotFeedback",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RunId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoterId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopilotFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CopilotFeedback_CopilotRuns_RunId",
                        column: x => x.RunId,
                        principalTable: "CopilotRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CopilotRunSteps",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RunId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    StartedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DurationMs = table.Column<long>(type: "bigint", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    ErrorMessage = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopilotRunSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CopilotRunSteps_CopilotRuns_RunId",
                        column: x => x.RunId,
                        principalTable: "CopilotRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CopilotFeedback_Rating_CreatedAtUtc",
                table: "CopilotFeedback",
                columns: new[] { "Rating", "CreatedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_CopilotFeedback_RunId_VoterId",
                table: "CopilotFeedback",
                columns: new[] { "RunId", "VoterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CopilotRuns_DocumentTypeCode_StartedAtUtc",
                table: "CopilotRuns",
                columns: new[] { "DocumentTypeCode", "StartedAtUtc" });

            migrationBuilder.CreateIndex(
                name: "IX_CopilotRuns_StartedAtUtc",
                table: "CopilotRuns",
                column: "StartedAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_CopilotRuns_Status",
                table: "CopilotRuns",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CopilotRunSteps_RunId_StartedAtUtc",
                table: "CopilotRunSteps",
                columns: new[] { "RunId", "StartedAtUtc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CopilotFeedback");

            migrationBuilder.DropTable(
                name: "CopilotRunSteps");

            migrationBuilder.DropTable(
                name: "CopilotRuns");
        }
    }
}
