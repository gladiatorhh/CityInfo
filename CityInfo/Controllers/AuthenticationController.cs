using CityInfo.Models;
using CityInfo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CityInfo.Controllers;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthenticationController(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    [HttpPost("authenticate")]
    public async Task<ActionResult<string>> AuthenticateUser(AuthenticaitonUserDto userForAuthenticaiton)
    {
        var user = await _userRepository.GetUserByUserNameAndPassworodAsync(userForAuthenticaiton.UserName, userForAuthenticaiton.Password);

        if (user == null)
            return Unauthorized();

        var secret = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
            );

        var signingCredintials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("Id",user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.Name),
        };

        var securityTokenDescription = new JwtSecurityToken(
            _configuration["Authentication:Issuer"],
            _configuration["Authentication:Audience"],
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            signingCredintials
            );

        var token = new JwtSecurityTokenHandler()
            .WriteToken(securityTokenDescription);

        return Ok(token);
    }
}
