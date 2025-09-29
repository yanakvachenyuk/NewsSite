using NewsSite.Models;
using NewsSite.Data;
using BCrypt.Net;

namespace NewsSite.Services
{
    public class AdminInitializer
    {
        private readonly ApplicationDbContext _context;

        public AdminInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            if (!_context.AdminUsers.Any())
            {
                var adminUser = new AdminUser
                {
                    Email = "admin@news.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    CreatedAt = DateTime.UtcNow
                };

                _context.AdminUsers.Add(adminUser);
                _context.SaveChanges();

                Console.WriteLine("Admin user created!");
                Console.WriteLine("Email: admin@news.com");
                Console.WriteLine("Password: admin123");
            }
        }
    }
}