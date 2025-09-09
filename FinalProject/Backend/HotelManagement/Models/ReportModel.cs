using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

public class ReportModel
{
    [Key]
    public int ReportId { get; set; }

    [Required]
    public string ReportType { get; set; } = string.Empty; 

    [ForeignKey("GeneratedByStaff")]
    public int GeneratedByStaffId { get; set; }

    public DateTime GeneratedDate { get; set; } = DateTime.Now;

    [Required]
    public string ReportData { get; set; } = string.Empty;

 
    public UserModel GeneratedByStaff { get; set; }
}
