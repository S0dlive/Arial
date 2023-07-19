using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers.Commands;

public class UpdateCourseHandler : IRequestHandler<UpdateCourseCommand>
{
    private readonly CourseDbContext _courseDbContext;
    public UpdateCourseHandler(CourseDbContext courseDbContext)
    {
        _courseDbContext = courseDbContext;
    }
    public async  Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _courseDbContext.Courses.FirstOrDefaultAsync();
        if (course != null)
        {
            var newCourse = new Course()
            {
                CourseName = request.NewTile,
                Description = request.NewDescription,
                CreatedAt = course.CreatedAt,
                Id = course.Id,
                LastUpdate = DateTime.Now
            };

            _courseDbContext.Courses.Update(newCourse);
            await _courseDbContext.SaveChangesAsync();
        }
        else
        {
            Console.WriteLine("this course don't exist.");
        }
    }
}