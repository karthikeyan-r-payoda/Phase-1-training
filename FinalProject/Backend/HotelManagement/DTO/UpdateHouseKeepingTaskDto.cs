using System;
using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UpdateHouseKeepingTaskDto
    {
        [Required]
        public int TaskId { get; set; } 

        [Required]
        public string Status { get; set; } = string.Empty; 
        

        public int? AssignedToUserId { get; set; }  
        public DateTime? CompletedDate { get; set; } 
    }
}
