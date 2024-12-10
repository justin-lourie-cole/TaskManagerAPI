using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TaskManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
  private readonly IConfiguration _configuration;

  public AuthController(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  [HttpPost("login")]
  public IActionResult Login([FromBody] LoginRequest request)
  {
    // For demo purposes - replace with real authentication
    if (request.Username == "user" && request.Password == "password")
    {
      var token = GenerateJwtToken(request.Username, "User", 1);
      return Ok(new { token });
    }

    return Unauthorized();
  }

  private string GenerateJwtToken(string username, string role, int userId)
  {
    var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[]
        {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role),
                new Claim("UserId", userId.ToString())
            }),
      Expires = DateTime.UtcNow.AddDays(7),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}

public class LoginRequest
{
  public string Username { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
}
