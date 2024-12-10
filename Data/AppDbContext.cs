using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data;

public class AppDbContext : DbContext
{
  // Constructor that injects the database context options
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  // Represents the Users table in the database
  public DbSet<User> Users { get; set; }

  // Represents the Tasks table in the database
  public DbSet<TodoTask> Tasks { get; set; }

  // Configures the model for the database
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().HasData(
      new User { Id = 1, Username = "admin", PasswordHash = "hashedPassword", Role = "Admin" }
    );
  }
}
