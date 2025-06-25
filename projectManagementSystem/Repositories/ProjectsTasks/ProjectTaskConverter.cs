using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.LogsEntrys;

namespace projectManagementSystem.Repositories.ProjectsTasks
{
    public static class ProjectTaskConverter
    {
        public static ProjectTask ToDomain(this ProjectTaskDb projectTaskDb)
        {
            return new ProjectTask(projectTaskDb.ProjectTaskId, projectTaskDb.ProjectTaskTitle, projectTaskDb.ProjectTaskDescription, projectTaskDb.ProjectTaskStatus, projectTaskDb.ProjectTaskAssignedUserId);
        }
    }
}
