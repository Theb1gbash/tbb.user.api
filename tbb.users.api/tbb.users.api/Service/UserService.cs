using System.Threading.Tasks;
using tbb.users.api.Interfaces;
using tbb.users.api.Models;

namespace tbb.users.api.Services
{
    public class UserService : IUserService
    {
        public async Task SendConfirmationEmailAsync(string email)
        {
            // Logic to send confirmation email
            await Task.CompletedTask; // Placeholder for actual email sending logic
        }

        public async Task StartUserSessionAsync(User user)
        {
            // Logic to start user session
            await Task.CompletedTask; // Placeholder for actual session start logic
        }
    }
}
