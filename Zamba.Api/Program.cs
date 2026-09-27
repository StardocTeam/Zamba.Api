using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Zamba.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions<Zamba.Api.Configuration.GitHubCopilotOptions>()
    .Bind(builder.Configuration.GetSection(Zamba.Api.Configuration.GitHubCopilotOptions.SectionName));
builder.Services.AddOptions<Zamba.Api.Configuration.VectorStoreOptions>()
    .Bind(builder.Configuration.GetSection(Zamba.Api.Configuration.VectorStoreOptions.SectionName));
builder.Services.AddOptions<Zamba.Api.Configuration.FileProcessingOptions>()
    .Bind(builder.Configuration.GetSection(Zamba.Api.Configuration.FileProcessingOptions.SectionName));
builder.Services.AddOptions<Zamba.Api.Configuration.PdfOcrOptions>()
    .Bind(builder.Configuration.GetSection(Zamba.Api.Configuration.PdfOcrOptions.SectionName));

// Add Entity Framework Core with SQL Server
var connectionString = builder.Configuration.GetConnectionString("ZambaConnectionDEV") 
    ?? throw new InvalidOperationException("Connection string 'ZambaConnectionDEV' not found.");
builder.Services.AddDbContext<Zamba.Api.Data.ZambaDbContext>(options =>
    options.UseSqlServer(connectionString)
);

// Add FluentValidation
builder.Services.AddScoped<IValidator<Zamba.Api.DTOs.CreateOrUpdateScheduleEventDto>, Zamba.Api.Validation.CreateOrUpdateScheduleEventValidator>();
builder.Services.AddScoped<IValidator<Zamba.Api.DTOs.ScheduleConfigDto>, Zamba.Api.Validation.ScheduleConfigValidator>();

// Add Schedule services
builder.Services.AddScoped<Zamba.Api.Services.IScheduleMapper, Zamba.Api.Services.ScheduleMapper>();
builder.Services.AddScoped<Zamba.Api.Services.ICopilotTelemetryService, Zamba.Api.Services.CopilotTelemetryService>();
builder.Services.AddScoped<Zamba.Api.Services.ICopilotTelemetryQueryService, Zamba.Api.Services.CopilotTelemetryQueryService>();

builder.Services.AddSingleton<Zamba.Api.Services.IPdfOcr, Zamba.Api.Services.LocalPdfOcr>();
builder.Services.AddSingleton<Zamba.Api.Services.IFileTextExtractor, Zamba.Api.Services.FileTextExtractor>();
builder.Services.AddSingleton<Zamba.Api.Services.ITextChunker, Zamba.Api.Services.TextChunker>();
builder.Services.AddSingleton<Zamba.Api.Services.IDocumentSkillProvider, Zamba.Api.Services.DocumentSkillProvider>();
builder.Services.AddSingleton<Zamba.Api.Services.IVectorStore, Zamba.Api.Services.SqliteVectorStore>();
builder.Services.AddScoped<Zamba.Api.Services.IMemoryContextService, Zamba.Api.Services.MemoryContextService>();

void ConfigureGitHubModelsClient(HttpClient client, IServiceProvider services)
{
    var options = services.GetRequiredService<Microsoft.Extensions.Options.IOptions<Zamba.Api.Configuration.GitHubCopilotOptions>>().Value;
    client.BaseAddress = new Uri(options.Endpoint.TrimEnd('/') + "/");
    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", options.ApiKey);
}

// GitHub Models (models.github.ai) has been retired, so "Local" (offline, no API key) is the default.
var embeddingProvider = builder.Configuration[$"{Zamba.Api.Configuration.GitHubCopilotOptions.SectionName}:EmbeddingProvider"];
if (string.Equals(embeddingProvider, "GitHubModels", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddHttpClient<Zamba.Api.Services.IEmbeddingClient, Zamba.Api.Services.GitHubModelsEmbeddingClient>(
        (services, client) => ConfigureGitHubModelsClient(client, services));
}
else
{
    builder.Services.AddSingleton<Zamba.Api.Services.IEmbeddingClient, Zamba.Api.Services.LocalHashEmbeddingClient>();
}

// Chat completions go through the GitHub Copilot SDK (agentic CLI runtime) rather than a raw HTTP call.
builder.Services.AddSingleton<Zamba.Api.Services.ICopilotSdkClientProvider, Zamba.Api.Services.CopilotSdkClientProvider>();
builder.Services.AddSingleton<Zamba.Api.Services.ICopilotChatClient, Zamba.Api.Services.CopilotSdkChatClient>();

// Allow the zamba-angular dev server to consume this API
const string ZambaAngularCorsPolicy = "ZambaAngularCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(ZambaAngularCorsPolicy, policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(ZambaAngularCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
