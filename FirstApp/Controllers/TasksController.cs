using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FirstApp.Controllers
{
    [Route("api/tasks")]
    public class TasksController : Controller
    {
        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(TasksDataStore.Current);
        }

        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = TasksDataStore.Current.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }
    }
}
