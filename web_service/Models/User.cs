namespace web_service.Models;

public class User
{
    public int UserId { get; init; }
    public string? DisplayName { get; init; }
    public string? Email { get; init; }
    public string? Auth { get; init; }
}