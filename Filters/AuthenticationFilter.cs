namespace MyStore.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyStore.Services;
using MyStore.Models;

public class AuthenticationFilter : Attribute, IAuthorizationFilter
{
    private readonly IUserService _userService;

    public AuthenticationFilter(IUserService userService)
    {
        _userService = userService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool isAuthenticated = false;

        if(context.HttpContext.Request.Headers.ContainsKey("AUTHENTICATION-KEY")) {

            string tokenValue = context.HttpContext.Request.Headers["AUTHENTICATION-KEY"].ToString();

            User user = _userService.FindByToken(tokenValue);

            if(user != null) {
                isAuthenticated = true;

                context.HttpContext.Items.Add("user", user);
            }
        }

        if(!isAuthenticated) {
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("message", "Unauthenticated");

            context.Result = new UnauthorizedObjectResult(payload);
        }
    }
}