
using Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthService _authService;

    public AuthController(IConfiguration config, IAuthService authService)
    {
        _config = config;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var user = await _authService.ValidateUserAsync(login.UserName, login.Password);

        if (user == null)
            return Unauthorized("Invalid credentials");

        var token = GenerateJwtToken(user);
        var Role = user.Role.RoleName;
        var userId = user.UserId;
        return Ok(new { Token = token, Role,userId });
    }

    private string GenerateJwtToken(UserModel user)
    {
        var jwtSettings = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),//
        new Claim("UserId", user.UserId.ToString()),
        new Claim("RoleId", user.RoleId.ToString())
        };

    if (user.Role != null) 
    {
        claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));
    }


        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

