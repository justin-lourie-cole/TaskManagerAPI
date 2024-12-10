using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TaskManagerAPI.Authorization;

public class RoleRequirement : IAuthorizationRequirement
{
  public string Role { get; }

  public RoleRequirement(string role)
  {
    Role = role;
  }
}

public class RoleHandler : AuthorizationHandler<RoleRequirement>
{
  protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
  {
    var userRole = context.User.FindFirst(ClaimTypes.Role)?.Value
        ?? context.User.FindFirst("Role")?.Value;

    if (userRole == requirement.Role || userRole == "Admin")
    {
      context.Succeed(requirement);
    }

    return Task.CompletedTask;
  }
}