using CourseService.Models;
using MediatR;

namespace CourseService.Queries;

public record GetCourseQuery(string Id) : IRequest<Course>;