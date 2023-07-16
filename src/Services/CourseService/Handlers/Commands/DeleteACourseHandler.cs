using CourseService.Commands;
using CourseService.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CourseService.Handlers.Commands;

public class DeleteACourseHandler : IRequestHandler<DeleteCourseCommand>
{
    private readonly CourseDbContext _courseDbContext;
    public DeleteACourseHandler(CourseDbContext courseDbContext)
    {
        _courseDbContext = courseDbContext;
    }
    public async Task Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var courseIsExist = await _courseDbContext.Courses.FirstOrDefaultAsync(t => t.Id == request.CourseId);
        if (courseIsExist != null)
        {
            _courseDbContext.Courses.Remove(courseIsExist);
            await _courseDbContext.SaveChangesAsync();
        }
    }
}