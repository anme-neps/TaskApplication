using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskApplication.Data;
using TaskApplication.Data.Common;
using TaskApplication.Data.Entities;
using TaskApplication.Data.Entities.Dtos;

namespace TaskApplication.Core.Interfaces
{
    public class TasksRepo : ITaskRepo
    {
        private readonly TaskDbContext _dbContext;
        public TasksRepo(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BaseRespone> CreateTask(TasksDto task)
        {
            var result = new BaseRespone();
            try
            {
                var data = new Tasks
                {
                    AssignedUserId = task.AssignedUserId,
                    Description = task.Description,
                    Status = task.Status,
                    Title = task.Title,
                };
                await _dbContext.Tasks.AddAsync(data);
                await _dbContext.SaveChangesAsync();
                result.IsSuccess = true;
                result.Description = "Task creation successful";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Description = ex.Message;
                return result;
            }
        }

        public async Task<BaseRespone> DeleteTask(int taskId)
        {
            var result = new BaseRespone();
            try
            {
                var currentData = await _dbContext.Tasks.Where(x => x.Id == taskId).FirstOrDefaultAsync();
                if (currentData is null)
                {
                    result.IsSuccess = false;
                    result.Description = "Task not found";
                    return result;
                }
                _dbContext.Tasks.Remove(currentData);
                await _dbContext.SaveChangesAsync();
                result.IsSuccess = true;
                result.Description = "Task delete successful";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Description = ex.Message;
                return result;
            }
        }

        public async Task<List<TasksDto>> GetAllTask()
        {
            return await _dbContext.Tasks.Select(s => new TasksDto
            {
                AssignedUserId = s.AssignedUserId,
                Description = s.Description,
                Status = s.Status,
                Title = s.Title,
                Id = s.Id
            }).ToListAsync();
        }

        public async Task<BaseRespone> UpdateTaskStatus(TaskStatusDto task)
        {
            var result = new BaseRespone();
            try
            {
                var currentData = await _dbContext.Tasks.Where(x => x.Id == task.Id).FirstOrDefaultAsync();
                if (currentData is null)
                {
                    result.IsSuccess = false;
                    result.Description = "Task not found";
                    return result;
                }
                currentData.Status = task.Status;

                await _dbContext.SaveChangesAsync();

                result.IsSuccess = true;
                result.Description = "Status update successful";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Description = ex.Message;
                return result;
            }
        }

        public async Task<BaseRespone> UpdateTasks(TasksDto task)
        {
            var result = new BaseRespone();
            try
            {
                var currentData = await _dbContext.Tasks.Where(x => x.Id == task.Id).FirstOrDefaultAsync();
                if (currentData is null)
                {
                    result.IsSuccess = false;
                    result.Description = "Task not found";
                    return result;
                }
                currentData.AssignedUserId = task.AssignedUserId;
                currentData.Description = task.Description;
                currentData.Status = task.Status;
                currentData.Title = task.Title;

                await _dbContext.SaveChangesAsync();

                result.IsSuccess = true;
                result.Description = "Task update successful";
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Description = ex.Message;
                return result;
            }
        }
    }
}
