using AgileBoardAPI.Security;
using AgileBoardAPI.Services;

namespace AgileBoardAPI.Middleware
{
    public class JwtAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserPrincipalService userPrincipalService, JwtProvider jwtProvider)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null && jwtProvider.IsTokenValid(token))
            {
                var username = jwtProvider.GetUsernameFromToken(token);
                var user = userPrincipalService.LoadUserByUsername(username); // Adapt to async if necessary
                                                                              // Create ClaimsPrincipal and set to HttpContext.User
            }

            await _next(context);
        }
    }


}
