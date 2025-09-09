using System.ComponentModel.DataAnnotations;

public class ReservationUpdateDto
    {
        [Required] public DateTime CheckInDate { get; set; }
        [Required] public DateTime CheckOutDate { get; set; }
        [Required] public decimal TotalAmount { get; set; }
        [Required] public int RoomId { get; set; }
    }