using System.Diagnostics;
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
            return Ok(course);
        }
        return BadRequest(new Error("400","the sub is null. . ."));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetCoursesController([FromQuery]int limit)
    {
        if (limit > 100)
        {
            return BadRequest(new Error("400", "you don't can have more of 100 course."));
        }

        return Ok(_courseDbContext.Courses.Take(limit).ToList());
    }

    [HttpPatch("{courseId}")]
    [Authorize]
    public async Task<IActionResult> UpdateAStatusCourse(string courseId, [FromBody] UpdateCourseRequest request)
    {
        var userId = User.Claims.FirstOrDefault(t => t.Type == "sub").Value;
        var course = _courseDbContext.Courses.FirstOrDefault(t => t.Id == courseId);
        if (course == null)
        {
            return NotFound(new Error("404", "this course doesn't exist."));
        }

        if (userId == null)
        {
            return BadRequest(new Error("400", "this token is not good"));
        }
        
        if (course.OwnerId != userId)
        {
            return Unauthorized(new Error("401", "you don't have the authorization for update this course"));
        }

        course.CourseName = request.NewCourseName;
        course.Description = request.NewDescription;
        course.LastUpdate = DateTime.Now;
        course.Price = request.NewPrice;

        _courseDbContext.Courses.Update(course);
        await _courseDbContext.SaveChangesAsync();
        return Ok(course);
    }

    [HttpPost("{courseId}")]
    public async Task<IActionResult> DeleteACourseController(string courseId)
    {
        var userId = User.Claims.FirstOrDefault(t => t.Type == "sub").Value;
        var course = _courseDbContext.Courses.FirstOrDefault(t => t.Id == courseId);
        if (course == null)
        {
            return NotFound(new Error("404", "this course don't exist"));
        }

        if (userId != course.OwnerId)
        {
            return Unauthorized(new Error("401", "you don't have the authorization for delete this course."));
        }

        var deletor = new CourseDeletor()
        {
            Id = Guid.NewGuid().ToString(),
            CourseId = courseId,
            ExpireIn = DateTime.Now.AddMinutes(10)
        };

        _courseDbContext.CourseDeletors.Add(deletor);
        await _courseDbContext.SaveChangesAsync();

        return Ok(deletor);
    }

    [HttpDelete("{deletorId}")]
    [AllowAnonymous]
    public async Task<IActionResult> DeletorController(string deletorId)
    {
        var deletor = _courseDbContext.CourseDeletors.FirstOrDefault(t => t.Id == deletorId);
        if (deletor.ExpireIn < DateTime.Now)
        {
            return Unauthorized(new Error("404", "This deletor is not good."));
        }
        
        _courseDbContext.Courses.Remove(_courseDbContext.Courses.FirstOrDefault(t => t.Id == deletor.CourseId));
        _courseDbContext.CourseDeletors.Remove(deletor);
        await _courseDbContext.SaveChangesAsync();
        return Ok();
    }
}

public record UpdateCourseRequest(string NewCourseName, double NewPrice, string NewDescription);
public record CreateCourseRequest(string CourseName, double Price, string Description);
public record Error(string ErrorStatusCode, string ErrorContent);