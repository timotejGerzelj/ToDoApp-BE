using Microsoft.AspNetCore.Mvc;
using ToDoApp.Interfaces;
using TodoTasks = ToDoApp.Models.Task;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET: api/Task
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Task>))]
        public IActionResult GetTasks()
        {
            var tasks = _taskRepository.GetTasks();
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            };
            return Ok(tasks);
        }
        // POST: api/Task
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Task<IEnumerable<Task>>))]
        public async Task<IActionResult> CreateTask(TodoTasks task)
        {
            var createdTask = await _taskRepository.CreateTask(task);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(createdTask);
        }

        [HttpGet("{taskId}")]
        [ProducesResponseType(200, Type = typeof(Task))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTaskById(int taskId){
            var task = await _taskRepository.GetTaskById(taskId);

            if (task == null)
                return NotFound(); 

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(task);
        }



        // DELETE: api/Task/5
        [HttpDelete("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTask(int taskId) {
            if (!_taskRepository.TaskExists(taskId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if (!_taskRepository.DeleteTask(taskId))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }
            return NoContent();
        }
        // UPDATE: api/Task/5
        [HttpPut("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int taskId, 
            [FromBody] TodoTasks updatedTask)
        {

            if (!_taskRepository.TaskExists(taskId)){
                return BadRequest(ModelState);
            }
            if (taskId != updatedTask.Id)
                return BadRequest(ModelState);
            if (!_taskRepository.UpdateTask(updatedTask))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        // UPDATE: api/Task/opravljeno/5
        [HttpPut("opravljeno/{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult TaskUpdateOpravljeno(int taskId){
            if (!_taskRepository.TaskExists(taskId)){
                return BadRequest(ModelState);
            }
            if (!_taskRepository.TaskUpdateOpravljeno(taskId))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }
            return NoContent();
    }
}
}
