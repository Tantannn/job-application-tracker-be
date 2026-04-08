using JobTracker.API.Enums;

namespace JobTracker.API.Entities;

public class JobApplication
{
  public int Id { get; set; }
  public int UserId { get; set; }
  public string Company { get; set; } = string.Empty;
  public string Role { get; set; } = string.Empty;
  public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
  public DateTime AppliedDate { get; set; }
  public string? Notes { get; set; }
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
  public User User { get; set; } = null!;
  
}