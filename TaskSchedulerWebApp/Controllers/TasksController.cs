using Microsoft.AspNetCore.Mvc;
using TaskSchedulerWebApp.Models;
using TaskSchedulerWebApp.Services;

namespace TaskSchedulerWebApp.Controllers
{
    [ApiController]
    [Route("home/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskStorageService _taskService;

        public TasksController(ITaskStorageService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var tasks = _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ScheduledTask task)
        {
            if (task == null || string.IsNullOrWhiteSpace(task.Name))
                return BadRequest("Invalid task data.");

            var createdTask = _taskService.AddTask(task);
            return CreatedAtAction(nameof(Get), new { id = createdTask.Id }, createdTask);
        }
    }
}
