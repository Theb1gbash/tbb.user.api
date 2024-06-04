using tbb.users.api.Models;
using tbb.users.api.Repositories;

namespace tbb.users.api.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;

        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ValidateUserAsync(string email, string password)
        {
            return await _userRepository.ValidateUserCredentialsAsync(email, password);
        }

        public async Task CreateUserAsync(User user)
        {
            await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> GetUserByResetTokenAsync(string token)
        {
            return await _userRepository.GetUserByResetTokenAsync(token);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }
    }
}
