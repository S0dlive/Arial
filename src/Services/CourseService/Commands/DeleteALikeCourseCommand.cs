using MediatR;

namespace CourseService.Commands;

public record DeleteALikeCourseCommand(string LikeId) : IRequest;