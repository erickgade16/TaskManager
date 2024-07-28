using Moq;
using TaskManager.Services.Interfaces;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers;

namespace TaskManager.Tests
{
    public class TaskControllerTests
    {
        private readonly Mock<ITaskService> _mockTaskService;
        private readonly TaskController _controller;

        public TaskControllerTests()
        {
            _mockTaskService = new Mock<ITaskService>();
            _controller = new TaskController(_mockTaskService.Object);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithListOfTasks()
        {
            // Arrange
            var tasks = new List<TaskViewModel> { new TaskViewModel { Id = 1, Title = "Test Task" } };
            _mockTaskService.Setup(service => service.GetTasks()).ReturnsAsync(tasks);

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TaskViewModel>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var task = new TaskViewModel { Id = 1, Title = "New Task" };
            _mockTaskService.Setup(service => service.CreateTask(task)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(task);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithTask()
        {
            // Arrange
            var task = new TaskViewModel { Id = 1, Title = "Existing Task" };
            _mockTaskService.Setup(service => service.GetTaskById(1)).ReturnsAsync(task);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TaskViewModel>(viewResult.ViewData.Model);
            Assert.Equal(task.Id, model.Id);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithTask()
        {
            // Arrange
            var task = new TaskViewModel { Id = 1, Title = "Task to Delete" };
            _mockTaskService.Setup(service => service.GetTaskById(1)).ReturnsAsync(task);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TaskViewModel>(viewResult.ViewData.Model);
            Assert.Equal(task.Id, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex()
        {
            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
