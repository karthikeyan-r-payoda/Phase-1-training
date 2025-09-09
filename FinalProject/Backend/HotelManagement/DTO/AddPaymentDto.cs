using System.ComponentModel.DataAnnotations;

public class AddPaymentDto
    {
        [Required]
        public int ReservationId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required, MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty; 
        
    }