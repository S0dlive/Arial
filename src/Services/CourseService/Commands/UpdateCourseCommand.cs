using MediatR;

namespace CourseService.Commands;

public record UpdateCourseCommand(string CourseId, string? NewTile, string? NewDescription) : IRequest;
