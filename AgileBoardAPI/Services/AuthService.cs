using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using AgileBoardAPI.Data; // Import your data-related namespaces
using AgileBoardAPI.Models; // Import your model-related namespaces
using AgileBoardAPI.DTO; // Import your DTO-related namespaces
using AgileBoardAPI.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AgileBoardAPI.Interfaces;

namespace AgileBoardAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfirmationTokenService _confirmationTokenService;
        private readonly IMailService _mailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(
            IUserService userService,
            IConfirmationTokenService confirmationTokenService,
            IMailService mailService,
            IHttpContextAccessor httpContextAccessor,
            IJwtProvider jwtProvider)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _confirmationTokenService = confirmationTokenService ?? throw new ArgumentNullException(nameof(confirmationTokenService));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        }


        public async Task SignUp(RegistrationRequest registrationRequest)
        {
            string confirmationURL = "http://localhost:5000/api/auth/confirm?token=";

            // Check if username or email are already in use
            bool usernameExists = await _userService.CheckIfUsernameExists(registrationRequest.Username);
            bool emailExists = await _userService.CheckIfEmailExists(registrationRequest.Email);

            if (usernameExists)
            {
                throw new InvalidOperationException($"Username {registrationRequest.Username} is already taken.");
            }
            else if (emailExists)
            {
                throw new InvalidOperationException($"Email {registrationRequest.Email} is already in use.");
            }
            else
            {
                // Save new user to database
                User user = await _userService.SaveNewUser(registrationRequest);

                // Generate and save token for the given user
                ConfirmationToken token = await _confirmationTokenService.GenerateConfirmationToken(user);

                // Send confirmation email
                await _mailService.SendConfirmationMail(new Mail(
                    "Please activate your account",
                    registrationRequest.Email,
                    $"Click on the link below to activate your account: {confirmationURL}{token.Token}"
                ));
            }
        }

        public string Login(LoginRequest loginRequest)
        {
            var user = _userService.FindByUsername(loginRequest.Username);
            if (user == null || !_userService.ValidatePassword(user, loginRequest.Password))
            {
                throw new AuthenticationException("Invalid credentials");
            }

            var claims = _jwtProvider.GetClaims(user);
            var token = _jwtProvider.GenerateJwtToken(claims);

            return token;
        }

        public void VerifyAccount(string token)
        {
            var confirmationToken = _confirmationTokenService.FindByToken(token);
            if (confirmationToken == null)
            {
                throw new InvalidOperationException("Invalid token");
            }

            _userService.EnableUser(confirmationToken.User);
        }

        public void Logout()
        {
            _authenticationManager.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
