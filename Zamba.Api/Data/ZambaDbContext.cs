using Microsoft.EntityFrameworkCore;

namespace Zamba.Api.Data;

/// <summary>
/// Entity Framework Core database context for Zamba API.
/// Manages all database entities and their relationships.
/// 
/// NOTE: This context is configured for SQL Server by default.
/// To switch to Oracle, see README for configuration changes.
/// </summary>
public class ZambaDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the ZambaDbContext with options.
    /// </summary>
    public ZambaDbContext(DbContextOptions<ZambaDbContext> options)
        : base(options)
    {
    }

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

    public DbSet<Models.CopilotRun> CopilotRuns { get; set; }

    public DbSet<Models.CopilotRunStep> CopilotRunSteps { get; set; }

    public DbSet<Models.CopilotFeedback> CopilotFeedback { get; set; }

    /// <summary>
    /// Configures the model when it is first created.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure ScheduleEvent
        modelBuilder.Entity<Models.ScheduleEvent>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.RuleId)
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(e => e.ExecutionType)
                .HasDefaultValue(Models.ScheduleExecutionType.ExecuteRule);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.EndpointOverride)
                .HasMaxLength(500);

            entity.Property(e => e.Query);

            entity.Property(e => e.EndpointUrl)
                .HasMaxLength(2048);

            entity.Property(e => e.EndpointMethod)
                .HasMaxLength(10);

            entity.Property(e => e.EndpointParameters);

            entity.Property(e => e.EndpointBody);

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255);

            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(255);

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Indexes for common queries
            entity.HasIndex(e => e.RuleId);
            entity.HasIndex(e => e.IsActive);
            entity.HasIndex(e => e.CreatedAt);

            // One-to-many relationship with ScheduleExecutionLog
            entity.HasMany(e => e.ExecutionLogs)
                .WithOne(l => l.ScheduleEvent)
                .HasForeignKey(l => l.ScheduleEventId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-one relationship with ScheduleConfig
            entity.HasOne(e => e.ScheduleConfig)
                .WithOne(c => c.ScheduleEvent)
                .HasForeignKey<Models.ScheduleConfig>(c => c.ScheduleEventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ScheduleConfig
        modelBuilder.Entity<Models.ScheduleConfig>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.CronExpression)
                .HasMaxLength(255);

            entity.Property(e => e.DaysOfWeek)
                .HasMaxLength(50);

            entity.Property(e => e.TimeZone)
                .HasMaxLength(100)
                .HasDefaultValue("Argentina Standard Time");

            // Convert TimeSpan to tinyint for storage (hours) or use time data type
            // SQL Server supports time(7) for TimeSpan without loss
            entity.Property(e => e.TimeOfDay)
                .HasColumnType("time");
        });

        // Configure ScheduleExecutionLog
        modelBuilder.Entity<Models.ScheduleExecutionLog>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.ResponseBody)
                .HasMaxLength(4000); // Truncate to 4000 chars to avoid excessive storage

            entity.Property(e => e.ErrorMessage)
                .HasMaxLength(2000);

            entity.Property(e => e.ScheduledAt)
                .HasPrecision(6); // Millisecond precision

            entity.Property(e => e.StartedAt)
                .HasPrecision(6);

            entity.Property(e => e.FinishedAt)
                .HasPrecision(6);

            // Indexes for common queries
            entity.HasIndex(e => e.ScheduleEventId);
            entity.HasIndex(e => e.ScheduledAt);
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => new { e.ScheduleEventId, e.ScheduledAt });
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
            entity.HasOne(step => step.Run)
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
            entity.HasOne(feedback => feedback.Run)
                .WithMany(run => run.Feedback)
                .HasForeignKey(feedback => feedback.RunId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
