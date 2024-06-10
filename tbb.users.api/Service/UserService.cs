using tbb.users.api.Interfaces;
using tbb.users.api.Models;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;

namespace tbb.users.api.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IUserRepository userRepository, IEmailService emailService, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUserAsync(RegistrationRequest request)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return (false, "Email is already in use.");
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password), // Hash the password
                NewsletterSubscription = request.NewsletterSubscription,
                UserType = request.UserType,
                OrganizationName = request.OrganizationName,
                ContactNumber = request.ContactNumber
            };

            var addedUser = await _userRepository.AddUserAsync(user);
            if (addedUser == null)
            {
                return (false, "Failed to register user.");
            }

            await SendConfirmationEmailAsync(user.Email);
            await StartUserSessionAsync(user);

            return (true, null);
        }

        public async Task SendConfirmationEmailAsync(string email)
        {
            string subject = "Welcome to Our Application!";
            string body = "Thank you for registering. Please click the link below to confirm your email address.";
            await _emailService.SendEmailAsync(email, subject, body);
        }

        public async Task StartUserSessionAsync(User user)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                httpContext.Session.SetString("UserEmail", user.Email);
            }

            await Task.CompletedTask; // Placeholder for actual session start logic
        }
    }
}
