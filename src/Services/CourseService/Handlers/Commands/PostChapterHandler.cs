using CourseService.Commands;
using CourseService.Data;
using CourseService.Models;
using MediatR;

namespace CourseService.Handlers.Commands;

public class PostChapterHandler : IRequestHandler<PostChapterCommand>
{
    private readonly CourseDbContext _courseDbContext;
    public PostChapterHandler(CourseDbContext courseDbContext)
    {
        _courseDbContext = courseDbContext;
    }
    public async Task Handle(PostChapterCommand request, CancellationToken cancellationToken)
    {
        var chapter = new Chapter()
        {
            Id = Guid.NewGuid().ToString(),
            ChapterContent = request.Content,
            ChapterName = request.ChapterName,
            LastUpdate = DateTime.Now,
            OwnerId = request.OwnerId,
            LittleDescription = request.LittleDescription
        };

        _courseDbContext.Chapters.Add(chapter);
        await _courseDbContext.SaveChangesAsync();
    }
}