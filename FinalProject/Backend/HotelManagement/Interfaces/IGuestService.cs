using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IGuestService
{
    Task<IEnumerable<GuestDto>> GetAllGuestsAsync();
    Task<GuestDto?> GetGuestByIdAsync(int guestId);
    Task<GuestDto> AddGuestAsync(AddGuestDto guestDto);
    Task<GuestDto?> UpdateGuestAsync(int guestId, UpdateGuestDto guestDto);
    Task<bool> DeleteGuestAsync(int guestId);
}
