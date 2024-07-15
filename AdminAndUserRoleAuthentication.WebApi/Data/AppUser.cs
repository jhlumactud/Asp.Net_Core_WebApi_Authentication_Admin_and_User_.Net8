using Microsoft.AspNetCore.Identity;

namespace AdminAndUserRoleAuthentication.WebApi.Data;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? Address { get; set; }
}
