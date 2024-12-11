using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApplication.Core.Interfaces;
using TaskApplication.Data.Entities.Dtos;

namespace TaskApplication.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(ITaskRepo taskRepo) : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpPost("createTask")]
        public async Task<ActionResult<string>> CreateTasks([FromBody] TasksDto taskDto)
        {
            var result = await taskRepo.CreateTask(taskDto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("updateTask")]
        public async Task<ActionResult<string>> UpdateTasks([FromBody] TasksDto taskDto)
        {
            var result = await taskRepo.UpdateTasks(taskDto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize(Roles = "Users")]
        [HttpPost("updateTaskStatus")]
        public async Task<ActionResult<string>> UpdateTasksStatus([FromBody] TaskStatusDto taskDto)
        {
            var result = await taskRepo.UpdateTaskStatus(taskDto);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("deleteTask")]
        public async Task<ActionResult<string>> DeleteTasks([FromBody] int taskId)
        {
            var result = await taskRepo.DeleteTask(taskId);
            if (!result.IsSuccess)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpGet("getAllTask")]
        public async Task<ActionResult<string>> GetAllTasks()
        {
            var result = await taskRepo.GetAllTask();
            return Ok(result);
        }
    }
}
