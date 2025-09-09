using DTOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class HouseKeepingController : ControllerBase
{
    private readonly IHouseKeepingService _houseKeepingService;

    public HouseKeepingController(IHouseKeepingService houseKeepingService)
    {
        _houseKeepingService = houseKeepingService;
    }

    [HttpGet("GetAllTasks")]
    public async Task<ActionResult<IEnumerable<HouseKeepingTaskModel>>> GetTasks()
    {
        var tasks = await _houseKeepingService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("GetTaskById/{id}")]
    public async Task<ActionResult<HouseKeepingTaskModel>> GetTask(int id)
    {
        var task = await _houseKeepingService.GetTaskByIdAsync(id);
        if (task == null) return NotFound(new { Message = "Housekeeping task not found." });

        return Ok(task);
    }

[HttpPost("AddTask")]
public async Task<ActionResult<HouseKeepingTaskModel>> AddTask(AddHouseKeepingTaskDto dto)
{
    var newTask = await _houseKeepingService.AddTaskAsync(dto);
    return CreatedAtAction(nameof(GetTask), new { id = newTask.TaskId }, newTask);
}


  [HttpPut("UpdateTask/{id}")]
public async Task<ActionResult<HouseKeepingTaskModel>> UpdateTask(int id, UpdateHouseKeepingTaskDto dto)
{
    if (id != dto.TaskId)
        return BadRequest(new { Message = "Task ID mismatch." });

    var updatedTask = await _houseKeepingService.UpdateTaskAsync(dto);
    if (updatedTask == null)
        return NotFound(new { Message = "Task not found." });

    return Ok(updatedTask);
}


    [HttpDelete("DeleteTask/{id}")]
    public async Task<ActionResult> DeleteTask(int id)
    {
        var deleted = await _houseKeepingService.DeleteTaskAsync(id);
        if (!deleted) return NotFound(new { Message = "Housekeeping task not found." });

        return NoContent();
    }
}
