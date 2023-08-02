namespace CourseService.Models;

public class Chapter
{
    public string Id { get; set; }
    public string CourseId { get; set; }
    public bool IsFree { get; set; } = false;
    public int Order { get; set; }
    public string ChapterName { get; set; }
    public string OwnerId { get; set; }
    public DateTime LastUpdate { get; set; }
    public string LittleDescription { get; set; }
    public string ChapterContent { get; set; }
}