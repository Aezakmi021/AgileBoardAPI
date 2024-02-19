using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AgileBoardAPI.Services;
using AgileBoardAPI.Interfaces;

namespace AgileBoardAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize] // Optional: Use [Authorize] if you want to secure these endpoints
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("exist/email/{email}")]
        public IActionResult ExistsByEmail(string email)
        {
            var exists = _userService.ExistsByEmail(email);
            return Ok(exists.ToString());
        }

        [HttpGet("exist/username/{username}")]
        public IActionResult ExistsByUsername(string username)
        {
            var exists = _userService.ExistsByUsername(username);
            return Ok(exists.ToString());
        }

        [HttpGet("username")]
        public IActionResult GetUsername()
        {
            var username = User.Identity.Name;
            return Ok(username);
        }
    }
}
