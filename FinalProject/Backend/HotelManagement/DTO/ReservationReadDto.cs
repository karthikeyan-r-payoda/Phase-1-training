  public class ReservationReadDto
    {
        public int ReservationId { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
          public string RoomNo  { get; set; }
        public string ReservationStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
    }