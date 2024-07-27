using TaskManager.Models;
using TaskManager.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TaskViewModel>> GetTasks()
        {
            return _repository.ListTasks();
        }

        public Task<TaskViewModel> GetTaskById(int id)
        {
            return _repository.GetTaskByIdAsync(id);
        }

        public Task CreateTask(TaskViewModel task)
        {
            return _repository.AddTask(task);
        }

        public Task UpdateTask(TaskViewModel task)
        {
            return _repository.UpdateTask(task);
        }

        public Task DeleteTask(int id)
        {
            return _repository.RemoveTask(id);
        }
    }
}