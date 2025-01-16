using Tracker.Api.Repository;
using Tracker.Domain.IoC;

namespace Tracker.Api.IoC;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTrackerDbContext("Tracker");

        return services.AddTransient<IActivityRepository, ActivityRepository>();
    }
}
