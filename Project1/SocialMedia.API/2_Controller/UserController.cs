using Microsoft.AspNetCore.Mvc;

namespace SocialMedia.API.Controller;

using SocialMedia.API.Model;
using SocialMedia.API.Service;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
}
