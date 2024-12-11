using TaskApplication.Data.Enums;

namespace TaskApplication.Data.Entities.Dtos
{
    public class TasksDto
    {
        public int Id { get; set; }
        public  required string Title { get; set; }
        public required string Description { get; set; }
        public string AssignedUserId { get; set; }
        public Status Status { get; set; }

    }
}
