using Microsoft.AspNetCore.Mvc;
using ProxiWorkAPI.Interfaces;
using ProxiWorkAPI.Models;

namespace ProxiWorkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var token = await _auth.Register(
                    dto.FullName, dto.PhoneNumber,
                    dto.Password, dto.UserType,
                    dto.Latitude, dto.Longitude);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _auth.Login(
                    dto.PhoneNumber, dto.Password,
                    dto.Latitude, dto.Longitude);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }

    public record RegisterDto(
        string FullName,
        string PhoneNumber,
        string Password,
        UserType UserType,
        double Latitude,
        double Longitude);

    public record LoginDto(
        string PhoneNumber,
        string Password,
        double Latitude,
        double Longitude);
}