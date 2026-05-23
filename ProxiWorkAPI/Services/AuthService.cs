using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProxiWorkAPI.Data;
using ProxiWorkAPI.Interfaces;
using ProxiWorkAPI.Models;

namespace ProxiWorkAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public AuthService(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string> Register(
            string fullName, string phoneNumber,
            string password, UserType userType,
            double lat, double lng)
        {
            if (await _context.Users.AnyAsync(
                    u => u.PhoneNumber == phoneNumber))
                throw new Exception("رقم الموبايل مسجل قبل كده");

            var user = new User
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                UserType = userType,
                Latitude = lat,
                Longitude = lng,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return GenerateToken(user);
        }

        public async Task<string> Login(
            string phoneNumber, string password,
            double lat, double lng)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber)
                ?? throw new Exception("رقم الموبايل أو كلمة السر غلط");

            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new Exception("رقم الموبايل أو كلمة السر غلط");

            user.Latitude = lat;
            user.Longitude = lng;
            user.LastSeenAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("fullName", user.FullName),
                new Claim("userType", user.UserType.ToString()),
                new Claim("phone", user.PhoneNumber)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}