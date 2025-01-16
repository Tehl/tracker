using System.ComponentModel.DataAnnotations;

namespace Tracker.Domain.Models;

public class Activity
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
}
