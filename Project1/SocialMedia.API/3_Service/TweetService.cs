using SocialMedia.API.Model;
using SocialMedia.API.Repo;

namespace SocialMedia.API.Service;

public class TweetService : ITweetService
{
    private readonly ITweetRepo _tweetRepo;
    private readonly IUserRepo _userRepo;

    public TweetService(ITweetRepo tweetRepo, IUserRepo userRepo)
    {
        _tweetRepo = tweetRepo;
        _userRepo = userRepo;
    }

    public Tweet CreateTweet(Tweet newTweet)
    {
        if (newTweet == null)
        {
            throw new ArgumentNullException(nameof(newTweet), "Tweet object cannot be null.");
        }

        if (string.IsNullOrWhiteSpace(newTweet.Body))
        {
            throw new ArgumentException("Tweet body cannot be null or empty.");
        }

        if (newTweet.ParentId.HasValue)
        {
            var parentTweet = _tweetRepo.GetTweetById(newTweet.ParentId.Value);
            if (parentTweet == null)
            {
                throw new ArgumentException(
                    $"Parent tweet with ID {newTweet.ParentId.Value} does not exist."
                );
            }
        }

        var user = _userRepo.GetUserById(newTweet.UserId);
        if (user == null)
        {
            throw new ArgumentException($"User with ID {newTweet.UserId} does not exist.");
        }

        newTweet.User = user;
        newTweet.CreatedAt = DateTime.UtcNow;
        newTweet.Likes = 0;

        return _tweetRepo.CreateTweet(newTweet);
    }

    public Tweet GetTweetById(int id)
    {
        var tweet = _tweetRepo.GetTweetById(id);
        if (tweet == null)
        {
            throw new ArgumentException($"Tweet with ID {id} does not exist.");
        }

        return tweet;
    }

    public IEnumerable<Tweet> GetTweetsByUserId(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException("User ID must be greater than 0.");
        }

        var tweets = _tweetRepo.GetTweetsByUserId(userId);
        if (!tweets.Any())
        {
            throw new ArgumentException($"No tweets found for user with ID {userId}.");
        }

        return tweets;
    }

    public Tweet UpdateTweet(int tweetId, string newBody)
    {
        if (string.IsNullOrWhiteSpace(newBody))
        {
            throw new ArgumentException("Tweet body cannot be null or empty.");
        }

        var tweet = _tweetRepo.GetTweetById(tweetId);
        if (tweet == null)
        {
            throw new ArgumentException($"Tweet with ID {tweetId} does not exist.");
        }

        return _tweetRepo.UpdateTweet(tweetId, newBody)!;
    }

    public bool LikeTweet(int tweetId)
    {
        var tweet = _tweetRepo.GetTweetById(tweetId);
        if (tweet == null)
        {
            throw new ArgumentException($"Tweet with ID {tweetId} does not exist.");
        }

        return _tweetRepo.LikeTweet(tweetId);
    }

    public bool UnlikeTweet(int tweetId)
    {
        var tweet = _tweetRepo.GetTweetById(tweetId);
        if (tweet == null)
        {
            throw new ArgumentException($"Tweet with ID {tweetId} does not exist.");
        }

        if (tweet.Likes <= 0)
        {
            throw new InvalidOperationException(
                $"Tweet with ID {tweetId} cannot have less than 0 likes."
            );
        }

        return _tweetRepo.UnlikeTweet(tweetId);
    }

    public bool DeleteTweet(int tweetId)
    {
        var tweet = _tweetRepo.GetTweetById(tweetId);
        if (tweet == null)
        {
            throw new ArgumentException($"Tweet with ID {tweetId} does not exist.");
        }

        return _tweetRepo.DeleteTweet(tweetId);
    }

    public IEnumerable<Tweet> GetRepliesForTweet(int tweetId)
    {
        var tweet = _tweetRepo.GetTweetById(tweetId);
        if (tweet == null)
        {
            throw new ArgumentException($"Tweet with ID {tweetId} does not exist.");
        }

        var replies = _tweetRepo.GetRepliesForTweet(tweetId);
        if (!replies.Any())
        {
            throw new ArgumentException($"No replies found for tweet with ID {tweetId}.");
        }

        return replies;
    }
}
