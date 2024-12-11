using TaskApplication.Data.Common;
using TaskApplication.Data.Entities.Dtos;

namespace TaskApplication.Core.Interfaces
{
    public interface ITaskRepo
    {
        public Task<BaseRespone> CreateTask(TasksDto task);
        public Task<BaseRespone> UpdateTasks(TasksDto task);
        public Task<BaseRespone> DeleteTask(int taskId);
        public Task<List<TasksDto>> GetAllTask();
        public Task<BaseRespone> UpdateTaskStatus(TaskStatusDto task);
    }
}
