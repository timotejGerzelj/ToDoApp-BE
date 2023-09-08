
using Microsoft.AspNetCore.Mvc;
using FakeItEasy;
using FluentAssertions;
using ToDoApp.Controllers;

using ToDoApp.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using ToDoApp.Data;
using ToDoApp.Repository;


namespace ToDoApp.Tests.Controller {
        // Arange - Go get your variables, classes functions etc.

        // Act - Execute the function 

        // Assert - Whatever is returned is it what you want
    
    public class TaskControllerTests
    {   private readonly ITaskRepository _taskRepositories;
        public TaskControllerTests() { 
            _taskRepositories = A.Fake<ITaskRepository>();
        }
     [Fact]
        public void TaskController_CreateTask_ReturnOK()
        {

            //Arrange
            var pokemon = new Models.Task{
                Naslov = "Sample Task",
                Opis = "This is a sample task description.",
                DatumUstvarjanja = DateTime.Now,
                Opravljeno = false 
            };
            A.CallTo(() => _taskRepositories.CreateTask(pokemon)).Returns(pokemon);
            var controller = new TaskController(_taskRepositories);
            var result = controller.CreateTask(pokemon);
            result.Should().NotBeNull();

        // Set properties as needed
        // For example: Id = 3, Naslov = "SomeTitle", Opis = "SomeDescription", ...
    }
        [Fact]
        public void TaskController_GetTasks_ReturnOK()
        {
            //Arrange
            var controller = new TaskController(_taskRepositories);

            //Act
            var result = controller.GetTasks();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }
        [Fact]
        public void TaskController_DeleteTask_ReturnNoContent()
        {
            // Arrange
            var taskId = 1; // Replace with the ID of the task to delete
            A.CallTo(() => _taskRepositories.TaskExists(taskId)).Returns(true);
            A.CallTo(() => _taskRepositories.DeleteTask(taskId)).Returns(true);
            var controller = new TaskController(_taskRepositories);

            // Act
            var result = controller.DeleteTask(taskId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(NoContentResult));
        }
        [Fact]
        public void TaskController_UpdateTask_ReturnNoContent()
        {
            // Arrange
            var taskId = 1; // Replace with the ID of the task to update
            var updatedTask = new Models.Task
            {
                Id = taskId,
                Naslov = "Updated Title",
                Opis = "Updated Description",
                DatumUstvarjanja = DateTime.Now,
                Opravljeno = true
            };
        A.CallTo(() => _taskRepositories.TaskExists(taskId)).Returns(true);
        A.CallTo(() => _taskRepositories.UpdateTask(updatedTask)).Returns(true);
        var controller = new TaskController(_taskRepositories);

        // Act
        var result = controller.UpdateTask(taskId, updatedTask);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        }
        [Fact]
        public async void TaskController_GetTaskById_ReturnOK()
        {
            // Arrange
            var taskId = 1; // Replace with the ID of the task to retrieve
            var expectedTask = new Models.Task
            {
                Id = taskId,
                Naslov = "Sample Task",
               Opis = "This is a sample task description.",
                DatumUstvarjanja = DateTime.Now,
                Opravljeno = false
            };
            A.CallTo(() => _taskRepositories.GetTaskById(taskId)).Returns(expectedTask);
            var controller = new TaskController(_taskRepositories);

            // Act
            var result = await controller.GetTaskById(taskId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<Models.Task>();
        }
        }

    }