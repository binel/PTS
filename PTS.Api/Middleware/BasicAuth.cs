namespace PTS.Api.Middleware;

using System.Text;
using PTS.Entity.DAL;
using PTS.Core;
using System.Security.Claims;

public class BasicAuth
{
    private readonly RequestDelegate _next;
    private readonly UserRepository _userRepository;

    public BasicAuth(RequestDelegate next, UserRepository userRepository)
    {
        _next = next;
        _userRepository = userRepository;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Response.StatusCode = 401;
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            return;
        }

        var authHeader = context.Request.Headers["Authorization"].ToString();
        if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = 401;
            return;
        }

        var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
        var decodedBytes = Convert.FromBase64String(encodedCredentials);
        var credentials = Encoding.UTF8.GetString(decodedBytes).Split(':', 2);

        var username = credentials[0];
        var password = credentials[1];

        var user = _userRepository.GetUserByUsername(username);

        if (user == null) {
            Console.WriteLine($"Failed auth - no user found with username {username}");
            context.Response.StatusCode = 401;
            return;
        }

        if (!PasswordHash.Verify(password, user.PasswordHash, (int)user.PasswordHashVersion)) {
            Console.WriteLine($"Failed auth for user {username} - invalid password");
            context.Response.StatusCode = 401;
            return;
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "User"),
            new Claim("UserId", user.Id.ToString())
        };

        var identity = new ClaimsIdentity(claims, "Basic");
        var principal = new ClaimsPrincipal(identity);

        context.User = principal;

        await _next(context);
    }
}