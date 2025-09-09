using DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await _paymentService.GetAllPaymentsAsync();
        return Ok(payments);
    }

    [HttpGet("{reservationId}")]
    public async Task<IActionResult> GetPaymentByReservationId(int reservationId)
    {
        var payment = await _paymentService.GetPaymentByReservationIdAsync(reservationId);
        if (payment == null) return NotFound($"Payment not found for reservation {reservationId}");
        return Ok(payment);
    }

    [HttpPut]
    public async Task<IActionResult> AddPayment([FromBody] AddPaymentDto paymentDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var payment = await _paymentService.AddPaymentAsync(paymentDto);
        return CreatedAtAction(nameof(GetPaymentByReservationId), new { reservationId = payment.ReservationId }, payment);
    }

    [HttpPut("{reservationId}")]
    public async Task<IActionResult> UpdatePayment(int reservationId, [FromBody] UpdatePaymentDto paymentDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updatedPayment = await _paymentService.UpdatePaymentAsync(reservationId, paymentDto);
        if (updatedPayment == null) return NotFound($"Payment not found for reservation {reservationId}");

        return Ok(updatedPayment);
    }

    [HttpDelete("{reservationId}")]
    public async Task<IActionResult> DeletePayment(int reservationId)
    {
        var deleted = await _paymentService.DeletePaymentAsync(reservationId);
        if (!deleted) return NotFound($"Payment not found for reservation {reservationId}");

        return NoContent();
    }
}
