using Microsoft.AspNetCore.Http.HttpResults;
using System.Data.Common;

namespace projectManagementSystem.Domain.Entities
{
    public class Log
    {
        public Log() { }
        public Log(int id, int taskId, int userId, string message, DateTime createdAt)
        {
            Id = id;
            TaskId = taskId;
            UserId = userId;
            Message = message;
            CreatedAt = createdAt;
        }

        public int Id { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
