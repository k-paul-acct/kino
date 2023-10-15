namespace Kino.ApiClient.Dto;

public class CommentDto
{
    public int Id { get; set; }
    public int TitleId { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Text { get; set; } = null!;
    public DateTime Date { get; set; }
}