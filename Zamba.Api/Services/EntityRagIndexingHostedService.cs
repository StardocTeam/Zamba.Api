namespace Zamba.Api.Services;

public sealed class EntityRagIndexingHostedService(
    IServiceScopeFactory scopeFactory,
    ILogger<EntityRagIndexingHostedService> logger) : BackgroundService
{
    private static readonly TimeSpan PollInterval = TimeSpan.FromSeconds(15);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await using var scope = scopeFactory.CreateAsyncScope();
                var indexingService = scope.ServiceProvider.GetRequiredService<IEntityRagIndexingService>();
                var result = await indexingService.ProcessOnInsertPendingAsync(stoppingToken);
                if (result.Attempted > 0)
                {
                    logger.LogInformation(
                        "On-insert RAG pass indexed {Indexed}, skipped {Skipped}, and failed {Failed} documents",
                        result.Indexed,
                        result.Skipped,
                        result.Failed);
                }
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "On-insert RAG indexing pass failed");
            }

            await Task.Delay(PollInterval, stoppingToken);
        }
    }
}