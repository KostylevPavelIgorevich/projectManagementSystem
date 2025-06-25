using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using projectManagementSystem.Domain.Entities;
using System.Data;

namespace projectManagementSystem.Repositories.LogsEntrys
{
    public class LogRepositories : ILogRepositories
    {
        private readonly string _connectionString;

        public LogRepositories(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddLog(Log log, CancellationToken cancellationToken)
        {
            string sqlExpression =
                """
                INSERT INTO logs (id, task_id, user_id, message, createdAt)
                VALUES (@id, @taskId, @userId, @message, @createdAt)
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new
                {
                    log.Id,
                    log.TaskId,
                    log.UserId,
                    log.Message,
                    log.CreatedAt
                }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }

        public async Task<IEnumerable<Log>> GetLogsByTaskId(int taskId, CancellationToken cancellationToken)
        {
            string sqlExpression =
                """
                SELECT id, task_id, user_id, message, createdAt
                FROM logs 
                WHERE task_id = @taskId 
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { taskId }, cancellationToken: cancellationToken);
                var result = await connectionDb.QueryAsync<Log>(command);
                return result;
            }
        }

        public async Task DeleteLogById(int logId, CancellationToken cancellationToken)
        {
            string sqlExpression = 
                """
                DELETE FROM logs 
                WHERE id = @logId
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { logId }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }
    }

}

