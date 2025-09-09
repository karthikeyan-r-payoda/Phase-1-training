using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

public class MaintenanceRequestModel
{
    [Key]
    public int MaintenanceRequestId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending"; 
  

    public int? AssignedToUserId { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? CompletedAt { get; set; }

   
    public RoomModel? Room { get; set; }
    public UserModel? AssignedToUser { get; set; }
}
