public class ReportDto
{
    public int ReportId { get; set; }
    public string ReportType { get; set; }
    public int GeneratedByStaffId { get; set; }
    public DateTime GeneratedDate { get; set; }
    public string ReportData { get; set; }
}
