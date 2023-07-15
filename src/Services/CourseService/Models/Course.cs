namespace CourseService.Models;

public class Course
{
    public string Id { get; set; }
    public double Price { get; set; }
    public string OwnerId { get; set; }
    public string Description { get; set; }
    public string CourseName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdate { get; set; }
}