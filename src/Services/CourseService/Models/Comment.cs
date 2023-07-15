namespace CourseService.Models;

public class Comment
{
    public string Id { get; set; }
    public string CommentContent { get; set; }
    public short StarsNumber { get; set; }
    public DateTime PostedAt { get; set; }
}