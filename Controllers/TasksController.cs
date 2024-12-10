using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Requires authentication for all endpoints
public class TasksController : ControllerBase
{
  private readonly ITaskService _taskService;

  public TasksController(ITaskService taskService)
  {
    _taskService = taskService;
  }

  [HttpGet]
  [Authorize(Policy = "RequireUserRole")] // Users and Admins can view tasks
  public async Task<IActionResult> GetAllTasks()
  {
    var tasks = await _taskService.GetAllTasksAsync();
    return Ok(tasks);
  }

  [HttpGet("{id}")]
  [Authorize(Policy = "RequireUserRole")]
  public async Task<IActionResult> GetTaskById(int id)
  {
    var task = await _taskService.GetTaskByIdAsync(id);
    if (task == null) return NotFound();
    return Ok(task);
  }

  [HttpPost]
  [Authorize(Policy = "RequireUserRole")]
  public async Task<IActionResult> CreateTask([FromBody] TodoTask task)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    // Set the UserId from the authenticated user's claims
    var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
    task.UserId = userId;

    var createdTask = await _taskService.CreateTaskAsync(task);
    if (createdTask == null) return StatusCode(500, "Failed to create task");

    return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
  }

  [HttpPut("{id}")]
  [Authorize(Policy = "RequireUserRole")]
  public async Task<IActionResult> UpdateTask(int id, [FromBody] TodoTask task)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);

    // Check if user owns the task or is admin
    var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");
    var existingTask = await _taskService.GetTaskByIdAsync(id);

    if (existingTask == null) return NotFound();
    if (existingTask.UserId != userId && !User.IsInRole("Admin"))
      return Forbid();

    task.Id = id;
    task.UserId = existingTask.UserId; // Preserve original owner
    var updatedTask = await _taskService.UpdateTaskAsync(task);
    if (updatedTask == null) return StatusCode(500, "Failed to update task");

    return NoContent();
  }

  [HttpDelete("{id}")]
  [Authorize(Policy = "RequireAdminRole")] // Only admins can delete tasks
  public async Task<IActionResult> DeleteTask(int id)
  {
    var success = await _taskService.DeleteTaskAsync(id);
    if (!success) return NotFound();

    return NoContent();
  }
}
