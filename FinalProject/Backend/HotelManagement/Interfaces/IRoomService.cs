using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRoomService
{
    Task<IEnumerable<RoomModel>> GetAllRoomsAsync();
    Task<RoomModel?> GetRoomByIdAsync(int id);
    Task<RoomModel> AddRoomAsync(RoomModel room);
    Task<RoomModel?> UpdateRoomAsync(int id, RoomModel room);
    Task<bool> DeleteRoomAsync(int id);
}
