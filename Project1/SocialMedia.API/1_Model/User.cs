namespace SocialMedia.API.Model;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }

    public ICollection<Tweet> Tweets { get; set; } = [];
}
