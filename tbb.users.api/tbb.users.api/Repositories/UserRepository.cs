using System.Data;
using System.Threading.Tasks;
using Dapper;
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

        public async Task<User> AddUserAsync(User user)
        {
            var query = "INSERT INTO Users (FirstName, LastName, Email, Password, NewsletterSubscription) VALUES (@FirstName, @LastName, @Email, @Password, @NewsletterSubscription); SELECT CAST(SCOPE_IDENTITY() as int)";
            var userId = await _dbConnection.ExecuteScalarAsync<int>(query, user);
            user.Id = userId;
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var query = "SELECT * FROM Users WHERE Email = @Email";
            return await _dbConnection.QuerySingleOrDefaultAsync<User>(query, new { Email = email });
        }
    }
}
