using SocialMedia.API.Model;

namespace SocialMedia.API.Repo;

public interface IUserRepo
{
    User CreateUser(User newUser);
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    User? GetUserByUsername(string username);
    User? DeleteUserById(int id);
}
