using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private static List<TaskItem> tasks = new List<TaskItem>
        {
            new TaskItem { Id = 1, Title = "Learn ", IsCompleted = false },
            new TaskItem { Id = 2, Title = "Build .NET API", IsCompleted = true }
        };

        [HttpGet]
        public IActionResult GetAll() => Ok(tasks);

        [HttpPost]
        public IActionResult AddTask(TaskItem task)
        {
            task.Id = tasks.Count + 1;
            tasks.Add(task);
            return CreatedAtAction(nameof(GetAll), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.IsCompleted = updatedTask.IsCompleted;
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null) return NotFound();

            tasks.Remove(task);
            return NoContent();
        }
    }
}
