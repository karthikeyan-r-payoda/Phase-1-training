using Dto;


public interface IUserService
{
    bool AddUser(AddUserDto userDto, out string message);
    IEnumerable<object> GetUsers();

    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<UserDto?> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
     Task<IEnumerable<UserSimpleDto>> GetUsersIdAndNameAsync();
    }

