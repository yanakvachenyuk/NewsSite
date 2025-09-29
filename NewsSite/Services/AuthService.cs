using NewsSite.Data;
using NewsSite.Services.Interfaces;

namespace NewsSite.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool ValidateUser(string email, string password)
        {
            var user = _context.AdminUsers.FirstOrDefault(u => u.Email == email);
            if (user == null) return false;

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
    }
}