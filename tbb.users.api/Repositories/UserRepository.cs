using Dapper;
using System.Data;
using System.Threading.Tasks;
using tbb.users.api.Interfaces;
using tbb.users.api.Models;

namespace tbb.users.api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            string query = "SELECT * FROM Users WHERE Email = @Email";
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            string query = "SELECT Password FROM Users WHERE Email = @Email";
            var hashedPassword = await _dbConnection.ExecuteScalarAsync<string>(query, new { Email = email });

            // Check if the password is correct
            return hashedPassword != null && BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public async Task CreateUserAsync(User user)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            string query = "INSERT INTO Users (Email, Password, RememberMe) VALUES (@Email, @Password, @RememberMe)";
            await _dbConnection.ExecuteAsync(query, new { user.Email, Password = hashedPassword, user.RememberMe });
        }

        public async Task<User> GetUserByResetTokenAsync(string token)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Email = @Email", new { Email = token });
        }

        public async Task UpdateUserAsync(User user)
        {
            string query = "UPDATE Users SET Password = @Password WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { user.Password, user.Id });
        }

        public async Task<User> AddUserAsync(User user)
        {
            var query = "INSERT INTO Users (FirstName, LastName, Email, Password, NewsletterSubscription) VALUES (@FirstName, @LastName, @Email, @Password, @NewsletterSubscription); SELECT CAST(SCOPE_IDENTITY() as int)";
            var userId = await _dbConnection.ExecuteScalarAsync<int>(query, user);
            user.Id = userId;
            return user;
        }
    }
}
