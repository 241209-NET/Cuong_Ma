using Microsoft.EntityFrameworkCore;
using SocialMedia.API.Model;

namespace SocialMedia.API.Data;

public partial class AppContext : DbContext
{
    public AppContext() { }

    public AppContext(DbContextOptions<AppContext> options)
        : base(options) { }

    public virtual DbSet<User>? Users { get; set; } = null!;
    public virtual DbSet<Tweet>? Tweets { get; set; } = null!;
}
