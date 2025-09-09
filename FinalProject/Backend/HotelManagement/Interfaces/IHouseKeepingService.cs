using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs;
using Models;

public interface IHouseKeepingService
{
    Task<IEnumerable<HouseKeepingTaskModel>> GetAllTasksAsync();
    Task<HouseKeepingTaskModel?> GetTaskByIdAsync(int id);
    Task<HouseKeepingTaskModel> AddTaskAsync(AddHouseKeepingTaskDto dto);
    Task<HouseKeepingTaskModel?> UpdateTaskAsync(UpdateHouseKeepingTaskDto dto);
    Task<bool> DeleteTaskAsync(int id);
}
