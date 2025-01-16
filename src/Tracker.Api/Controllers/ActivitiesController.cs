using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.Api.Repository;
using Tracker.Domain.Models;

namespace Tracker.Api.Controllers;

[Route("Activities")]
public class ActivitiesController : Controller
{
    private readonly IActivityRepository _activityRepository;

    public ActivitiesController(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Activity[]), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        var activities = await _activityRepository.GetAsync();

        return Ok(activities);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(Activity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var activity = await _activityRepository.GetByIdAsync(id);
        if (activity == null)
        {
            return NotFound();
        }

        return Ok(activity);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Activity), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Create([FromBody] Activity activity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _activityRepository.CreateAsync(activity);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(typeof(Activity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] Activity activity)
    {
        if (activity != null && activity.Id != default && activity.Id != id)
        {
            ModelState.AddModelError(nameof(activity.Id), "Activity to be updated is not located at this URI");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        activity!.Id = id;
        try
        {
            var result = await _activityRepository.UpdateAsync(activity);

            return Ok(result);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _activityRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            throw;
        }
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(typeof(Activity), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _activityRepository.DeleteAsync(id);

            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _activityRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            throw;
        }
    }
}
