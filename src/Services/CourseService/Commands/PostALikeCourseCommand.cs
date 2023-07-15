using MediatR;

namespace CourseService.Commands;

public record PostALikeCourseCommand(string CourseId, string LikorId) : IRequest;