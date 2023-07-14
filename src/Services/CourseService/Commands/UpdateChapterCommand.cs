using MediatR;

namespace CourseService.Commands;

public record UpdateChapterCommand(string CourseId, string NewContent) : IRequest;