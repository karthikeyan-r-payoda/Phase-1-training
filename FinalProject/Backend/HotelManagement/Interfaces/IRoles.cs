using Models;

public interface IRoles
{
    Task<RoleModel> CreateRoleAsync(RoleModel role);
    Task<IEnumerable<RoleModel>> GetRolesAsync();
}
