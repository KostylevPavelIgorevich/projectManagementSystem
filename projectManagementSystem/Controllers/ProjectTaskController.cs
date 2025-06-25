using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.LogsEntrys;
using projectManagementSystem.Repositories.ProjectsTasks;

namespace projectManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskRepositories _projectTaskRepositories;

        public ProjectTaskController(IProjectTaskRepositories projectTaskRepositories)
        {
            _projectTaskRepositories = projectTaskRepositories;
        }

        [HttpPost]
        [Route(nameof(AddProjectTask))]
        public async Task<ProjectTask> AddProjectTask(ProjectTask projectTask, CancellationToken cancellationToken)
        {
            await _projectTaskRepositories.AddProjectTask(projectTask, cancellationToken);
            return projectTask;
        }

        [HttpGet]
        [Route(nameof(GetProjectTaskByTaskId))]
        public async Task<ProjectTask> GetProjectTaskByTaskId([FromQuery] int taskId, CancellationToken cancellationToken)
        {
            return await _projectTaskRepositories.GetProjectTaskByTaskId(taskId, cancellationToken);
        }

        [HttpGet]
        [Route(nameof(GetProjectTaskByAssignedUserId))]
        public async Task<IEnumerable<ProjectTask>> GetProjectTaskByAssignedUserId([FromQuery] int userId, CancellationToken cancellationToken)
        {
            return await _projectTaskRepositories.GetProjectTaskByAssignedUserId(userId, cancellationToken);
        }

        [HttpPut]
        [Route(nameof(UpdateProjectTask))]
        public async Task<IActionResult> UpdateProjectTask(ProjectTask projectTask, CancellationToken cancellationToken)
        {
            await _projectTaskRepositories.UpdateProjectTask(projectTask, cancellationToken);
            return Ok();
        }

        [HttpPatch]
        [Route(nameof(UpdateProjectStatusTaskByTaskId))]
        public async Task<IActionResult> UpdateProjectStatusTaskByTaskId([FromQuery] int taskId, [FromQuery] string newStatus, CancellationToken cancellationToken)
        {
            await _projectTaskRepositories.UpdateProjectStatusTaskByTaskId(taskId, newStatus, cancellationToken);
            return Ok();
        }

        [HttpGet]
        [Route(nameof(GetProjectTaskByStatus))]
        public async Task<IEnumerable<ProjectTask>> GetProjectTaskByStatus([FromQuery] string status, CancellationToken cancellationToken)
        {
            return await _projectTaskRepositories.GetProjectTaskByStatus(status, cancellationToken);
        }

        [HttpDelete]
        [Route(nameof(DeleteProjectTaskByTaskId))]
        public async Task<IActionResult> DeleteProjectTaskByTaskId([FromQuery] int taskId, CancellationToken cancellationToken)
        {
            await _projectTaskRepositories.DeleteProjectTaskByTaskId(taskId, cancellationToken);
            return Ok();
        }
    }
}

