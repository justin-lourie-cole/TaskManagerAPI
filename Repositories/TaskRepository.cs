using TaskManagerAPI.Data;
using TaskManagerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagerAPI.Repositories;

public class TaskRepository : IRepository<TodoTask>
{
  // Stores the database context
  private readonly AppDbContext _context;

  // Constructor that injects the database context
  public TaskRepository(AppDbContext context)
  {
    _context = context;
  }
  // Returns all todo tasks from the database
  public async Task<IEnumerable<TodoTask>> GetAllAsync()
  {
    return await _context.Tasks.ToListAsync();
  }

  // Returns a single todo task by its ID
  public async Task<TodoTask?> GetByIdAsync(int id)
  {
    return await _context.Tasks.FindAsync(id);
  }

  // Adds a new todo task to the database
  public async Task<TodoTask> AddAsync(TodoTask task)
  {
    var entry = await _context.Tasks.AddAsync(task);
    return entry.Entity;
  }

  // Updates an existing todo task in the database
  public Task<TodoTask> UpdateAsync(TodoTask task)
  {
    var entry = _context.Tasks.Update(task);
    return Task.FromResult(entry.Entity);
  }

  // Deletes a todo task from the database
  public Task<TodoTask> DeleteAsync(TodoTask task)
  {
    var entry = _context.Tasks.Remove(task);
    return Task.FromResult(entry.Entity);
  }

  // Saves changes to the database
  public async Task<bool> SaveChangesAsync()
  {
    return await _context.SaveChangesAsync() > 0;
  }
}
