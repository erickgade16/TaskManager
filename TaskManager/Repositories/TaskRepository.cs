using TaskViewModel = TaskManager.Models.TaskViewModel;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskViewModel> _tasks = new List<TaskViewModel>();
    private int _nextId = 1;

    public Task<TaskViewModel> GetTaskByIdAsync(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(task);
    }

    public Task<TaskViewModel> AddTask(TaskViewModel task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
        return Task.FromResult(task);
    }

    public Task<TaskViewModel?> RemoveTask(int taskId)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null)
        {
            _tasks.Remove(task);
            return Task.FromResult(task);
        }
        return Task.FromResult<TaskViewModel>(null);
    }

    public Task<TaskViewModel> UpdateTask(TaskViewModel task)
    {
        var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
        if (existingTask != null)
        {
            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.DueDate = task.DueDate;
            existingTask.Priority = task.Priority;
            return Task.FromResult(existingTask);
        }
        return Task.FromResult<TaskViewModel>(null);
    }

    public Task<IEnumerable<TaskViewModel>> ListTasks()
    {
        return Task.FromResult<IEnumerable<TaskViewModel>>(_tasks);
    }
}