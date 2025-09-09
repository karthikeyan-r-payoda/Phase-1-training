using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet("GetAllRooms")]
    public async Task<ActionResult<IEnumerable<RoomModel>>> GetRooms()
    {
        var rooms = await _roomService.GetAllRoomsAsync();
        return Ok(rooms);
    }

    [HttpGet("GetRoomById/{id}")]
    public async Task<ActionResult<RoomModel>> GetRoom(int id)
    {
        var room = await _roomService.GetRoomByIdAsync(id);
        if (room == null) return NotFound();
        return Ok(room);
    }

    [HttpPost("AddRoom")]
    public async Task<ActionResult<RoomModel>> AddRoom(RoomModel room)
    {
        var newRoom = await _roomService.AddRoomAsync(room);
        return CreatedAtAction(nameof(GetRoom), new { id = newRoom.RoomId }, newRoom);
    }

    [HttpPut("UpdateRoom/{id}")]
    public async Task<ActionResult<RoomModel>> UpdateRoom(int id, RoomModel room)
    {
        var updatedRoom = await _roomService.UpdateRoomAsync(id, room);
        if (updatedRoom == null) return NotFound();
        return Ok(updatedRoom);
    }

    [HttpDelete("DeleteRoom/{id}")]
    public async Task<ActionResult> DeleteRoom(int id)
    {
        var deleted = await _roomService.DeleteRoomAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
