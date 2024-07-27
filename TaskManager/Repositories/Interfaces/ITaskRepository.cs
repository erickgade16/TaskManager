namespace TaskManager.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        void AddTask(Task task);
        void RemoveTask(int taskId);
        void UpdateTask(Task task);
        IEnumerable<Task> ListTasks();
    }
}
