using AuthorizationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Data;

public class AuthorizationDbContext : DbContext
{
    public AuthorizationDbContext(DbContextOptions<AuthorizationDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<User> Users { get; set; }
}