using MediatR;

namespace CourseService.Commands;

public record PostChapterCommand (string Id, string CourseId, string OwnerId, DateTime CreatedAt, string ChapterName, string LittleDescription, string Content) 
    : IRequest;