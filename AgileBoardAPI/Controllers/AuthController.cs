using AgileBoardAPI.DTO;
using AgileBoardAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AgileBoardAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] RegistrationRequest registrationRequest)
        {
            try
            {
                _authService.SignUp(registrationRequest);
                return Created("User registration successful", null);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("confirm")]
        public IActionResult VerifyAccount([FromQuery] string token)
        {
            try
            {
                _authService.VerifyAccount(token);
                return Ok("Account activated");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var token = _authService.Login(loginRequest);
                return Ok(token);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _authService.Logout();
            return Ok();
        }
    }
}
