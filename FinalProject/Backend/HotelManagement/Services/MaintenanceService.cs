
using HotelManagement.context;
using Microsoft.EntityFrameworkCore;

public class MaintenanceService : IMaintenanceService
{
    private readonly AppDbContext _context;

    public MaintenanceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceRequestDto>> GetAllRequestsAsync()
    {
        try
        {
            return await _context.MaintenanceRequests
                .Include(r => r.Room)
                .Include(r => r.AssignedToUser)
                .Select(r => new MaintenanceRequestDto
                {
                    MaintenanceRequestId = r.MaintenanceRequestId,
                    RoomId = r.RoomId,
                    Description = r.Description,
                    Status = r.Status,
                    AssignedToUserId = r.AssignedToUserId,
                    CreatedAt = r.CreatedAt,
                    CompletedAt = r.CompletedAt,
                    RoomNumber = r.Room.RoomNumber,
                    AssignedToUserName = r.AssignedToUser != null ? r.AssignedToUser.Name : null
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving maintenance requests: {ex.Message}", ex);
        }
    }

    public async Task<MaintenanceRequestDto?> GetRequestByIdAsync(int requestId)
    {
        try
        {
            var r = await _context.MaintenanceRequests
                .Include(rq => rq.Room)
                .Include(rq => rq.AssignedToUser)
                .FirstOrDefaultAsync(rq => rq.MaintenanceRequestId == requestId);

            if (r == null) return null;

            return new MaintenanceRequestDto
            {
                MaintenanceRequestId = r.MaintenanceRequestId,
                RoomId = r.RoomId,
                Description = r.Description,
                Status = r.Status,
                AssignedToUserId = r.AssignedToUserId,
                CreatedAt = r.CreatedAt,
                CompletedAt = r.CompletedAt,
                RoomNumber = r.Room.RoomNumber,
                AssignedToUserName = r.AssignedToUser != null ? r.AssignedToUser.Name : null
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving maintenance request {requestId}: {ex.Message}", ex);
        }
    }

    public async Task<MaintenanceRequestDto> AddRequestAsync(AddMaintenanceRequestDto dto)
    {
        try
        {
            var request = new MaintenanceRequestModel
            {
                RoomId = dto.RoomId,
                Description = dto.Description,
                AssignedToUserId = dto.AssignedToUserId,
                Status = "Pending",
                CreatedAt = DateTime.Now
            };

            _context.MaintenanceRequests.Add(request);

            var room = await _context.Rooms.FindAsync(dto.RoomId);
            if (room != null)
            {
                room.Status = "Maintenance";
            }

            await _context.SaveChangesAsync();

            return new MaintenanceRequestDto
            {
                MaintenanceRequestId = request.MaintenanceRequestId,
                RoomId = request.RoomId,
                Description = request.Description,
                Status = request.Status,
                AssignedToUserId = request.AssignedToUserId,
                CreatedAt = request.CreatedAt,
                RoomNumber = room?.RoomNumber
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding maintenance request: {ex.Message}", ex);
        }
    }

    public async Task<MaintenanceRequestDto?> UpdateRequestAsync(UpdateMaintenanceRequestDto dto)
    {
        try
        {
            var request = await _context.MaintenanceRequests.FindAsync(dto.MaintenanceRequestId);
            if (request == null) return null;

            request.Status = dto.Status;
            request.AssignedToUserId = dto.AssignedToUserId ?? request.AssignedToUserId;

            if (dto.Status == "Completed" && dto.CompletedAt.HasValue)
                request.CompletedAt = dto.CompletedAt;

            var room = await _context.Rooms.FindAsync(request.RoomId);
            if (room != null)
            {
                if (dto.Status == "Completed")
                    room.Status = "Available";
                else if (dto.Status == "InProgress")
                    room.Status = "Maintenance";
            }

            await _context.SaveChangesAsync();

            return new MaintenanceRequestDto
            {
                MaintenanceRequestId = request.MaintenanceRequestId,
                RoomId = request.RoomId,
                Description = request.Description,
                Status = request.Status,
                AssignedToUserId = request.AssignedToUserId,
                CreatedAt = request.CreatedAt,
                CompletedAt = request.CompletedAt,
                RoomNumber = room?.RoomNumber
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating maintenance request {dto.MaintenanceRequestId}: {ex.Message}", ex);
        }
    }

    public async Task<bool> DeleteRequestAsync(int requestId)
    {
        try
        {
            var request = await _context.MaintenanceRequests.FindAsync(requestId);
            if (request == null) return false;

            _context.MaintenanceRequests.Remove(request);

            var room = await _context.Rooms.FindAsync(request.RoomId);
            if (room != null && request.Status != "Completed")
                room.Status = "Available";

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting maintenance request {requestId}: {ex.Message}", ex);
        }
    }
}
