namespace Dto;
public class AddUserDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public int RoleId { get; set; }
    public string? Phone { get; set; }
    public string? ShiftSchedule { get; set; }
    public DateTime CreatedAt { get; set; }
}