using MediatR;

namespace CourseService.Commands;

public record DeleteChapterCommand(string ChapterId) : IRequest;