using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

      
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public RoleModel? Role { get; set; }

        [Phone]
        public string Phone { get; set; } = string.Empty;

        public string? ShiftSchedule { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
