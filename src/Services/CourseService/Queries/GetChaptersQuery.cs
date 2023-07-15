using CourseService.Models;
using MediatR;

namespace CourseService.Queries;

public record GetChaptersQuery(string CourseId) : IRequest<List<Chapter>>;