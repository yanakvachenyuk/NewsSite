using NewsSite.Models;

namespace NewsSite.Services.Interfaces
{
    public interface IAuthService
    {
        bool ValidateUser(string email, string password);
    }
}