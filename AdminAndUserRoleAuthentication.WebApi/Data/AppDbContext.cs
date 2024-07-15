using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminAndUserRoleAuthentication.WebApi.Data;

public class AppDbContext(DbContextOptions options) : IdentityDbContext<AppUser>(options)
{
}
