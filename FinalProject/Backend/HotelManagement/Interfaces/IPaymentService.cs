using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPaymentService
{
    Task<IEnumerable<PaymentDto>> GetAllPaymentsAsync();
    Task<PaymentDto?> GetPaymentByReservationIdAsync(int reservationId);
    Task<PaymentDto> AddPaymentAsync(AddPaymentDto paymentDto);
    Task<PaymentDto?> UpdatePaymentAsync(int reservationId, UpdatePaymentDto paymentDto);
    Task<bool> DeletePaymentAsync(int reservationId);
}
