
using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReportService
{
    Task<ReportDto> GenerateReportAsync(GenerateReportRequestDto request);
    Task<IEnumerable<ReportDto>> GetAllReportsAsync();
    Task<ReportDto?> GetReportByIdAsync(int reportId);

    Task<IEnumerable<StaffDto>> GetAllStaffAsync();
}
