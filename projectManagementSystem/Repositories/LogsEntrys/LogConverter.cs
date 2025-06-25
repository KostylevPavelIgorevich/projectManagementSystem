using Microsoft.Extensions.Logging.Abstractions;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectManagementSystem.Domain.Entities;

namespace projectManagementSystem.Repositories.LogsEntrys
{
    public static class LogConverter
    {
        public static Log ToDomain(this LogDb logEntry)
        {
            return new Log(logEntry.LogEntryId, logEntry.LogEntryUserId, logEntry.LogEntryTaskId, logEntry.LogEntryMessage, logEntry.LogEntryTimeStamp);
        }
    }
}
