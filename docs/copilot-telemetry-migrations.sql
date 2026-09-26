BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE TABLE [CopilotRuns] (
        [Id] uniqueidentifier NOT NULL,
        [StartedAtUtc] datetime2 NOT NULL,
        [CompletedAtUtc] datetime2 NULL,
        [Status] nvarchar(32) NOT NULL,
        [Prompt] nvarchar(max) NOT NULL,
        [Response] nvarchar(max) NULL,
        [FileName] nvarchar(512) NULL,
        [DocumentId] nvarchar(64) NULL,
        [DocumentTypeCode] int NULL,
        [SessionId] nvarchar(128) NULL,
        [EmbeddingProvider] nvarchar(64) NULL,
        [ChatModel] nvarchar(128) NULL,
        [DocumentAlreadyIndexed] bit NULL,
        [ChunksIndexed] int NOT NULL,
        [ChunksUsed] int NOT NULL,
        [EmbeddingDurationMs] bigint NULL,
        [RetrievalDurationMs] bigint NULL,
        [ChatDurationMs] bigint NULL,
        [TotalDurationMs] bigint NULL,
        [InputTokens] bigint NULL,
        [OutputTokens] bigint NULL,
        [CacheReadTokens] bigint NULL,
        [CacheWriteTokens] bigint NULL,
        [EstimatedCost] float NULL,
        [ErrorMessage] nvarchar(4000) NULL,
        CONSTRAINT [PK_CopilotRuns] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE TABLE [CopilotFeedback] (
        [Id] bigint NOT NULL IDENTITY,
        [RunId] uniqueidentifier NOT NULL,
        [VoterId] nvarchar(128) NOT NULL,
        [Rating] int NOT NULL,
        [Comment] nvarchar(2000) NULL,
        [CreatedAtUtc] datetime2 NOT NULL,
        CONSTRAINT [PK_CopilotFeedback] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CopilotFeedback_CopilotRuns_RunId] FOREIGN KEY ([RunId]) REFERENCES [CopilotRuns] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE TABLE [CopilotRunSteps] (
        [Id] bigint NOT NULL IDENTITY,
        [RunId] uniqueidentifier NOT NULL,
        [Name] nvarchar(64) NOT NULL,
        [Status] nvarchar(32) NOT NULL,
        [StartedAtUtc] datetime2 NOT NULL,
        [CompletedAtUtc] datetime2 NULL,
        [DurationMs] bigint NULL,
        [Detail] nvarchar(4000) NULL,
        [ErrorMessage] nvarchar(4000) NULL,
        CONSTRAINT [PK_CopilotRunSteps] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_CopilotRunSteps_CopilotRuns_RunId] FOREIGN KEY ([RunId]) REFERENCES [CopilotRuns] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE INDEX [IX_CopilotFeedback_Rating_CreatedAtUtc] ON [CopilotFeedback] ([Rating], [CreatedAtUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE UNIQUE INDEX [IX_CopilotFeedback_RunId_VoterId] ON [CopilotFeedback] ([RunId], [VoterId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE INDEX [IX_CopilotRuns_DocumentTypeCode_StartedAtUtc] ON [CopilotRuns] ([DocumentTypeCode], [StartedAtUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE INDEX [IX_CopilotRuns_StartedAtUtc] ON [CopilotRuns] ([StartedAtUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE INDEX [IX_CopilotRuns_Status] ON [CopilotRuns] ([Status]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    CREATE INDEX [IX_CopilotRunSteps_RunId_StartedAtUtc] ON [CopilotRunSteps] ([RunId], [StartedAtUtc]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260926220710_AddCopilotTelemetry'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260926220710_AddCopilotTelemetry', N'10.0.0');
END;

COMMIT;
GO

