using System.ComponentModel.DataAnnotations;

public class RoomModel
{
    [Key]
    public int RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty; 
    public decimal Price { get; set; }
    public string Status { get; set; } = "Available"; 
    public int Capacity { get; set; }
    public Boolean AC { get; set; }

}