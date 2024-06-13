using System.Threading.Tasks;

namespace tbb.users.api.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
