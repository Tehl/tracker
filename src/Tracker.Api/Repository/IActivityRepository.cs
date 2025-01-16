using Tracker.Domain.Models;

namespace Tracker.Api.Repository;
public interface IActivityRepository
{
    Task<Activity[]> GetAsync();
    Task<Activity?> GetByIdAsync(int id);
    Task<Activity> CreateAsync(Activity activity);
    Task<Activity> UpdateAsync(Activity activity);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
