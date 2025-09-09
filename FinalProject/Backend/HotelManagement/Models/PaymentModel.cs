using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

public class PaymentModel
{
    [Key, ForeignKey("Reservation")]  // Primary Key == Foreign Key
    public int ReservationId { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(50)]
    public string PaymentMethod { get; set; } = string.Empty; 
   

    [Required]
    [MaxLength(50)]
    public string PaymentStatus { get; set; } = "Pending"; 
   

    
    public ReservationModel? Reservation { get; set; }
}
