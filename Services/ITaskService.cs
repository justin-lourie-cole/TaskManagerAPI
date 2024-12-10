using TaskManagerAPI.Models;

namespace TaskManagerAPI.Services;

public interface ITaskService
{
  Task<IEnumerable<TodoTask>> GetAllTasksAsync();
  Task<TodoTask?> GetTaskByIdAsync(int id);
  Task<TodoTask> CreateTaskAsync(TodoTask task);
  Task<TodoTask?> UpdateTaskAsync(TodoTask task);
  Task<bool> DeleteTaskAsync(int id);
}
