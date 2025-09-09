using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class AddHouseKeepingTaskDto
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public string TaskDescription { get; set; } = string.Empty;

        
        public int? AssignedToUserId { get; set; }
    }
}
