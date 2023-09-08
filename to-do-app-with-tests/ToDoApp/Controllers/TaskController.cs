using Microsoft.AspNetCore.Mvc;
using ToDoApp.Data;
using TodoTasks = ToDoApp.Models.Task;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Task
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TodoTasks>))]
        public ActionResult<IEnumerable<TodoTasks>> GetTasks()
        {
            var tasks = _context.Tasks.ToList();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(tasks);
 
        }

// GET: api/Task/5
[HttpGet("{id}")]
[ProducesResponseType(200, Type = typeof(TodoTasks))]
[ProducesResponseType(400)]
public ActionResult<TodoTasks> GetTaskById(int id)
{
        if (!TaskExists(id))
            return NotFound();

        var task = _context.Tasks.Find(id);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(task);
    }


[HttpPost]
[ProducesResponseType(204)]
[ProducesResponseType(400)]
public async Task<ActionResult<TodoTasks>> CreateTask(TodoTasks task)
{
    if (task == null)
            return BadRequest(ModelState);

    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    _context.Tasks.Add(task);
    await _context.SaveChangesAsync(); 
    return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);

}
// PUT: api/Task/5
[HttpPut("{id}")]
[ProducesResponseType(400)]
[ProducesResponseType(204)]
[ProducesResponseType(404)]
public async Task<IActionResult> UpdateTask(int id, TodoTasks task)
{
            if (task == null)
              return BadRequest(ModelState);
            if (id != task.Id)
                return BadRequest("Invalid task ID.");
            if (TaskExists(id)){
                var existingTask = await _context.Tasks.FindAsync(id);
                if (existingTask == null)
                {
                    return NotFound();
                }

                _context.Entry(existingTask).CurrentValues.SetValues(task);
                await _context.SaveChangesAsync();

                return NoContent();
        }
        else {
                ModelState.AddModelError("", "Something went wrong updating the task");
                return StatusCode(500, ModelState);

        }
        }

// DELETE: api/Task/5
[HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (!TaskExists(id)){
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                {
                    return NotFound();
                }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);



            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
    }

[HttpPut("opravljeno/{id}")]
[ProducesResponseType(400)]
[ProducesResponseType(204)]
[ProducesResponseType(404)]
public async Task<IActionResult> TaskUpdateOpravljeno(int id, TodoTasks task)
{
            if (task == null)
              return BadRequest(ModelState);
            if (id != task.Id)
                return BadRequest("Invalid task ID.");
            if (TaskExists(id)){
                var existingTask = await _context.Tasks.FindAsync(id);
                if (existingTask == null)
                {
                    return NotFound();
                }

                existingTask.Opravljeno = true; // Update only the Opravljeno field
                await _context.SaveChangesAsync();

                return NoContent();
        }
        else {
                ModelState.AddModelError("", "Something went wrong updating the task");
                return StatusCode(500, ModelState);

        }
}
        private bool TaskExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
