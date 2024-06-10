using System.Threading.Tasks;
using tbb.users.api.Models;

namespace tbb.users.api.Interfaces
{
    public interface IUserService
    {
        Task<(bool IsSuccess, string ErrorMessage)> RegisterUserAsync(RegistrationRequest request);
        Task SendConfirmationEmailAsync(string email);
        Task StartUserSessionAsync(User user);
    }
}
