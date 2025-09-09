using DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationController(IReservationService service)
    {
        _service = service;
    }

    [HttpGet("GetAllReservations")]
    public async Task<IActionResult> GetAll()
    {
        var reservations = await _service.GetAllReservationsAsync();
        return Ok(reservations);
    }

    [HttpGet("GetReservationById/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var reservation = await _service.GetReservationByIdAsync(id);
        if (reservation == null) return NotFound();
        return Ok(reservation);
    }

    [HttpPost("AddReservation")]
    public async Task<IActionResult> Add([FromBody] ReservationCreateDto dto)
    {
        try
        {
            var reservation = await _service.AddReservationAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = reservation.ReservationId }, reservation);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("UpdateReservation/{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ReservationUpdateDto dto)
    {
        var updated = await _service.UpdateReservationAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

 [HttpPut("CancelReservation/{id}")]
public async Task<IActionResult> Cancel(int id)
{
    var result = await _service.CancelReservationAsync(id);

    if (result == "Reservation not found")
        return NotFound(new { message = result });

    if (result == "Cannot cancel after check-in")
        return BadRequest(new { message = result });
    
    if (result == "The Reservation already Checked out")
        return BadRequest(new { message = result });

     if (result == "The Reservation already Cancelled")
        return BadRequest(new { message = result });


    return Ok(new { message = result });
}


    [HttpPut("checkout/{id}")]
public async Task<IActionResult> CheckOutReservation(int id)
{
    var result = await _service.CheckOutReservationAsync(id);

    if (result == "Reservation not found")
        return NotFound(new { message = result });

   
    if (result == "Cannot check out after Cancelled")
        return BadRequest(new { message = result });
    else if (result == "Reservation already CheckedOut")
        return BadRequest(new { message = result });
     else if (result == "Prior to checking out, pay")
        return BadRequest(new { message = result });          
    return Ok(new { message = result });
}
     [HttpGet("GetGuestsIdName")]
    public async Task<IActionResult> GetGuests()
    {
        var guests = await _service.GetGuestsAsync();
        return Ok(guests);
    }

    [HttpGet("GetRoomsIdName")]
    public async Task<IActionResult> GetRooms()
    {
        var rooms = await _service.GetRoomsAsync();
        return Ok(rooms);
    }
}
