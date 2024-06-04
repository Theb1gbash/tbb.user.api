using tbb.users.api.Models;

namespace tbb.users.api.Providers
{
    public interface IUserProvider
    {
        Task<bool> ValidateUserAsync(string email, string password);
        Task CreateUserAsync(User user);
        Task<User> GetUserByResetTokenAsync(string token);
        Task UpdateUserAsync(User user);
    }
}
