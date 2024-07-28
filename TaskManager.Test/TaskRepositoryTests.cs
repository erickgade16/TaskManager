using TaskManager.Models;


namespace TaskManager.Tests
{
    public class TaskRepositoryTests
    {
        private readonly TaskRepository _repository;

        public TaskRepositoryTests()
        {
            _repository = new TaskRepository();
        }

        [Fact]
        public async Task AddTask_ShouldAddTaskToList()
        {
            // Arrange
            var task = new TaskViewModel { Title = "New Task", Description = "Description", DueDate = DateTime.Now, Priority = true };

            // Act
            var addedTask = await _repository.AddTask(task);

            // Assert
            Assert.NotNull(addedTask);
            Assert.Equal(1, addedTask.Id);
            Assert.Single(await _repository.ListTasks());
        }

        [Fact]
        public async Task GetTaskByIdAsync_ShouldReturnCorrectTask()
        {
            // Arrange
            var task = new TaskViewModel { Title = "New Task", Description = "Description", DueDate = DateTime.Now, Priority = true };
            await _repository.AddTask(task);

            // Act
            var retrievedTask = await _repository.GetTaskByIdAsync(1);

            // Assert
            Assert.NotNull(retrievedTask);
            Assert.Equal(1, retrievedTask.Id);
        }

        [Fact]
        public async Task RemoveTask_ShouldRemoveTaskFromList()
        {
            // Arrange
            var task = new TaskViewModel { Title = "New Task", Description = "Description", DueDate = DateTime.Now, Priority = true };
            await _repository.AddTask(task);

            // Act
            var removedTask = await _repository.RemoveTask(1);

            // Assert
            Assert.NotNull(removedTask);
            Assert.Empty(await _repository.ListTasks());
        }

        [Fact]
        public async Task UpdateTask_ShouldUpdateExistingTask()
        {
            // Arrange
            var task = new TaskViewModel { Title = "New Task", Description = "Description", DueDate = DateTime.Now, Priority = true };
            await _repository.AddTask(task);

            var updatedTask = new TaskViewModel { Id = 1, Title = "Updated Task", Description = "Updated Description", DueDate = DateTime.Now.AddDays(1), Priority = false };

            // Act
            var result = await _repository.UpdateTask(updatedTask);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Task", result.Title);
            Assert.Equal("Updated Description", result.Description);
            Assert.False(result.Priority); // Corrigir a verificação da prioridade
        }

        [Fact]
        public async Task ListTasks_ShouldReturnAllTasks()
        {
            // Arrange
            var task1 = new TaskViewModel { Title = "Task 1", Description = "Description 1", DueDate = DateTime.Now, Priority = true };
            var task2 = new TaskViewModel { Title = "Task 2", Description = "Description 2", DueDate = DateTime.Now.AddDays(1), Priority = false };

            await _repository.AddTask(task1);
            await _repository.AddTask(task2);

            // Act
            var tasks = await _repository.ListTasks();

            // Assert
            Assert.Equal(2, tasks.Count());
        }
    }
}
