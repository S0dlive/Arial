using CourseService.Commands;
using MediatR;

namespace CourseService.Handlers.Commands;

public class CreateACourseHandler : IRequestHandler<CreateACourseCommand>
{
    public Task Handle(CreateACourseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}