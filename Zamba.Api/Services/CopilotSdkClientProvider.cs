using GitHub.Copilot;

namespace Zamba.Api.Services;

public interface ICopilotSdkClientProvider
{
    Task<CopilotClient> GetClientAsync(CancellationToken cancellationToken);
}

/// <summary>
/// Lazily starts and shares a single <see cref="CopilotClient"/> runtime process for the lifetime
/// of the application. Mode is Empty since this is a shared web/API server, not an interactive CLI.
/// </summary>
public sealed class CopilotSdkClientProvider : ICopilotSdkClientProvider, IAsyncDisposable
{
    private readonly SemaphoreSlim _startLock = new(1, 1);
    private CopilotClient? _client;

    public async Task<CopilotClient> GetClientAsync(CancellationToken cancellationToken)
    {
        if (_client is not null)
        {
            return _client;
        }

        await _startLock.WaitAsync(cancellationToken);
        try
        {
            if (_client is null)
            {
                var baseDirectory = Path.Combine(AppContext.BaseDirectory, ".copilot-home");
                Directory.CreateDirectory(baseDirectory);

                var client = new CopilotClient(new CopilotClientOptions
                {
                    Mode = CopilotClientMode.Empty,
                    BaseDirectory = baseDirectory,
                    UseLoggedInUser = false
                });
                await client.StartAsync();
                _client = client;
            }

            return _client;
        }
        finally
        {
            _startLock.Release();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_client is not null)
        {
            await _client.DisposeAsync();
        }

        _startLock.Dispose();
    }
}
