using TaskManager.Models;

namespace TaskManager.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskViewModel>> GetTasks();
        Task<TaskViewModel> GetTaskById(int id);
        Task CreateTask(TaskViewModel task);
        Task UpdateTask(TaskViewModel task);
        Task DeleteTask(int id);
    }
}