using DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost("GenerateReport")]
    public async Task<IActionResult> GenerateReport([FromBody] GenerateReportRequestDto request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var report = await _reportService.GenerateReportAsync(request);
        return Ok(report);
    }

    [HttpGet("GetAllReports")]
    public async Task<IActionResult> GetAllReports()
    {
        var reports = await _reportService.GetAllReportsAsync();
        return Ok(reports);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReportById(int id)
    {
        var report = await _reportService.GetReportByIdAsync(id);
        if (report == null) return NotFound($"Report with ID {id} not found");

        return Ok(report);
    }

        [HttpGet("Getstaffs")]
    public async Task<IActionResult> GetStaffs()
    {
        var staffList = await _reportService.GetAllStaffAsync();
        return Ok(staffList);
    }
}
