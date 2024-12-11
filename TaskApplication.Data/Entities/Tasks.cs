using TaskApplication.Data.Enums;

namespace TaskApplication.Data.Entities
{
    public class Tasks
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string AssignedUserId { get; set; }
        public Status Status { get; set; }

    }
}
