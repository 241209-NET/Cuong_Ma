using SocialMedia.API.DTO;
using SocialMedia.API.Model;

namespace SocialMedia.API.Util;

public static class Utilities
{
    public static User UserDTOToObject(UserInDTO userDTO)
    {
        return new User { Username = userDTO.Username, Password = userDTO.Password };
    }
}
