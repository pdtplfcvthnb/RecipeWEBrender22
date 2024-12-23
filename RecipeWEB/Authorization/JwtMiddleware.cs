using Microsoft.Extensions.Options;
using RecipeWEB.Helpers;
using RecipeWEB.Models;

namespace RecipeWEB.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, RecipeContext context1, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null)
            {
                var user = await context1.Users.FindAsync(accountId.Value);

                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }
            await _next(context);
        }
    }
}
