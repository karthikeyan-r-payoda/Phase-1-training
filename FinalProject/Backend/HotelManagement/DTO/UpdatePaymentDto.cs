using System.ComponentModel.DataAnnotations;

public class UpdatePaymentDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required, MaxLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string PaymentStatus { get; set; } = "Pending"; 
       
    }