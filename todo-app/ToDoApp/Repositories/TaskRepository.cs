using Microsoft.EntityFrameworkCore;
using ToDoApp.Interfaces;
using ToDoApp.Data;
using TodoTasks = ToDoApp.Models.Task;

namespace ToDoApp.Repository
{
        public class TaskRepository : ITaskRepository
    {
        private readonly TaskContext _context;
        public TaskRepository(TaskContext context){
            _context = context;
        }
        public bool DeleteTask(int id)
        {
            var sql = "DELETE FROM todo_tasks WHERE id = {0}";
            var parameters = new object[] { id };

            int rowsAffected = _context.Database.ExecuteSqlRaw(sql, parameters);
            return rowsAffected > 0;
        }

        public ICollection<TodoTasks> GetTasks(){     
        try
            {
                var tasks = _context.TodoTasks.FromSqlRaw("SELECT * FROM todo_tasks").ToList();
                return tasks;
            }
        catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<TodoTasks>(); // Return an empty collection or handle the error gracefully
            }
       }
        public Task<TodoTasks?> GetTaskById(int id)
        {
            var task = _context.TodoTasks.FromSqlRaw("SELECT * FROM todo_tasks WHERE id = {0}", id).FirstOrDefaultAsync();
            return task;

        }
        public async Task<TodoTasks> CreateTask(TodoTasks task)
        {
            var sql = "INSERT INTO todo_tasks (naslov, opis, datum_ustvarjanja, opravljeno) VALUES ({0}, {1}, {2}, {3})";
            var parameters = new object[] { task.Naslov, task.Opis, task.DatumUstvarjanja, task.Opravljeno };
            var generatedId = await _context.Database.ExecuteSqlRawAsync(sql, parameters);
            var newTask = await _context.TodoTasks
            .FromSqlRaw("SELECT * FROM todo_tasks WHERE naslov = {0} AND opis = {1} AND datum_ustvarjanja = {2}", task.Naslov, task.Opis, task.DatumUstvarjanja)
            .FirstOrDefaultAsync();
            task.Id = generatedId;
             if (newTask == null){
                return NotFound();
            }
            return newTask;
        }
        public bool TaskUpdateOpravljeno(int id){
        string sql = "UPDATE todo_tasks SET opravljeno = true WHERE id = {0}";
        var parameters = new object[] {  id };
        int rowsAffected = _context.Database.ExecuteSqlRaw(sql, parameters);
        return rowsAffected > 0; 
        }
        private TodoTasks NotFound()
        {
            throw new NotImplementedException();
        }

        public bool UpdateTask(TodoTasks task)
        {
            string sql = "UPDATE todo_tasks SET naslov = {0}, opis = {1}, datum_ustvarjanja = {2}, opravljeno = {3} WHERE id = {4}";
            var parameters = new object[] { task.Naslov, task.Opis, task.DatumUstvarjanja, task.Opravljeno, task.Id };
            int rowsAffected = _context.Database.ExecuteSqlRaw(sql, parameters);
            return rowsAffected > 0;
        }
        public bool TaskExists(int id)
        {   
            return _context.TodoTasks.Any(p => p.Id == id);

        }
    }


    }