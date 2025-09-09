using Models;

public interface IAuthService
{
    Task<UserModel?> ValidateUserAsync(string username, string password);
}