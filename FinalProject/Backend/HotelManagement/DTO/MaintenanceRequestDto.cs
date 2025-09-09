using System;

public class MaintenanceRequestDto
{
    public int MaintenanceRequestId { get; set; }
    public int RoomId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int? AssignedToUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

   
    public string? RoomNumber { get; set; }
    public string? AssignedToUserName { get; set; }
}
