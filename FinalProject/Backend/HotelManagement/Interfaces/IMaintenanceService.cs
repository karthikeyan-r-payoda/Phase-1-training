using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMaintenanceService
{
    Task<IEnumerable<MaintenanceRequestDto>> GetAllRequestsAsync();
    Task<MaintenanceRequestDto?> GetRequestByIdAsync(int requestId);
    Task<MaintenanceRequestDto> AddRequestAsync(AddMaintenanceRequestDto dto);
    Task<MaintenanceRequestDto?> UpdateRequestAsync(UpdateMaintenanceRequestDto dto);
    Task<bool> DeleteRequestAsync(int requestId);
}
