using VehiculeLocation.Backend.Data;
using VehiculeLocation.Backend.Models;

namespace VehiculeLocation.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int id)
        {
            // La logique est maintenant ici
            return await _context.User.FindAsync(id);
        }
    }
}
