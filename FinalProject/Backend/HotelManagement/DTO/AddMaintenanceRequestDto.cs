using System.ComponentModel.DataAnnotations;

public class AddMaintenanceRequestDto
{
    [Required]
    public int RoomId { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    public int? AssignedToUserId { get; set; } 
}
