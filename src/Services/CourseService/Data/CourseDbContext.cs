using CourseService.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Data;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<CourseDeletor> CourseDeletors { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<LikeComment> LikeComments { get; set; }
}