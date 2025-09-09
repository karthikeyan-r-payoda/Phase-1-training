using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoles _roleService;

    public RoleController(IRoles roleService)
    {
        _roleService = roleService;
    }

    [HttpPost("AddRole")]
    public async Task<IActionResult> CreateRole([FromBody] RoleModel role)
    {
        if (role == null)
            return BadRequest("Invalid role data.");

        var createdRole = await _roleService.CreateRoleAsync(role);
        return Ok(createdRole);
    }

    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetRolesAsync();
        return Ok(roles);
    }
}
