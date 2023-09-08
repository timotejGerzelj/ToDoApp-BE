using Microsoft.EntityFrameworkCore;
using ToDoApp.Controllers;
using ToDoApp.Data;
using FluentAssertions;
using Xunit;
using TodoTasks = ToDoApp.Models.Task;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Tests.Controller
{
    public class ToDoControllerTests{
 private async Task<AppDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new AppDbContext(options);
            databaseContext.Database.EnsureCreated();
            var tasks = new List<Task>();
            
            if (await databaseContext.Tasks.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.Tasks.Add(
                        new TodoTasks {
                        Id = i,
                        Naslov = $"Task {i}",
                        Opis = $"Description {i}",
                        DatumUstvarjanja = DateTime.Now, // You may set this to a specific date
                        Opravljeno = i % 2 == 0 // This alternates true and false
                    }
                    );
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }    
        
        [Fact]
        public async void TaskController_GetTask_ReturnsTask()
        {
                // Arrange
                var dbContext = await GetDatabaseContext();
                var taskController = new TaskController(dbContext);

                // Act
                var actionResult = taskController.GetTaskById(1);
    
                // Assert
                var objectResult = actionResult.Result as OkObjectResult;
                objectResult.Should().NotBeNull();
    
                var task = objectResult.Value as TodoTasks;
                task.Should().NotBeNull();
                task.Should().BeOfType<TodoTasks>();
        }

        [Fact]
        public async void TaskController_GetTasks_ReturnsTasks()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var taskController = new TaskController(dbContext);

            // Act
            var result = taskController.GetTasks();

            // Assert
            var objectResult = result.Result as OkObjectResult;
            objectResult.Should().NotBeNull();

            var tasks = objectResult.Value as List<TodoTasks>;
            tasks.Should().NotBeNull();
            tasks.Should().HaveCountGreaterThan(9); 
        }
        [Fact]
public async Task TaskController_UpdateTask_UpdatesTask()
{
    // Arrange
    var dbContext = await GetDatabaseContext();
    var taskController = new TaskController(dbContext);
    var taskIdToUpdate = 1;

    // Create a modified task with the same ID
    var modifiedTask = new TodoTasks
    {
        Id = taskIdToUpdate,
        Naslov = "Modified Task",
        Opis = "Updated Description",
        DatumUstvarjanja = DateTime.Now,
        Opravljeno = true
    };

    // Act
    var result = await taskController.UpdateTask(taskIdToUpdate, modifiedTask);

    // Assert
    result.Should().BeOfType<NoContentResult>();

    // Retrieve the updated task from the database
    var updatedTask = await dbContext.Tasks.FindAsync(taskIdToUpdate);
    updatedTask.Should().NotBeNull();
    updatedTask.Naslov.Should().Be("Modified Task");
    updatedTask.Opis.Should().Be("Updated Description");
    updatedTask.Opravljeno.Should().Be(true);
}

[Fact]
public async Task TaskController_DeleteTask_DeletesTask()
{
    // Arrange
    var dbContext = await GetDatabaseContext();
    var taskController = new TaskController(dbContext);
    var taskIdToDelete = 1;

    // Act
    var result = await taskController.DeleteTask(taskIdToDelete);

    // Assert
    result.Should().BeOfType<NoContentResult>();

    // Verify that the task with the specified ID is deleted from the database
    var deletedTask = await dbContext.Tasks.FindAsync(taskIdToDelete);
    deletedTask.Should().BeNull();
    }

    [Fact]
    public async Task TaskController_DeleteTask_InvalidId_ReturnsNotFound()
    {
    // Arrange
    var dbContext = await GetDatabaseContext();
    var taskController = new TaskController(dbContext);
    var taskIdToDelete = 999; // Non-existent ID

    // Act
    var result = await taskController.DeleteTask(taskIdToDelete);

    // Assert
    result.Should().BeOfType<NotFoundResult>();
    }
    [Fact]
    public async Task TaskController_UpdateOpravljeno_ValidId_ReturnsNoContent(){
                   // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var dbContext = new AppDbContext(options))
            {
                // Add a task with Opravljeno set to false
                var task = new TodoTasks { Id = 1, Naslov = "Test Task", Opis = "Description", Opravljeno = false };
                dbContext.Tasks.Add(task);
                dbContext.SaveChanges();
            }

            using (var dbContext = new AppDbContext(options))
            {
                var controller = new TaskController(dbContext);
                var updatedTask = new TodoTasks { Id = 1, Naslov="", DatumUstvarjanja=DateTime.Now, Opis="", Opravljeno = true };

                // Act
                var result = await controller.TaskUpdateOpravljeno(1, updatedTask);

                // Assert
                result.Should().BeOfType<NoContentResult>();
            }
    }


    
}
}