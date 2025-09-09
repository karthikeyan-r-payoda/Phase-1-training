using Dto;
using HotelManagement.context;
using HotelManagement.Security;
using Microsoft.EntityFrameworkCore;
using Models;


public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly AuthService _authService;

    public UserService(AppDbContext context, AuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public bool AddUser(AddUserDto userDto, out string message)
    {
        try
        {
            var userEntity = new UserModel
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = _authService.HashPass(userDto.Password),
                RoleId = userDto.RoleId,
                Phone = userDto.Phone,
                ShiftSchedule = userDto.ShiftSchedule,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(userEntity);
            _context.SaveChanges();

            message = "User added successfully.";
            return true;
        }
        catch (Exception ex)
        {
            message = $"Error while adding user: {ex.Message}";
            return false;
        }
    }

    public IEnumerable<object> GetUsers()
    {
        try
        {
            return _context.Users
                           .Select(x => new { x.UserId,x.Name, x.Email, x.Role.RoleName, x.ShiftSchedule })
                           .ToList();
        }
        catch
        {
            return Enumerable.Empty<object>();
        }
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role?.RoleName ?? string.Empty,
                ShiftSchedule = user.ShiftSchedule
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving user {userId}: {ex.Message}", ex);
        }
    }

    public async Task<UserDto?> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.Phone = updateUserDto.Phone;
            user.RoleId = updateUserDto.RoleId;
            user.ShiftSchedule = updateUserDto.ShiftSchedule;

            await _context.SaveChangesAsync();

            // Reload role info
            await _context.Entry(user).Reference(u => u.Role).LoadAsync();

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role?.RoleName ?? string.Empty,
                ShiftSchedule = user.ShiftSchedule
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating user {userId}: {ex.Message}", ex);
        }
    }
    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        try
        {
            return await _context.Roles
                .Select(r => new RoleDto
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving roles: {ex.Message}", ex);
        }
    }

public async Task<IEnumerable<UserSimpleDto>> GetUsersIdAndNameAsync()
{
    try
    {
        return await _context.Users
            .Select(u => new UserSimpleDto
            {
                StaffId = u.UserId,
                Name = u.Name
            })
            .ToListAsync();
    }
    catch (Exception ex)
    {
        throw new Exception($"Error retrieving users: {ex.Message}", ex);
    }
}


}

