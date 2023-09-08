using TodoTasks = ToDoApp.Models.Task;


namespace ToDoApp.Interfaces {
    public interface ITaskRepository
    {
        ICollection<TodoTasks> GetTasks();
        Task<TodoTasks?> GetTaskById(int id);
        Task<TodoTasks> CreateTask(TodoTasks task);
        bool UpdateTask(TodoTasks task);
        bool DeleteTask(int id);
        bool TaskUpdateOpravljeno(int id); 
        bool TaskExists(int id);
    }
}