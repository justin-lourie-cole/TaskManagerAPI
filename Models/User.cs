using System.Text.Json.Serialization;

namespace TaskManagerAPI.Models;
public class User
{
  public int Id { get; set; }
  public string Username { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public string Role { get; set; } = "User"; // User or Admin
  [JsonIgnore]
  public ICollection<TodoTask> Tasks { get; set; } = [];
}
