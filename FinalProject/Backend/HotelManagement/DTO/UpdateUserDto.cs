using System.ComponentModel.DataAnnotations;

public class UpdateUserDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public int RoleId { get; set; }

        public string? ShiftSchedule { get; set; }
    }