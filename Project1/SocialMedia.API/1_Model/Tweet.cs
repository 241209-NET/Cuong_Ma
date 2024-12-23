namespace SocialMedia.API.Model;

public class Tweet
{
    public int Id { get; set; }
    public required string Body { get; set; }
    public int Likes { get; set; }
    public int? ParentId { get; set; }
    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
    public required User User { get; set; }
    public ICollection<Tweet>? Replies { get; set; }
}
