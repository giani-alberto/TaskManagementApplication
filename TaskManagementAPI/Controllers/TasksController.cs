using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TaskServices _tasksService;

        public TasksController(TaskServices tasksService)
        {
              _tasksService = tasksService;
        }

        [HttpGet]
        public List<TaskItem> GetAll()
        {
            return _tasksService.GetAllTasks();
        }

        [HttpPost]

        public IActionResult Add(string title, string priority, string description)
        {
            _tasksService.AddTask(title ?? "", priority ?? "", description ?? "");
            _tasksService.SaveTasks();
            return Ok("Task added succesfully! ");
        }

        [HttpPut("{id}/complete")]
        public IActionResult ChangeStatus(int id, string status)
        {
            bool wasUpdated = _tasksService.CompleteTask(id, status);

            if (wasUpdated)
            {
                _tasksService.SaveTasks();
                return Ok($"Task {id} what marked as {status}!");
            }
            else
                return NotFound($"The task {id} was not found!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool WasDeleted =_tasksService.DeleteTask(id);
            if(WasDeleted)
            {
               _tasksService.SaveTasks();
                return Ok($"Task {id} was deleted succesfully. ");
            }
            else
            {
                return NotFound($"Task {id} not found.");
            }    
        }
    }
}
