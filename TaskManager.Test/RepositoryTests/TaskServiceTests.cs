using Moq;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Test.RepositoryTests
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _mockRepository;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _mockRepository = new Mock<ITaskRepository>();
            _service = new TaskService(_mockRepository.Object);
        }

        [Fact]
        public async Task GetTasks_ShouldReturnTasks()
        {
            var tasks = new List<TaskViewModel>
            {
                new TaskViewModel { Id = 1, Title = "Task 1", Description = "Description 1", DueDate = DateTime.Now, Priority = true },
                new TaskViewModel { Id = 2, Title = "Task 2", Description = "Description 2", DueDate = DateTime.Now.AddDays(1), Priority = false }
            };
            _mockRepository.Setup(repo => repo.ListTasks()).ReturnsAsync(tasks);

            var result = await _service.GetTasks();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTaskById_ShouldReturnTask()
        {
            var task = new TaskViewModel { Id = 1, Title = "Task 1", Description = "Description 1", DueDate = DateTime.Now, Priority = true };
            _mockRepository.Setup(repo => repo.GetTaskByIdAsync(1)).ReturnsAsync(task);

            var result = await _service.GetTaskById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task CreateTask_ShouldCallRepositoryAddTask()
        {
            var task = new TaskViewModel { Title = "New Task", Description = "Description", DueDate = DateTime.Now, Priority = true };
            _mockRepository.Setup(repo => repo.AddTask(task)).ReturnsAsync(task);

            await _service.CreateTask(task);

            _mockRepository.Verify(repo => repo.AddTask(task), Times.Once);
        }

        [Fact]
        public async Task UpdateTask_ShouldCallRepositoryUpdateTask()
        {
            var task = new TaskViewModel { Id = 1, Title = "Updated Task", Description = "Updated Description", DueDate = DateTime.Now.AddDays(1), Priority = false };
            _mockRepository.Setup(repo => repo.UpdateTask(task)).ReturnsAsync(task);

            await _service.UpdateTask(task);

            _mockRepository.Verify(repo => repo.UpdateTask(task), Times.Once);
        }

        [Fact]
        public async Task DeleteTask_ShouldCallRepositoryRemoveTask()
        {
            _mockRepository.Setup(repo => repo.RemoveTask(1)).ReturnsAsync((TaskViewModel?)null);

            await _service.DeleteTask(1);

            _mockRepository.Verify(repo => repo.RemoveTask(1), Times.Once);
        }
    }
}
