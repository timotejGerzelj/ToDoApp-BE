// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using TodoTask = ToDoApp.Models.Task;

namespace ToDoApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TodoTask> Tasks { get; set; }
    }
}