using projectManagementSystem.Domain.Entities;

namespace projectManagementSystem.Repositories.ProjectsTasks
{
    public interface IProjectTaskRepositories
    {
        Task AddProjectTask(ProjectTask task, CancellationToken cancellationToken);
        Task<ProjectTask> GetProjectTaskByTaskId(int taskId, CancellationToken cancellationToken);
        Task<IEnumerable<ProjectTask>> GetProjectTaskByAssignedUserId(int userId, CancellationToken cancellationToken);
        Task UpdateProjectTask(ProjectTask task, CancellationToken cancellationToken);
        Task UpdateProjectStatusTaskByTaskId(int taskId, string newStatus, CancellationToken cancellationToken);
        Task<IEnumerable<ProjectTask>> GetProjectTaskByStatus(string status, CancellationToken cancellationToken);
        Task DeleteProjectTaskByTaskId(int taskId, CancellationToken cancellationToken);
    }
}
