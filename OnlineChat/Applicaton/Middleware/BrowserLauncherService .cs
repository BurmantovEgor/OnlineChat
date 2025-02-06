using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class BrowserLauncherService : IHostedService
{
    private readonly ILogger<BrowserLauncherService> _logger;

    public BrowserLauncherService(ILogger<BrowserLauncherService> logger)
    {
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            string[] urls =
            {
                "https://localhost:7027/SendMessage",
                "https://localhost:7027/ReceiveMessage",
                "https://localhost:7027/GetLastMessages"
            };

            foreach (var url in urls)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Ошибка при запуске браузера: {ex.Message}");
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
