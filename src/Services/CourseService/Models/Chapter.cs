namespace CourseService.Models;

public class Chapter
{
    public string Id { get; set; }
    public int Order { get; set; }
    public string ChapterName { get; set; }
    public string OwnerId { get; set; }
    public DateTime LastUpdate { get; set; }
    public string LittleDescription { get; set; }
    public string ChapterContent { get; set; }
}