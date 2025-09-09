using System.ComponentModel.DataAnnotations;

public class GenerateReportRequestDto
    {
        [Required]
        public string ReportType { get; set; } = string.Empty; 
      
        public int StaffId { get; set; }
    }