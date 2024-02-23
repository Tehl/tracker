using System.ComponentModel.DataAnnotations;

namespace Tracker.Domain.Models;

public class Activity
{
    public int Id { get; set; }

    [StringLength(100)]
    public required string Name { get; set; }
}
