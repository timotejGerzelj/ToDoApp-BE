using Microsoft.EntityFrameworkCore;
using TodoTasks = ToDoApp.Models.Task;

namespace ToDoApp.Data;
public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options)
        : base(options)
    {}

    public DbSet<TodoTasks> TodoTasks { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoTasks>().HasKey(t => new { t.Id});
        }

}