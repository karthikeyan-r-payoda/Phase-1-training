using HotelManagement.context;
using Microsoft.EntityFrameworkCore;
using Models;

public class RoleService : IRoles
{
    private readonly AppDbContext _context;

    public RoleService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RoleModel> CreateRoleAsync(RoleModel role)
    {
        try
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating role: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<RoleModel>> GetRolesAsync()
    {
        try
        {
            return await _context.Roles.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving roles: {ex.Message}", ex);
        }
    }
}
