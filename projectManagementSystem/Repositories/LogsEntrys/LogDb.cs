namespace projectManagementSystem.Repositories.LogsEntrys
{
    public class LogDb
    {
        public int LogEntryId { get; set; }
        public int LogEntryTaskId { get; set; }
        public int LogEntryUserId { get; set; }
        public string LogEntryMessage { get; set; }
        public DateTime LogEntryTimeStamp { get; set; }

    }
}
