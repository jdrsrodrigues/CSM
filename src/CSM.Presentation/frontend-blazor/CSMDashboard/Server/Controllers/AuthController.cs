using CSMDashboard.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // Validação simplificada
        if (model.Email == "admin@csm.com" && model.Password == "123456")
        {
            var token = GerarJwt(model.Email);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    private string GerarJwt(string email)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua-chave-secreta-aqui"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "CSMDashboard",
            audience: "CSMDashboard",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
