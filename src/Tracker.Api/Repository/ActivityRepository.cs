using Microsoft.EntityFrameworkCore;
using Tracker.Domain.Context;
using Tracker.Domain.Models;

namespace Tracker.Api.Repository;

internal class ActivityRepository : IActivityRepository
{
    private readonly TrackerDbContext _dbContext;

    public ActivityRepository(TrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Activity[]> GetAsync()
    {
        return _dbContext.Activities.ToArrayAsync();
    }

    public Task<Activity?> GetByIdAsync(int id)
    {
        return _dbContext.Activities.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Activity> CreateAsync(Activity activity)
    {
        _dbContext.Attach(activity);
        await _dbContext.SaveChangesAsync();
        return activity;
    }

    public async Task<Activity> UpdateAsync(Activity activity)
    {
        _dbContext.Entry(activity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return activity;
    }

    public async Task DeleteAsync(int id)
    {
        _dbContext.Entry(new Activity { Id = id }).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await GetByIdAsync(id) != null;
    }
}
