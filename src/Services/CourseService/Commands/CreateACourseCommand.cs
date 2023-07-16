using MediatR;

namespace CourseService.Commands;

public record CreateACourseCommand (string Id, string CourseName, string Description, string OwnerId, DateTime CreatedAt, DateTime LastUpdate, double Price) : IRequest;