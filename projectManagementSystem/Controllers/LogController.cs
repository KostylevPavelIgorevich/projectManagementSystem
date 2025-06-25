using Microsoft.AspNetCore.Mvc;
using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.LogsEntrys;
using System.Xml.Linq;

namespace projectManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogRepositories _logRepository;
        public LogController(ILogRepositories logRepository) => _logRepository = logRepository;

        [HttpPost]
        [Route(nameof(AddLog))]
        public async Task<Log> AddLog(Log logEntry, CancellationToken cancellationToken)
        {
            await _logRepository.AddLog(logEntry, cancellationToken);
            return logEntry;
        }

        [HttpGet]
        [Route(nameof(GetLogsByTaskId))]
        public async Task<List<Log>> GetLogsByTaskId(int taskId, CancellationToken cancellationToken)
        {
            var logs = await _logRepository.GetLogsByTaskId(taskId, cancellationToken);
            return logs.ToList();
        }

        [HttpPost]
        [Route(nameof(DeleteLogById))]
        public async Task DeleteLogById(int id, CancellationToken cancellationToken)
        { 
            await _logRepository.DeleteLogById(id , cancellationToken);
            
        }
    }
}
