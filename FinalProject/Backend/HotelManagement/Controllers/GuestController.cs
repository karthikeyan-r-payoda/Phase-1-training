using DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class GuestController : ControllerBase
{
    private readonly IGuestService _guestService;

    public GuestController(IGuestService guestService)
    {
        _guestService = guestService;
    }

    [HttpGet("GetAllGuests")]
    public async Task<IActionResult> GetAllGuests()
    {
        var guests = await _guestService.GetAllGuestsAsync();
        return Ok(guests);
    }

    [HttpGet("GetGuestById/{id}")]
    public async Task<IActionResult> GetGuestById(int id)
    {
        var guest = await _guestService.GetGuestByIdAsync(id);
        if (guest == null) return NotFound($"Guest with ID {id} not found");
        return Ok(guest);
    }

    [HttpPost("AddGuest")]
    public async Task<IActionResult> AddGuest([FromBody] AddGuestDto guestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var guest = await _guestService.AddGuestAsync(guestDto);
        return CreatedAtAction(nameof(GetGuestById), new { id = guest.GuestId }, guest);
    }

    [HttpPut("UpdateGuest/{id}")]
    public async Task<IActionResult> UpdateGuest(int id, [FromBody] UpdateGuestDto guestDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updatedGuest = await _guestService.UpdateGuestAsync(id, guestDto);
        if (updatedGuest == null) return NotFound($"Guest with ID {id} not found");

        return Ok(updatedGuest);
    }

    [HttpDelete("DeleteGuest/{id}")]
    public async Task<IActionResult> DeleteGuest(int id)
    {
        var deleted = await _guestService.DeleteGuestAsync(id);
        if (!deleted) return NotFound($"Guest with ID {id} not found");

        return NoContent();
    }
}
