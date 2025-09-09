using DTOs;
using HotelManagement.context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HouseKeepingService : IHouseKeepingService
{
    private readonly AppDbContext _context;

    public HouseKeepingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<HouseKeepingTaskModel>> GetAllTasksAsync()
    {
        try
        {
            return await _context.HouseKeepingTasks
                .Include(t => t.Room)
                .Include(t => t.AssignedToUser)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching housekeeping tasks. " + ex.Message, ex);
        }
    }

    public async Task<HouseKeepingTaskModel?> GetTaskByIdAsync(int id)
    {
        try
        {
            return await _context.HouseKeepingTasks
                .Include(t => t.Room)
                .Include(t => t.AssignedToUser)
                .FirstOrDefaultAsync(t => t.TaskId == id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching task with ID {id}. " + ex.Message, ex);
        }
    }

    public async Task<HouseKeepingTaskModel> AddTaskAsync(AddHouseKeepingTaskDto dto)
    {
        try
        {
            var task = new HouseKeepingTaskModel
            {
                RoomId = dto.RoomId,
                TaskDescription = dto.TaskDescription,
                AssignedToUserId = dto.AssignedToUserId,
                Status = "Pending",
                AssignedDate = DateTime.Now
            };

            _context.HouseKeepingTasks.Add(task);


            var room = await _context.Rooms.FindAsync(dto.RoomId);
            if (room != null)
            {
                room.Status = "Cleaning";
            }

            await _context.SaveChangesAsync();
            return task;
        }
        catch (Exception ex)
        {
            throw new Exception("Error adding housekeeping task. " + ex.Message, ex);
        }
    }

    public async Task<HouseKeepingTaskModel?> UpdateTaskAsync(UpdateHouseKeepingTaskDto dto)
    {
        try
        {
            var task = await _context.HouseKeepingTasks.FindAsync(dto.TaskId);
            if (task == null) return null;

            task.Status = dto.Status;
            task.AssignedToUserId = dto.AssignedToUserId ?? task.AssignedToUserId;

            if (dto.Status == "Completed" && dto.CompletedDate.HasValue)
                task.CompletedDate = dto.CompletedDate;

         
            var room = await _context.Rooms.FindAsync(task.RoomId);
            if (room != null)
            {
                if (task.Status == "Completed")
                {
                    room.Status = "Available";

               
                    var reservations = _context.Reservations
                        .Where(r => r.RoomId == room.RoomId && r.ReservationStatus == "Pending")
                        .ToList();

                    foreach (var reservation in reservations)
                    {
                        reservation.ReservationStatus = "ReadyForCheckIn";
                    }
                }
                else if (task.Status == "InProgress")
                {
                    room.Status = "Cleaning";
                }
            }

            await _context.SaveChangesAsync();
            return task;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating housekeeping task with ID {dto.TaskId}. " + ex.Message, ex);
        }
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        try
        {
            var task = await _context.HouseKeepingTasks.FindAsync(id);
            if (task == null) return false;

            _context.HouseKeepingTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting housekeeping task with ID {id}. " + ex.Message, ex);
        }
    }
}
