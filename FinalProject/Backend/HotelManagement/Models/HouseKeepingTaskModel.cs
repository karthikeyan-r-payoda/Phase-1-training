using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class HouseKeepingTaskModel
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [ForeignKey("Room")]
        public int RoomId { get; set; }

        [Required]
        public string TaskDescription { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Pending"; 
        // Pending, InProgress, Completed

        [Required]
        public DateTime AssignedDate { get; set; } = DateTime.Now;

        public DateTime? CompletedDate { get; set; }

        // Optional: Assign task to a staff member
        [ForeignKey("User")]
        public int? AssignedToUserId { get; set; }

        // Navigation
        public RoomModel? Room { get; set; }
        public UserModel? AssignedToUser { get; set; }
    }
}
