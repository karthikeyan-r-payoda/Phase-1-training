using DTOs;
using HotelManagement.context;
using Microsoft.EntityFrameworkCore;
using Models;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync()
    {
        try
        {
            return await _context.Payments
                .Select(p => new PaymentDto
                {
                    ReservationId = p.ReservationId,
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    PaymentMethod = p.PaymentMethod,
                    PaymentStatus = p.PaymentStatus
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching payments: {ex.Message}", ex);
        }
    }

    public async Task<PaymentDto?> GetPaymentByReservationIdAsync(int reservationId)
    {
        try
        {
            var payment = await _context.Payments.FindAsync(reservationId);
            if (payment == null) return null;

            return new PaymentDto
            {
                ReservationId = payment.ReservationId,
                Amount = payment.Amount,
                PaymentDate = payment.PaymentDate,
                PaymentMethod = payment.PaymentMethod,
                PaymentStatus = payment.PaymentStatus
            };
         
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching payment for reservation {reservationId}: {ex.Message}", ex);
        }
    }

   public async Task<PaymentDto> AddPaymentAsync(AddPaymentDto paymentDto)
{
    try
    {
        // 🔹 Check if reservation exists
        var reservation = await _context.Reservations.FindAsync(paymentDto.ReservationId);
        if (reservation == null)
            throw new Exception($"Reservation {paymentDto.ReservationId} not found.");

        var payment = new PaymentModel
        {
            ReservationId = paymentDto.ReservationId,
            Amount = paymentDto.Amount,
            PaymentMethod = paymentDto.PaymentMethod,
            PaymentDate = IndianTime.GetIndianTime(),
            PaymentStatus = "Completed" 
        };

        _context.Payments.Add(payment);

        reservation.PaymentStatus = "Paid";

        await _context.SaveChangesAsync();

        return new PaymentDto
        {
            ReservationId = payment.ReservationId,
            Amount = payment.Amount,
            PaymentDate = payment.PaymentDate,
            PaymentMethod = payment.PaymentMethod,
            PaymentStatus = payment.PaymentStatus
        };
    }
    catch (Exception ex)
    {
        throw new Exception($"Error adding payment: {ex.Message}", ex);
    }
}

public async Task<PaymentDto?> UpdatePaymentAsync(int reservationId, UpdatePaymentDto paymentDto)
{
    try
    {
        var existingPayment = await _context.Payments.FindAsync(reservationId);
        if (existingPayment == null) return null;

        var reservation = await _context.Reservations.FindAsync(reservationId);
        if (reservation == null) throw new Exception($"Reservation {reservationId} not found.");

        existingPayment.Amount = paymentDto.Amount;
        existingPayment.PaymentMethod = paymentDto.PaymentMethod;
        existingPayment.PaymentStatus = paymentDto.PaymentStatus;

        if (paymentDto.PaymentStatus == "Completed")
            reservation.PaymentStatus = "Paid";
        else if (paymentDto.PaymentStatus == "Failed")
            reservation.PaymentStatus = "Unpaid";
        else
            reservation.PaymentStatus = "Pending";

        await _context.SaveChangesAsync();

        return new PaymentDto
        {
            ReservationId = existingPayment.ReservationId,
            Amount = existingPayment.Amount,
            PaymentDate = existingPayment.PaymentDate,
            PaymentMethod = existingPayment.PaymentMethod,
            PaymentStatus = existingPayment.PaymentStatus
        };
    }
    catch (Exception ex)
    {
        throw new Exception($"Error updating payment for reservation {reservationId}: {ex.Message}", ex);
    }
}


    public async Task<bool> DeletePaymentAsync(int reservationId)
    {
        try
        {
            var payment = await _context.Payments.FindAsync(reservationId);
            if (payment == null) return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting payment for reservation {reservationId}: {ex.Message}", ex);
        }
    }
}
