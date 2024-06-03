using tbb.users.api.Models;

namespace tbb.users.api.Interfaces
{
    public interface IUserService
    {
        Task SendConfirmationEmailAsync(string email);
        Task StartUserSessionAsync(User user);
    }
}

