using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ReservationModel
    {
        [Key]
        public int ReservationId { get; set; }

        // Foreign Keys
        [Required]
        public int GuestId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }  // Staff who created the reservation

        // Reservation Details
        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string ReservationStatus { get; set; } = "Pending";
       

        [MaxLength(50)]
        public string PaymentStatus { get; set; } = "Unpaid";
       

        public DateTime DateCreated { get; set; } = DateTime.Now;

        
        public GuestModel? Guest { get; set; }
        public RoomModel? Room { get; set; }
        public UserModel? CreatedByUser { get; set; }
        
        
    
    public PaymentModel? Payment { get; set; }
    }
}
