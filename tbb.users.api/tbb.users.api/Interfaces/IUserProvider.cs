using tbb.users.api.Models;

namespace tbb.users.api.Interfaces
{
    public interface IUserProvider
    {
        Task<bool> RegisterUserAsync(RegistrationRequest request);
    }
}
