using Dapper;
using Microsoft.Data.SqlClient;
using Npgsql;
using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.ProjectsTasks;
using System.Data;
using System.Data.Common;

namespace projectManagementSystem.Repositories.Users
{
    public class UserRepositories : IUserRepositories
    {
        private readonly string _connectionString;

        public UserRepositories(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<int> AddUser(User user, CancellationToken cancellationToken)
        {
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                string sqlExpression =
                """
                INSERT INTO users (name, password, role, login) 
                VALUES (@name, @password, @role, @login)
                RETURNING id;
                """;

                var command = new CommandDefinition(sqlExpression, new
                {
                    name = user.Name,
                    password = user.Password,
                    role = user.Role,
                    login = user.Login
                }, cancellationToken: cancellationToken);

                // Получаем id нового пользователя от базы
                return await connectionDb.ExecuteScalarAsync<int>(command);
            }
        }

        public async Task<User> GetUserByLogin(string login, CancellationToken cancellationToken)
        {
            string sqlExpression = 
                """
                SELECT id, name, password, role, login 
                FROM users 
                WHERE login = @login
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { login }, cancellationToken: cancellationToken);
                var result = await connectionDb.QueryFirstOrDefaultAsync<User>(command);
                return result;
            }
        }

        public async Task DeleteUser(string userName, CancellationToken cancellationToken)
        {
            string sqlExpression =
                """
                DELETE FROM users 
                WHERE name = @userName
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { userName }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }

        public async Task UpdateUser(int id, string name, string role, CancellationToken cancellationToken)
        {
            string sqlExpression =
                """
                UPDATE users 
                SET name = @name, role = @role 
                WHERE id = @id
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new
                {
                    Id = id,
                    Name = name,
                    Role = role,
                }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }
        public async Task<User> GetUserByName(string name, CancellationToken cancellationToken)
        {
            string sqlExpression =
                """
                SELECT id, name, password, role, login 
                FROM users 
                WHERE name = @name
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { name }, cancellationToken: cancellationToken);
                var result = await connectionDb.QueryFirstOrDefaultAsync<User>(command);
                return result;
            }
        }
        public async Task UpdateLoginPasswordById(int id, string login, string password, CancellationToken cancellationToken)
        {
            string sqlExpression =
                """
                UPDATE users 
                SET password = @Password, login = @Login 
                WHERE id = @Id
                """;

            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new
                {
                    id = id,
                    login = login,
                    password = password
                }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }
    }
}

