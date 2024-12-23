namespace SocialMedia.API.Service;

using System.Collections.Generic;
using SocialMedia.API.DTO;
using SocialMedia.API.Model;

public interface IUserService
{
    User CreateUser(UserInDTO newUser);
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
    User? GetUserByUsername(string username);
    User? DeleteUserById(int id);
}
