using Dto;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(AddUserDto user)
    {
        if (_userService.AddUser(user, out string message))
        {
            return Ok(message);
        }
        else
        {
            return BadRequest(message);
        }
    }

    [HttpGet("GetUsers")]
    public IActionResult GetUsers()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpGet("GetUserById/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound($"User with ID {id} not found.");
        return Ok(user);
    }

    [HttpPut("UpdateUser/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
        if (updatedUser == null) return NotFound($"User with ID {id} not found.");

        return Ok(updatedUser);
    }

    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _userService.GetAllRolesAsync();
        return Ok(roles);
    }

[HttpGet("StaffIdName")]
public async Task<IActionResult> GetUsersIdAndName()
{
    var users = await _userService.GetUsersIdAndNameAsync();
    return Ok(users);
}

}

