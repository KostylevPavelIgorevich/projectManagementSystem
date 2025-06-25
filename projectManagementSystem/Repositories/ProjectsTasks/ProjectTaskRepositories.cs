using Dapper;
using Npgsql;
using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.LogsEntrys;
using System.Data;

namespace projectManagementSystem.Repositories.ProjectsTasks
{
    public class ProjectTaskRepositories : IProjectTaskRepositories
    {
        private readonly string _connectionString;

        public ProjectTaskRepositories(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddProjectTask(ProjectTask task, CancellationToken cancellationToken)
        {
            string sqlExpression = 
                """
                INSERT INTO project_tasks (id, title, description, status, assigneduserid)  
                VALUES (@id, @title, @description, @status, @assignedUserId)
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new
                {
                    id = task.Id,
                    title = task.Title,
                    description = task.Description,
                    status = task.Status,
                    assignedUserId = task.Assigneduserid
                }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }
        public async Task<ProjectTask> GetProjectTaskByTaskId(int id, CancellationToken cancellationToken)
        {
            string sqlExpression =
                """
                SELECT id, title, description, status, assigneduserid 
                FROM project_tasks 
                WHERE id = @id
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { id }, cancellationToken: cancellationToken);
                var result = await connectionDb.QueryFirstOrDefaultAsync<ProjectTask>(command);
                return result;
            }
        }
        public async Task<IEnumerable<ProjectTask>> GetProjectTaskByAssignedUserId(int id, CancellationToken cancellationToken)
        {
            string sqlExpression =
                 """
                 SELECT id, title, description, status, assigneduserid 
                 FROM project_tasks 
                 WHERE assigneduserid = @id
                 """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { id }, cancellationToken: cancellationToken);
                var result = await connectionDb.QueryAsync<ProjectTask>(command);
                return result;
            }
        }

        public async Task UpdateProjectTask(ProjectTask task, CancellationToken cancellationToken)
        {
            string sqlExpression =
                 """
                 UPDATE project_tasks 
                 SET id = @id, title = @title, description = @description, status = @status, assigneduserid = @assigneduserid
                 WHERE id = @id
                 """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new
                {
                    task.Id,
                    task.Title,
                    task.Description,
                    task.Status,
                    task.Assigneduserid,
                }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }

        public async Task UpdateProjectStatusTaskByTaskId(int taskId, string newStatus, CancellationToken cancellationToken)
        {
            string sqlExpression = 
                 """
                 UPDATE project_tasks SET status = @newStatus 
                 WHERE id = @taskId
                 """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { newStatus, taskId }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }
        public async Task<IEnumerable<ProjectTask>> GetProjectTaskByStatus(string status, CancellationToken cancellationToken)
        {
            const string sqlExpression = 
                """
                SELECT id, title, description, status, assigneduserid
                FROM project_tasks
                WHERE status = @status
                """;

            using var connection = new NpgsqlConnection(_connectionString);
            var command = new CommandDefinition(sqlExpression, new { status }, cancellationToken: cancellationToken);

            return await connection.QueryAsync<ProjectTask>(command);
        }

        public async Task DeleteProjectTaskByTaskId(int taskId, CancellationToken cancellationToken)
        {
            string sqlExpression =
                """
                DELETE FROM project_tasks 
                WHERE id = @taskId
                """;
            using (IDbConnection connectionDb = new NpgsqlConnection(_connectionString))
            {
                var command = new CommandDefinition(sqlExpression, new { taskId }, cancellationToken: cancellationToken);
                await connectionDb.ExecuteAsync(command);
            }
        }
    }
}

