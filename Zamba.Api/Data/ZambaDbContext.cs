using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Zamba.Api.Data;

/// <summary>
/// Entity Framework Core database context for Zamba API.
/// Manages all database entities and their relationships.
/// The provider is selected with the Database:Provider configuration setting.
/// </summary>
public class ZambaDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the ZambaDbContext with options.
    /// </summary>
    public ZambaDbContext(DbContextOptions<ZambaDbContext> options)
        : base(options) { }

    /// <summary>
    /// DbSet for Schedule Events.
    /// </summary>
    public DbSet<Models.ScheduleEvent> ScheduleEvents { get; set; }

    /// <summary>
    /// DbSet for Schedule Configurations.
    /// </summary>
    public DbSet<Models.ScheduleConfig> ScheduleConfigs { get; set; }

    /// <summary>
    /// DbSet for Schedule Execution Logs.
    /// </summary>
    public DbSet<Models.ScheduleExecutionLog> ScheduleExecutionLogs { get; set; }

    public DbSet<Models.ScheduleWorkerStatus> ScheduleWorkerStatuses { get; set; }

    public DbSet<Models.CopilotRun> CopilotRuns { get; set; }

    public DbSet<Models.CopilotRunStep> CopilotRunSteps { get; set; }

    public DbSet<Models.CopilotFeedback> CopilotFeedback { get; set; }

    public DbSet<Models.TraceSource> TraceSources { get; set; }

    public DbSet<Models.TraceRecord> TraceRecords { get; set; }

    public DbSet<Models.EntityRagIndexingConfig> EntityRagIndexingConfigs { get; set; }

    public DbSet<Models.EntityRagDocumentState> EntityRagDocumentStates { get; set; }

    public DbSet<Models.DashboardUser> DashboardUsers => Set<Models.DashboardUser>();
    public DbSet<Models.DashboardResetPasswordToken> DashboardResetPasswordTokens =>
        Set<Models.DashboardResetPasswordToken>();
    public DbSet<Models.DashboardPendingVerificationUser> DashboardPendingVerificationUsers =>
        Set<Models.DashboardPendingVerificationUser>();
    public DbSet<Models.DashboardReportCategory> DashboardReportCategories =>
        Set<Models.DashboardReportCategory>();
    public DbSet<Models.DashboardReportDescription> DashboardReportDescriptions =>
        Set<Models.DashboardReportDescription>();
    public DbSet<Models.DashboardReport> DashboardReports => Set<Models.DashboardReport>();
    public DbSet<Models.DashboardSidebarMenuItem> DashboardSidebarMenuItems =>
        Set<Models.DashboardSidebarMenuItem>();
    public DbSet<Models.DashboardCalendarEvent> DashboardCalendarEvents =>
        Set<Models.DashboardCalendarEvent>();
    public DbSet<Models.DashboardWidgetContainer> DashboardWidgetContainers =>
        Set<Models.DashboardWidgetContainer>();
    public DbSet<Models.DashboardCarouselContent> DashboardCarouselContents =>
        Set<Models.DashboardCarouselContent>();
    public DbSet<Models.DashboardCarouselConfig> DashboardCarouselConfigs =>
        Set<Models.DashboardCarouselConfig>();
    public DbSet<Models.DashboardVideoPlayerSource> DashboardVideoPlayerSources =>
        Set<Models.DashboardVideoPlayerSource>();
    public DbSet<Models.DashboardRole> DashboardRoles => Set<Models.DashboardRole>();
    public DbSet<Models.DashboardUserConfiguration> DashboardUserConfigurations =>
        Set<Models.DashboardUserConfiguration>();
    public DbSet<Models.DashboardUserRight> DashboardUserRights => Set<Models.DashboardUserRight>();
    public DbSet<Models.DashboardUserGroup> DashboardUserGroups => Set<Models.DashboardUserGroup>();
    public DbSet<Models.DashboardGroupInheritance> DashboardGroupInheritances =>
        Set<Models.DashboardGroupInheritance>();
    public DbSet<Models.DashboardWorkflow> DashboardWorkflows => Set<Models.DashboardWorkflow>();
    public DbSet<Models.DashboardWorkflowStep> DashboardWorkflowSteps =>
        Set<Models.DashboardWorkflowStep>();
    public DbSet<Models.DashboardDocumentType> DashboardDocumentTypes =>
        Set<Models.DashboardDocumentType>();
    public DbSet<Models.DashboardWorkflowDocument> DashboardWorkflowDocuments =>
        Set<Models.DashboardWorkflowDocument>();
    public DbSet<Models.VacationDocument> VacationDocuments => Set<Models.VacationDocument>();
    public DbSet<Models.DashboardDepartment> DashboardDepartments =>
        Set<Models.DashboardDepartment>();

    /// <summary>
    /// Configures the model when it is first created.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var isOracle = string.Equals(
            Database.ProviderName,
            "Oracle.EntityFrameworkCore",
            StringComparison.Ordinal
        );

        // Configure ScheduleEvent
        modelBuilder.Entity<Models.ScheduleEvent>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name).IsRequired().HasMaxLength(255);

            entity.Property(e => e.RuleId).IsRequired().HasMaxLength(255);

            entity
                .Property(e => e.ExecutionType)
                .HasDefaultValue(Models.ScheduleExecutionType.ExecuteRule);

            entity.Property(e => e.Description).HasMaxLength(1000);

            entity.Property(e => e.ExpectedResult);

            entity.Property(e => e.NotificationEmail).HasMaxLength(320);

            entity.Property(e => e.NotifyOnSuccess).HasDefaultValue(false);

            entity.Property(e => e.NotifyOnFailure).HasDefaultValue(false);

            entity.Property(e => e.NotifySlackOnSuccess).HasDefaultValue(false);

            entity.Property(e => e.NotifySlackOnFailure).HasDefaultValue(false);

            entity.Property(e => e.EndpointOverride).HasMaxLength(500);

            entity.Property(e => e.Query);

            entity.Property(e => e.EndpointUrl).HasMaxLength(2048);

            entity.Property(e => e.EndpointMethod).HasMaxLength(10);

            entity.Property(e => e.EndpointParameters);

            entity.Property(e => e.EndpointBody);

            entity.Property(e => e.RagDocumentTypeId).HasColumnType("decimal(18,0)");

            entity.Property(e => e.CreatedBy).HasMaxLength(255);

            entity.Property(e => e.UpdatedBy).HasMaxLength(255);

            entity
                .Property(e => e.CreatedAt)
                .HasDefaultValueSql(isOracle ? "SYS_EXTRACT_UTC(SYSTIMESTAMP)" : "GETUTCDATE()");

            entity
                .Property(e => e.UpdatedAt)
                .HasDefaultValueSql(isOracle ? "SYS_EXTRACT_UTC(SYSTIMESTAMP)" : "GETUTCDATE()");

            // Indexes for common queries
            entity.HasIndex(e => e.RuleId);
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.CreatedAt);
            var executionTypeDocumentTypeIndex = entity.HasIndex(e => new
            {
                e.ExecutionType,
                e.RagDocumentTypeId,
            });
            if (isOracle)
            {
                executionTypeDocumentTypeIndex.IsUnique();
            }
            else
            {
                executionTypeDocumentTypeIndex
                    .IsUnique()
                    .HasFilter("[RagDocumentTypeId] IS NOT NULL");
            }

            // One-to-many relationship with ScheduleExecutionLog
            entity
                .HasMany(e => e.ExecutionLogs)
                .WithOne(l => l.ScheduleEvent)
                .HasForeignKey(l => l.ScheduleEventId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one relationship with ScheduleConfig
            entity
                .HasOne(e => e.ScheduleConfig)
                .WithOne(c => c.ScheduleEvent)
                .HasForeignKey<Models.ScheduleConfig>(c => c.ScheduleEventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ScheduleConfig
        modelBuilder.Entity<Models.ScheduleConfig>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CronExpression).HasMaxLength(255);

            entity.Property(e => e.DaysOfWeek).HasMaxLength(50);

            entity
                .Property(e => e.TimeZone)
                .HasMaxLength(100)
                .HasDefaultValue("Argentina Standard Time");

            // Convert TimeSpan to tinyint for storage (hours) or use time data type
            // SQL Server supports time(7) for TimeSpan without loss
            var timeOfDay = entity.Property(e => e.TimeOfDay);
            if (!isOracle)
            {
                timeOfDay.HasColumnType("time");
            }
        });

        // Configure ScheduleExecutionLog
        modelBuilder.Entity<Models.ScheduleExecutionLog>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ResponseBody).HasMaxLength(4000); // Truncate to 4000 chars to avoid excessive storage

            entity.Property(e => e.Result).HasMaxLength(20);

            entity.Property(e => e.ErrorMessage).HasMaxLength(2000);

            entity.Property(e => e.ScheduledAt).HasPrecision(6); // Millisecond precision

            entity.Property(e => e.StartedAt).HasPrecision(6);

            entity.Property(e => e.FinishedAt).HasPrecision(6);

            // Indexes for common queries
            entity.HasIndex(e => e.ScheduleEventId);
            entity.HasIndex(e => e.ScheduledAt);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => new { e.ScheduleEventId, e.ScheduledAt });
        });

        modelBuilder.Entity<Models.ScheduleWorkerStatus>(entity =>
        {
            entity.HasKey(status => status.WorkerInstanceId);
            entity.Property(status => status.WorkerInstanceId).HasMaxLength(128);
            entity.Property(status => status.HostName).HasMaxLength(255).IsRequired();
            entity.Property(status => status.LastError).HasMaxLength(2000);
            entity.HasIndex(status => status.LastHeartbeatAtUtc);
        });

        modelBuilder.Entity<Models.CopilotRun>(entity =>
        {
            entity.HasKey(run => run.Id);
            entity.Property(run => run.Prompt).IsRequired();
            entity.Property(run => run.Status).HasMaxLength(32).IsRequired();
            entity.Property(run => run.FileName).HasMaxLength(512);
            entity.Property(run => run.DocumentId).HasMaxLength(64);
            entity.Property(run => run.EmbeddingProvider).HasMaxLength(64);
            entity.Property(run => run.ChatModel).HasMaxLength(128);
            entity.Property(run => run.SessionId).HasMaxLength(128);
            entity.Property(run => run.ErrorMessage).HasMaxLength(4000);
            entity.Property(run => run.Response).IsRequired(false);
            entity.HasIndex(run => run.StartedAtUtc);
            entity.HasIndex(run => run.Status);
            entity.HasIndex(run => new { run.DocumentTypeCode, run.StartedAtUtc });
        });

        modelBuilder.Entity<Models.CopilotRunStep>(entity =>
        {
            entity.HasKey(step => step.Id);
            entity.Property(step => step.Name).HasMaxLength(64).IsRequired();
            entity.Property(step => step.Status).HasMaxLength(32).IsRequired();
            entity.Property(step => step.Detail).HasMaxLength(4000);
            entity.Property(step => step.ErrorMessage).HasMaxLength(4000);
            entity.HasIndex(step => new { step.RunId, step.StartedAtUtc });
            entity
                .HasOne(step => step.Run)
                .WithMany(run => run.Steps)
                .HasForeignKey(step => step.RunId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Models.CopilotFeedback>(entity =>
        {
            entity.HasKey(feedback => feedback.Id);
            entity.Property(feedback => feedback.VoterId).HasMaxLength(128).IsRequired();
            entity.Property(feedback => feedback.Comment).HasMaxLength(2000);
            entity.HasIndex(feedback => new { feedback.RunId, feedback.VoterId }).IsUnique();
            entity.HasIndex(feedback => new { feedback.Rating, feedback.CreatedAtUtc });
            entity
                .HasOne(feedback => feedback.Run)
                .WithMany(run => run.Feedback)
                .HasForeignKey(feedback => feedback.RunId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Models.TraceSource>(entity =>
        {
            entity.HasKey(source => source.Id);
            entity.Property(source => source.ApplicationName).HasMaxLength(128).IsRequired();
            entity.Property(source => source.Environment).HasMaxLength(64).IsRequired();
            entity.Property(source => source.FolderPath).HasMaxLength(2048).IsRequired();
            entity
                .HasIndex(source => new
                {
                    source.ApplicationName,
                    source.Environment,
                    source.FolderPath,
                })
                .IsUnique();
        });

        modelBuilder.Entity<Models.TraceRecord>(entity =>
        {
            entity.HasKey(trace => trace.Id);
            entity.Property(trace => trace.RelativePath).HasMaxLength(2048).IsRequired();
            entity.Property(trace => trace.FileName).HasMaxLength(512).IsRequired();
            entity.Property(trace => trace.UserId).HasMaxLength(128);
            entity.Property(trace => trace.Title).HasMaxLength(512).IsRequired();
            entity.HasIndex(trace => new { trace.TraceSourceId, trace.RelativePath }).IsUnique();
            entity.HasIndex(trace => new { trace.TraceSourceId, trace.TraceTimestampUtc });
            entity.HasIndex(trace => trace.UserId);
            entity
                .HasOne(trace => trace.TraceSource)
                .WithMany(source => source.Traces)
                .HasForeignKey(trace => trace.TraceSourceId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Models.EntityRagIndexingConfig>(entity =>
        {
            entity.HasKey(config => config.DocumentTypeId);
            entity.Property(config => config.DocumentTypeId).HasPrecision(18, 0);
            entity.Property(config => config.IndexMode).HasConversion<int>();
            entity.HasIndex(config => new { config.IsEnabled, config.IndexMode });
        });

        modelBuilder.Entity<Models.EntityRagDocumentState>(entity =>
        {
            entity.HasKey(state => new { state.DocumentTypeId, state.DocumentId });
            entity.Property(state => state.DocumentTypeId).HasPrecision(18, 0);
            entity.Property(state => state.DocumentId).HasPrecision(18, 0);
            entity.Property(state => state.Status).HasConversion<int>();
            entity.Property(state => state.VectorDocumentId).HasMaxLength(64);
            entity.Property(state => state.FileName).HasMaxLength(512);
            entity.Property(state => state.LastError).HasMaxLength(4000);
            entity.HasIndex(state => new
            {
                state.DocumentTypeId,
                state.Status,
                state.UpdatedAtUtc,
            });
        });

        DashboardModelConfiguration.Configure(modelBuilder, isOracle);

        modelBuilder.Entity<Models.DashboardDepartment>(entity =>
        {
            entity.ToTable("SLST_S152", isOracle ? null : "zambabpm_RRHH");
            entity.HasKey(row => row.Id);
            entity.Property(row => row.Id).HasColumnName("Codigo").HasColumnType("numeric(18,0)");
            entity
                .Property(row => row.Name)
                .HasColumnName("Descripcion")
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Models.VacationDocument>(entity =>
        {
            entity.ToTable("DOC_I110", isOracle ? null : "zambabpm_RRHH");
            entity.HasKey(row => row.DocumentId);
            entity
                .Property(row => row.DocumentId)
                .HasColumnName("DOC_ID")
                .HasColumnType("numeric(18,0)");
            entity.Property(row => row.LastUpdate).HasColumnName("lupdate");
            entity.Property(row => row.Created).HasColumnName("crdate");
            entity.Property(row => row.Date).HasColumnName("Fecha");
            entity
                .Property(row => row.I110)
                .HasColumnName("I110")
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(row => row.I166).HasColumnName("I166").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I168).HasColumnName("I168").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I169).HasColumnName("I169").HasColumnType("numeric(18,0)");
            entity
                .Property(row => row.I136)
                .HasColumnName("I136")
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(row => row.I189).HasColumnName("I189").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I224).HasColumnName("I224").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I192).HasColumnName("I192").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I227).HasColumnName("I227");
            entity.Property(row => row.I228).HasColumnName("I228");
            entity.Property(row => row.I225).HasColumnName("I225").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I229).HasColumnName("I229");
            entity.Property(row => row.I230).HasColumnName("I230");
            entity.Property(row => row.I226).HasColumnName("I226").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I164).HasColumnName("I164").HasColumnType("numeric(18,0)");
            entity
                .Property(row => row.I102)
                .HasColumnName("I102")
                .HasMaxLength(100)
                .IsUnicode(false);
            entity
                .Property(row => row.I101)
                .HasColumnName("I101")
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(row => row.I239).HasColumnName("I239").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I165).HasColumnName("i165").HasColumnType("numeric(7,0)");
            entity.Property(row => row.I262).HasColumnName("I262");
            entity.Property(row => row.I264).HasColumnName("I264").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I283).HasColumnName("I283");
            entity.Property(row => row.I258).HasColumnName("i258").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I295).HasColumnName("I295").HasColumnType("numeric(18,0)");
            entity.Property(row => row.I167).HasColumnName("I167").HasColumnType("numeric(18,0)");
            entity
                .Property(row => row.I170)
                .HasColumnName("i170")
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(row => row.I290).HasColumnName("I290").HasColumnType("numeric(18,0)");
        });

        if (isOracle)
        {
            ConfigureOracleIdentifiers(modelBuilder);
        }
    }

    private static void ConfigureOracleIdentifiers(ModelBuilder modelBuilder)
    {
        var migrationManagedTables = new HashSet<string>(StringComparer.Ordinal)
        {
            "ScheduleEvents",
            "ScheduleConfigs",
            "ScheduleExecutionLogs",
            "ScheduleWorkerStatuses",
            "CopilotRuns",
            "CopilotRunSteps",
            "CopilotFeedback",
            "TraceSources",
            "TraceRecords",
            "EntityRagIndexingConfigs",
            "EntityRagDocumentStates",
        };

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName is null)
            {
                continue;
            }

            entityType.SetSchema(null);

            // These tables were created by EF migrations with quoted PascalCase names.
            // Oracle preserves the case of quoted identifiers, so normalizing them to
            // uppercase makes runtime queries target different, nonexistent objects.
            if (migrationManagedTables.Contains(tableName))
            {
                continue;
            }

            entityType.SetTableName(tableName.ToUpperInvariant());
            var table = StoreObjectIdentifier.Table(
                entityType.GetTableName()!,
                entityType.GetSchema()
            );
            foreach (var property in entityType.GetProperties())
            {
                var propertyType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
                if (propertyType == typeof(bool))
                {
                    property.SetValueConverter(new BoolToZeroOneConverter<int>());
                    property.SetColumnType("NUMBER(1)");
                }

                var columnName = property.GetColumnName(table);
                if (columnName is not null)
                {
                    property.SetColumnName(columnName.ToUpperInvariant());
                }
            }
        }
    }
}
