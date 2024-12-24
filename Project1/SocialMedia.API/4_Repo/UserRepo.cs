#nullable disable
using SocialMedia.API.Model;

namespace SocialMedia.API.Repo;

public class UserRepo : IUserRepo
{
    private readonly Data.AppContext _appContext;

    public UserRepo(Data.AppContext appContext) => _appContext = appContext;

    public User CreateUser(User newUser)
    {
        _appContext.Users.Add(newUser);
        _appContext.SaveChanges();
        return newUser;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _appContext.Users.ToList();
    }

    public User GetUserById(int id)
    {
        return _appContext.Users.Find(id);
    }

    public User GetUserByUsername(string username)
    {
        return _appContext.Users.FirstOrDefault(u => u.Username == username);
    }

    public User DeleteUserById(int id)
    {
        var user = GetUserById(id);
        _appContext.Users.Remove(user);
        _appContext.SaveChanges();
        return user;
    }
}
