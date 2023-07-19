using CourseService.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourseService.Controllers;

[ApiController()]
[Route("api/course")]
public class CourseController : Controller
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<IActionResult> CreateCourseController(CreateACourseCommand createACourseCommand)
    {
        await _mediator.Send(createACourseCommand);
        return Ok();
    }

    public async Task<IActionResult> UpdateCourseController(UpdateCourseCommand updateCourseCommand)
    {
        await _mediator.Send(updateCourseCommand);
        return Ok();
    }

    public async Task<IActionResult> DeleteCourseController(DeleteCourseCommand deleteCourseCommand)
    {
        await _mediator.Send(deleteCourseCommand);
        return Ok();
    }
}