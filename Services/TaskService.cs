using TaskManagerAPI.Models;
using TaskManagerAPI.Repositories;

namespace TaskManagerAPI.Services;

public class TaskService : ITaskService
{
  // Dependency injection for the task repository
  private readonly IRepository<TodoTask> _taskRepository;

  // Constructor that injects the task repository
  public TaskService(IRepository<TodoTask> taskRepository)
  {
    _taskRepository = taskRepository;
  }

  // Returns all todo tasks from the database
  public async Task<IEnumerable<TodoTask>> GetAllTasksAsync()
  {
    return await _taskRepository.GetAllAsync();
  }

  // Returns a single todo task by its ID
  public async Task<TodoTask?> GetTaskByIdAsync(int id)
  {
    return await _taskRepository.GetByIdAsync(id);
  }

  // Creates a new todo task in the database
  public async Task<TodoTask> CreateTaskAsync(TodoTask task)
  {
    var createdTask = await _taskRepository.AddAsync(task);
    await _taskRepository.SaveChangesAsync();
    return createdTask;
  }

  // Updates an existing todo task in the database
  public async Task<TodoTask?> UpdateTaskAsync(TodoTask task)
  {
    var existingTask = await _taskRepository.GetByIdAsync(task.Id);
    if (existingTask == null) return null;

    var updatedTask = await _taskRepository.UpdateAsync(task);
    await _taskRepository.SaveChangesAsync();
    return updatedTask;
  }

  // Deletes a todo task from the database
  public async Task<bool> DeleteTaskAsync(int id)
  {
    var task = await _taskRepository.GetByIdAsync(id);
    if (task == null) return false;

    await _taskRepository.DeleteAsync(task);
    return await _taskRepository.SaveChangesAsync();
  }
}
