using tbb.users.api.Models;

namespace tbb.users.api.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
        Task CreateUserAsync(User user);
        Task<User> GetUserByResetTokenAsync(string token);
        Task UpdateUserAsync(User user);
    }
}
