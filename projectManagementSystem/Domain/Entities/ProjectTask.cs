namespace projectManagementSystem.Domain.Entities
{
    public class ProjectTask
    {
        public ProjectTask(int id, string title, string description, string status, int assigneduserid) 
        {
            Id = id;
            Title = title;
            Description = description;
            Status = status;
            Assigneduserid = assigneduserid;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int Assigneduserid { get; set; }

    }
}
