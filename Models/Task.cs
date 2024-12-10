namespace TaskManagerAPI.Models;

public class TodoTask
{
  public int Id { get; set; }
  public string Title { get; set; } = string.Empty;
  public string Description { get; set; } = string.Empty;
  public bool Completed { get; set; }
  public int UserId { get; set; }
  public User User { get; set; } = new User();
}

