using CourseService.Data;
using CourseService.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers;

[ApiController()]
[Route("api/course")]
public class CourseController : Controller
{
    private readonly CourseDbContext _courseDbContext;
    public CourseController(CourseDbContext courseDbContext)
    {
        _courseDbContext = courseDbContext;
    }

    [HttpPost()]
    [Authorize]
    public async Task<IActionResult> CreateCourseController(CreateCourseRequest request)
    {
        var userId = User.Claims.FirstOrDefault(t => t.Type == "sub");
        if (userId != null)
        {
            var course = new Course()
            {
                Id = Guid.NewGuid().ToString(),
                CourseName = request.CourseName,
                CreatedAt = DateTime.Now, 
                Description = request.Description,
                LastUpdate = DateTime.Now,
                OwnerId = userId.Value,
            };

            if (course.Description.Length < 20)
            {
                return BadRequest(new Error("400", "description don't have less than 20 char"));
            }
            else if (course.CourseName.Length > 20)
            {
                return BadRequest(new Error("400", "course name don't have most than 20 char"));
            }
            _courseDbContext.Courses.Add(course);
            await _courseDbContext.SaveChangesAsync();
            return Ok();
        }
        return BadRequest("the sub is null. . .");
    }
}

public record CreateCourseRequest(string CourseName, double Price, string Description);

public record Error(string ErrorStatusCode, string ErrorContent);
