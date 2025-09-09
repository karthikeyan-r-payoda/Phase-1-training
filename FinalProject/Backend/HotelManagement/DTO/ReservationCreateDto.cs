using System.ComponentModel.DataAnnotations;

public class ReservationCreateDto
    {
        [Required] public int GuestId { get; set; }
        [Required] public int RoomId { get; set; }
        [Required] public int CreatedByUserId { get; set; }
        [Required] public DateTime CheckInDate { get; set; }
        [Required] public DateTime CheckOutDate { get; set; }
    }