using DTOs;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReservationService
{
    Task<IEnumerable<ReservationReadDto>> GetAllReservationsAsync();
    Task<ReservationReadDto?> GetReservationByIdAsync(int id);
    Task<ReservationReadDto> AddReservationAsync(ReservationCreateDto dto);
    Task<ReservationReadDto?> UpdateReservationAsync(int id, ReservationUpdateDto dto);
    Task<string> CancelReservationAsync(int id);
    Task<string> CheckOutReservationAsync(int id);

     Task<IEnumerable<GuestSummaryDto>> GetGuestsAsync();
    Task<IEnumerable<RoomSummaryDto>> GetRoomsAsync();
}
