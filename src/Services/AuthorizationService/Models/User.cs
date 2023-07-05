namespace AuthorizationService.Models;

public class User
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}