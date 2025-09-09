using System;
using System.ComponentModel.DataAnnotations;

public class UpdateMaintenanceRequestDto
{
    [Required]
    public int MaintenanceRequestId { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty; 
 

    public int? AssignedToUserId { get; set; }
    public DateTime? CompletedAt { get; set; }
}
