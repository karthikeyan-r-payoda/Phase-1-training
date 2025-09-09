using DTOs;
using HotelManagement.context;
using Microsoft.EntityFrameworkCore;
using Models;

public class GuestService : IGuestService
{
    private readonly AppDbContext _context;

    public GuestService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GuestDto>> GetAllGuestsAsync()
    {
        try
        {
            return await _context.Guests
                .Select(g => new GuestDto
                {
                    GuestId = g.GuestId,
                    FullName = g.FirstName + " " + g.LastName,
                    Email = g.Email,
                    Phone = g.Phone,
                    Address = g.Address,
                    Preferences = g.Preferences
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving guests: {ex.Message}", ex);
        }
    }

    public async Task<GuestDto?> GetGuestByIdAsync(int guestId)
    {
        try
        {
            var guest = await _context.Guests.FindAsync(guestId);
            if (guest == null) return null;

            return new GuestDto
            {
                GuestId = guest.GuestId,
                FullName = guest.FirstName + " " + guest.LastName,
                Email = guest.Email,
                Phone = guest.Phone,
                Address = guest.Address,
                Preferences = guest.Preferences
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving guest with ID {guestId}: {ex.Message}", ex);
        }
    }

    public async Task<GuestDto> AddGuestAsync(AddGuestDto guestDto)
    {
        try
        {
            var guest = new GuestModel
            {
                FirstName = guestDto.FirstName,
                LastName = guestDto.LastName,
                Email = guestDto.Email,
                Phone = guestDto.Phone,
                Address = guestDto.Address,
                Preferences = guestDto.Preferences
            };

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();

            return new GuestDto
            {
                GuestId = guest.GuestId,
                FullName = guest.FirstName + " " + guest.LastName,
                Email = guest.Email,
                Phone = guest.Phone,
                Address = guest.Address,
                Preferences = guest.Preferences
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding guest: {ex.Message}", ex);
        }
    }

    public async Task<GuestDto?> UpdateGuestAsync(int guestId, UpdateGuestDto guestDto)
    {
        try
        {
            var guest = await _context.Guests.FindAsync(guestId);
            if (guest == null) return null;

            guest.FirstName = guestDto.FirstName;
            guest.LastName = guestDto.LastName;
            guest.Email = guestDto.Email;
            guest.Phone = guestDto.Phone;
            guest.Address = guestDto.Address;
            guest.Preferences = guestDto.Preferences;

            await _context.SaveChangesAsync();

            return new GuestDto
            {
                GuestId = guest.GuestId,
                FullName = guest.FirstName + " " + guest.LastName,
                Email = guest.Email,
                Phone = guest.Phone,
                Address = guest.Address,
                Preferences = guest.Preferences
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating guest {guestId}: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteGuestAsync(int guestId)
    {
        try
        {
            var guest = await _context.Guests.FindAsync(guestId);
            if (guest == null) return false;

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting guest {guestId}: {ex.Message}", ex);
        }
    }
}
