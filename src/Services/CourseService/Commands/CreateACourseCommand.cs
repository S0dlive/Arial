using MediatR;

namespace CourseService.Commands;

public record CreateACourseCommand (string Id, string CourseName, string Descriptions, string OwnerId, DateTime CreatedAt) : IRequest;