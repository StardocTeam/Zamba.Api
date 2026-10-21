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
            var isOracle = ActiveProvider.Contains("Oracle", StringComparison.Ordinal);

            migrationBuilder.CreateTable(
                name: "CopilotRuns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: isOracle ? "RAW(16)" : null, nullable: false),
                    StartedAtUtc = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: false
                    ),
                    CompletedAtUtc = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: true
                    ),
                    Status = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(32)" : null,
                        maxLength: 32,
                        nullable: false
                    ),
                    Prompt = table.Column<string>(type: isOracle ? "NCLOB" : null, nullable: false),
                    Response = table.Column<string>(
                        type: isOracle ? "NCLOB" : null,
                        nullable: true
                    ),
                    FileName = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(512)" : null,
                        maxLength: 512,
                        nullable: true
                    ),
                    DocumentId = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(64)" : null,
                        maxLength: 64,
                        nullable: true
                    ),
                    DocumentTypeCode = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: true
                    ),
                    SessionId = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(128)" : null,
                        maxLength: 128,
                        nullable: true
                    ),
                    EmbeddingProvider = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(64)" : null,
                        maxLength: 64,
                        nullable: true
                    ),
                    ChatModel = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(128)" : null,
                        maxLength: 128,
                        nullable: true
                    ),
                    DocumentAlreadyIndexed = table.Column<bool>(
                        type: isOracle ? "NUMBER(1)" : null,
                        nullable: true
                    ),
                    ChunksIndexed = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    ChunksUsed = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    EmbeddingDurationMs = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    RetrievalDurationMs = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    ChatDurationMs = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    TotalDurationMs = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    InputTokens = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    OutputTokens = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    CacheReadTokens = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    CacheWriteTokens = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    EstimatedCost = table.Column<double>(
                        type: isOracle ? "BINARY_DOUBLE" : null,
                        nullable: true
                    ),
                    ErrorMessage = table.Column<string>(
                        type: isOracle ? "NCLOB" : null,
                        maxLength: 4000,
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopilotRuns", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "CopilotFeedback",
                columns: table => new
                {
                    Id = table
                        .Column<long>(type: isOracle ? "NUMBER(19)" : null, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RunId = table.Column<Guid>(type: isOracle ? "RAW(16)" : null, nullable: false),
                    VoterId = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(128)" : null,
                        maxLength: 128,
                        nullable: false
                    ),
                    Rating = table.Column<int>(
                        type: isOracle ? "NUMBER(10)" : null,
                        nullable: false
                    ),
                    Comment = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(2000)" : null,
                        maxLength: 2000,
                        nullable: true
                    ),
                    CreatedAtUtc = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopilotFeedback", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CopilotFeedback_CopilotRuns_RunId",
                        column: x => x.RunId,
                        principalTable: "CopilotRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "CopilotRunSteps",
                columns: table => new
                {
                    Id = table
                        .Column<long>(type: isOracle ? "NUMBER(19)" : null, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RunId = table.Column<Guid>(type: isOracle ? "RAW(16)" : null, nullable: false),
                    Name = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(64)" : null,
                        maxLength: 64,
                        nullable: false
                    ),
                    Status = table.Column<string>(
                        type: isOracle ? "NVARCHAR2(32)" : null,
                        maxLength: 32,
                        nullable: false
                    ),
                    StartedAtUtc = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: false
                    ),
                    CompletedAtUtc = table.Column<DateTime>(
                        type: isOracle ? "TIMESTAMP(7)" : null,
                        nullable: true
                    ),
                    DurationMs = table.Column<long>(
                        type: isOracle ? "NUMBER(19)" : null,
                        nullable: true
                    ),
                    Detail = table.Column<string>(
                        type: isOracle ? "NCLOB" : null,
                        maxLength: 4000,
                        nullable: true
                    ),
                    ErrorMessage = table.Column<string>(
                        type: isOracle ? "NCLOB" : null,
                        maxLength: 4000,
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CopilotRunSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CopilotRunSteps_CopilotRuns_RunId",
                        column: x => x.RunId,
                        principalTable: "CopilotRuns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CopilotFeedback_Rating_CreatedAtUtc",
                table: "CopilotFeedback",
                columns: new[] { "Rating", "CreatedAtUtc" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CopilotFeedback_RunId_VoterId",
                table: "CopilotFeedback",
                columns: new[] { "RunId", "VoterId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_CopilotRuns_DocumentTypeCode_StartedAtUtc",
                table: "CopilotRuns",
                columns: new[] { "DocumentTypeCode", "StartedAtUtc" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_CopilotRuns_StartedAtUtc",
                table: "CopilotRuns",
                column: "StartedAtUtc"
            );

            migrationBuilder.CreateIndex(
                name: "IX_CopilotRuns_Status",
                table: "CopilotRuns",
                column: "Status"
            );

            migrationBuilder.CreateIndex(
                name: "IX_CopilotRunSteps_RunId_StartedAtUtc",
                table: "CopilotRunSteps",
                columns: new[] { "RunId", "StartedAtUtc" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "CopilotFeedback");

            migrationBuilder.DropTable(name: "CopilotRunSteps");

            migrationBuilder.DropTable(name: "CopilotRuns");
        }
    }
}
