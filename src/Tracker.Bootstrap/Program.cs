using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Tracker.Domain.Context;
using Tracker.Domain.IoC;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        _ = logging.ClearProviders().AddConsole();
    })
    .ConfigureServices((context, services) =>
    {
        _ = services.AddTrackerDbContext("Tracker");
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Starting application bootstrap at {timestamp}", DateTimeOffset.UtcNow);

        var services = scope.ServiceProvider;

        var context = services.GetRequiredService<TrackerDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        logger.LogInformation("Application bootstrap complete at {timestamp}", DateTimeOffset.UtcNow);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Failed to bootstrap application: {message}", ex.Message);
    }
}
