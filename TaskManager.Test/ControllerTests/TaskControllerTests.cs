using Moq;
using TaskManager.Services.Interfaces;
using TaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers;

namespace TaskManager.Test.ControllerTests
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
            var tasks = new List<TaskViewModel> { new TaskViewModel { Id = 1, Title = "Test Task" } };
            _mockTaskService.Setup(service => service.GetTasks()).ReturnsAsync(tasks);

            var result = await _controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<TaskViewModel>>(viewResult.ViewData.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            var result = _controller.Create();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            var task = new TaskViewModel { Id = 1, Title = "New Task" };
            _mockTaskService.Setup(service => service.CreateTask(task)).Returns(Task.CompletedTask);

            var result = await _controller.Create(task);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithTask()
        {
            var task = new TaskViewModel { Id = 1, Title = "Existing Task" };
            _mockTaskService.Setup(service => service.GetTaskById(1)).ReturnsAsync(task);

            var result = await _controller.Edit(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TaskViewModel>(viewResult.ViewData.Model);
            Assert.Equal(task.Id, model.Id);
        }

        [Fact]
        public async Task Delete_ReturnsViewResult_WithTask()
        {
            var task = new TaskViewModel { Id = 1, Title = "Task to Delete" };
            _mockTaskService.Setup(service => service.GetTaskById(1)).ReturnsAsync(task);

            var result = await _controller.Delete(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TaskViewModel>(viewResult.ViewData.Model);
            Assert.Equal(task.Id, model.Id);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex()
        {
            var result = await _controller.DeleteConfirmed(1);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
