using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCrypt.Net;
using tbb.users.api.Interfaces;
using tbb.users.api.Models;
using Microsoft.Extensions.Logging;

namespace tbb.users.api.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ILogger<UserProvider> _logger;

        public UserProvider(IUserRepository userRepository, IUserService userService, ILogger<UserProvider> logger)
        {
            _userRepository = userRepository;
            _userService = userService;
            _logger = logger;
        }

        public async Task<bool> RegisterUserAsync(RegistrationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FirstName) || request.FirstName.Length < 2)
            {
                _logger.LogError("Invalid FirstName: {FirstName}", request.FirstName);
                return false;
            }
            if (string.IsNullOrWhiteSpace(request.LastName) || request.LastName.Length < 2)
            {
                _logger.LogError("Invalid LastName: {LastName}", request.LastName);
                return false;
            }
            if (string.IsNullOrWhiteSpace(request.Email) || !IsValidEmail(request.Email))
            {
                _logger.LogError("Invalid Email: {Email}", request.Email);
                return false;
            }
            if (string.IsNullOrWhiteSpace(request.Password) || !IsValidPassword(request.Password))
            {
                _logger.LogError("Invalid Password: {Password}", request.Password);
                return false;
            }
            if (request.Password != request.ConfirmPassword)
            {
                _logger.LogError("Passwords do not match: {Password} != {ConfirmPassword}", request.Password, request.ConfirmPassword);
                return false;
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            if (existingUser != null)
            {
                _logger.LogError("Email already exists: {Email}", request.Email);
                return false;
            }

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = HashPassword(request.Password),
                NewsletterSubscription = request.NewsletterSubscription
            };

            var addedUser = await _userRepository.AddUserAsync(user);
            if (addedUser == null)
            {
                _logger.LogError("Error adding user to the database");
                return false;
            }

            await _userService.SendConfirmationEmailAsync(user.Email);
            await _userService.StartUserSessionAsync(user);

            return true;
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        private bool IsValidPassword(string password)
        {
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasNumber = new Regex(@"[0-9]+");
            var hasSpecialChar = new Regex(@"[\W]+");
            return password.Length >= 8 && hasUpperChar.IsMatch(password) && hasLowerChar.IsMatch(password) && hasNumber.IsMatch(password) && hasSpecialChar.IsMatch(password);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
