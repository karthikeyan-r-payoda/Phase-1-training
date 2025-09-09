using System.Security.Cryptography;
using System.Text;
using HotelManagement.context;
using Microsoft.EntityFrameworkCore;
using Models;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel?> ValidateUserAsync(string username, string password)
    {
        return await _context.Users
        .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == username && u.Password == HashPass(password));
    }

    
    public string HashPass(string password)
    {

        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] pass_convertor = Encoding.UTF8.GetBytes(password);
            byte[] bytes = sha256Hash.ComputeHash(pass_convertor);

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

    }
}
