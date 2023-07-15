using MediatR;

namespace CourseService.Commands;

public record PostALikeCommentCommand(string CommentId, string LikorId) : IRequest;
