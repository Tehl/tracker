using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tracker.Domain.Context;

namespace Tracker.Domain.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTrackerDbContext(
        this IServiceCollection services,
        string connectionStringName
    )
    {
        return services.AddDbContext<TrackerDbContext>((serviceProvider, dbContextOptions) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(connectionStringName);

            var serverVersion = ServerVersion.AutoDetect(connectionString);

            _ = dbContextOptions.UseMySql(
                connectionString,
                serverVersion,
                x => x.MigrationsAssembly("Tracker.Bootstrap")
            );
        });
    }
}
