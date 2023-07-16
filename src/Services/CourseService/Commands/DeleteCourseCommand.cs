using MediatR;

namespace CourseService.Commands;

public record DeleteCourseCommand(string CourseId) : IRequest;