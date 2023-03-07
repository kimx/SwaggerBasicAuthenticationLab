using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace SwaggerBasicAuthenticationLab;

/// <summary>
/// 參考來源:https://www.c-sharpcorner.com/article/basic-authentication-in-swagger-open-api-net-5/
/// 1.實作AuthenticationHandler
/// </summary>
public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{


    #region Constructor  
    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }
    #endregion

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorization = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorization))
            return AuthenticateResult.Fail($"Authentication failed: Authorization empty");
        var authHeader = AuthenticationHeaderValue.Parse(authorization);
        var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
        string username = credentials.FirstOrDefault();
        var password = credentials.LastOrDefault();
        var result = username == "kim" && password == "kim123";
        if (!result)
            return AuthenticateResult.Fail($"Authentication failed: Invalid credentials");

        var claims = new[] {
                new Claim(ClaimTypes.Name, username)
            };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

}