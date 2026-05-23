using ProxiWorkAPI.Models;

namespace ProxiWorkAPI.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(string fullName, string phoneNumber,
                              string password, UserType userType,
                              double lat, double lng);

        Task<string> Login(string phoneNumber, string password,
                           double lat, double lng);
    }
}