using TaskApplication.Data.Enums;

namespace TaskApplication.Data.Entities.Dtos
{
    public class TaskStatusDto
    {
        public int Id { get; set; }
        public Status Status { get; set; }
    }
}
