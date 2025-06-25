namespace projectManagementSystem.Repositories.ProjectsTasks
{
    public class ProjectTaskDb
    {
        public int ProjectTaskId { get; set; }
        public string ProjectTaskTitle { get; set; }
        public string ProjectTaskDescription { get; set; }
        public string ProjectTaskStatus { get; set; }
        public int ProjectTaskAssignedUserId { get; set; }
    }
}
