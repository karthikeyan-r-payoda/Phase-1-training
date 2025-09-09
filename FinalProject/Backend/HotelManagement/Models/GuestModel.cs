using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class GuestModel
    {
        [Key]
        public int GuestId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string? Preferences { get; set; }

        // Navigation
        public ICollection<ReservationModel>? Reservations { get; set; }
    }
}
