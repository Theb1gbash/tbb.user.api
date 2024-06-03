using tbb.users.api.Models;

namespace tbb.users.api.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);
    }
}
