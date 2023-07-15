using CourseService.Models;
using MediatR;

namespace CourseService.Queries;

public record GetChapterQuery(string ChapterId) : IRequest<Chapter>;