using HotelManagement.context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RoomService : IRoomService
{
    private readonly AppDbContext _context;

    public RoomService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RoomModel>> GetAllRoomsAsync()
    {
        try
        {
            return await _context.Rooms.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving rooms: {ex.Message}", ex);
        }
    }

    public async Task<RoomModel?> GetRoomByIdAsync(int id)
    {
        try
        {
            return await _context.Rooms.FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving room with ID {id}: {ex.Message}", ex);
        }
    }

    public async Task<RoomModel> AddRoomAsync(RoomModel room)
    {
        try
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding room: {ex.Message}", ex);
        }
    }

    public async Task<RoomModel?> UpdateRoomAsync(int id, RoomModel room)
    {
        try
        {
            var existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom == null) return null;

            existingRoom.RoomNumber = room.RoomNumber;
            existingRoom.RoomType = room.RoomType;
            existingRoom.Price = room.Price;
            existingRoom.Status = room.Status;
            existingRoom.Capacity = room.Capacity;
            existingRoom.AC = room.AC;

            await _context.SaveChangesAsync();
            return existingRoom;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating room with ID {id}: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteRoomAsync(int id)
    {
        try
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting room with ID {id}: {ex.Message}", ex);
        }
    }
}
