using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceController : ControllerBase
{
    private readonly IMaintenanceService _maintenanceService;

    public MaintenanceController(IMaintenanceService maintenanceService)
    {
        _maintenanceService = maintenanceService;
    }

    [HttpGet("GetAllMaintenances")]
    public async Task<ActionResult<IEnumerable<MaintenanceRequestDto>>> GetAll()
    {
        var requests = await _maintenanceService.GetAllRequestsAsync();
        return Ok(requests);
    }

    [HttpGet("GetMaintenanceById/{id}")]
    public async Task<ActionResult<MaintenanceRequestDto>> GetById(int id)
    {
        var request = await _maintenanceService.GetRequestByIdAsync(id);
        if (request == null) return NotFound(new { Message = "Maintenance request not found." });
        return Ok(request);
    }

    [HttpPost("AddMaintenance")]
    public async Task<ActionResult<MaintenanceRequestDto>> Add(AddMaintenanceRequestDto dto)
    {
        var request = await _maintenanceService.AddRequestAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = request.MaintenanceRequestId }, request);
    }

    [HttpPut("UpdateMaintenance/{id}")]
    public async Task<ActionResult<MaintenanceRequestDto>> Update(int id, UpdateMaintenanceRequestDto dto)
    {
        if (id != dto.MaintenanceRequestId)
            return BadRequest(new { Message = "Request ID mismatch." });

        var updated = await _maintenanceService.UpdateRequestAsync(dto);
        if (updated == null) return NotFound(new { Message = "Maintenance request not found." });

        return Ok(updated);
    }

    [HttpDelete("DeleteMaintenance/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _maintenanceService.DeleteRequestAsync(id);
        if (!deleted) return NotFound(new { Message = "Maintenance request not found." });

        return NoContent();
    }
}
