using VehiculeLocation.Backend.Models;

namespace VehiculeLocation.Backend.Services
{
    public interface IUserService
    {
        Task<User> GetUser(int id);
    }
}
