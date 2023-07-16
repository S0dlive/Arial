using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using MediatR;

namespace CourseService.Handlers.Commands;

public class CreateACourseHandler : IRequestHandler<CreateACourseCommand>
{
    private readonly CourseDbContext _courseDbContext;
    public CreateACourseHandler(CourseDbContext courseDbContext)
    {
        _courseDbContext = courseDbContext;
    }
    public async Task Handle(CreateACourseCommand request, CancellationToken cancellationToken)
    {
        Course course = new Course()
        {
            CourseName = request.CourseName,
            CreatedAt = DateTime.Now,
            Description = request.Description,
            Id = Guid.NewGuid().ToString(),
            LastUpdate = DateTime.Now,
            OwnerId = request.OwnerId,
            Price = request.Price,
        };
        _courseDbContext.Courses.Add(course);
        await _courseDbContext.SaveChangesAsync();
    }
}