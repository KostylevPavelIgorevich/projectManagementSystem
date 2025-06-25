using projectManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectManagementSystem.Repositories.LogsEntrys
{
    public interface ILogRepositories
    {
        Task AddLog(Log log, CancellationToken cancellationToken);
        Task<IEnumerable<Log>> GetLogsByTaskId(int taskId, CancellationToken cancellationToken);
        Task DeleteLogById(int logId, CancellationToken cancellationToken);
    }
}
