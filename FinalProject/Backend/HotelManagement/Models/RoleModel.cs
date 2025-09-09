using System.ComponentModel.DataAnnotations;
namespace Models;
public class RoleModel
{
    [Key]
    public int RoleId { get; set; }
    public string? RoleName { get; set;}
}

