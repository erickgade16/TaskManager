using TaskManager.Models;

public interface ITaskRepository
{
    Task<IEnumerable<TaskViewModel>> ListTasks();
    Task<TaskViewModel?> GetTaskByIdAsync(int id);
    Task<TaskViewModel?> AddTask(TaskViewModel task);
    Task<TaskViewModel?> UpdateTask(TaskViewModel task);
    Task<TaskViewModel?> RemoveTask(int id);
}

