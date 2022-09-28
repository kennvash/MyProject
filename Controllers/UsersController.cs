namespace MyStore.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using MyStore.Models;
using MyStore.Services;
using MyStore.Operations.Users;
using MyStore.Exceptions;
using MyStore.Filters;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AuthenticationService _authenticationService;
    private readonly ValidateRegister _validateRegister;

    public UsersController(IUserService userService, ValidateRegister validateRegister, AuthenticationService authenticationService)
    {
        _userService = userService;
        _validateRegister = validateRegister;
        _authenticationService = authenticationService;
    }

    [HttpPost("logout")]
    [ServiceFilter(typeof(AuthenticationFilter))]
    public ActionResult Logout()
    {
        User user = (User)HttpContext.Items["user"];
        _authenticationService.Logout(user);

        Dictionary<string, string> msg = new Dictionary<string, string>();
        msg["message"] = "Successfully logged out";

        return Ok(msg);
    }

    [HttpPost("login")]
    public ActionResult Login([FromBody] object payload)
    {
        try
        {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            string username = hash["username"].ToString();
            string password = hash["password"].ToString();

            string token = _authenticationService.Login(username, password);

            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["token"] = token;

            return Ok(msg);
        }
        catch(InvalidLoginException e)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Invalid login";

            return StatusCode(StatusCodes.Status404NotFound, msg);
        }
        catch(Exception e)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }

    [HttpPost("register")]
    public ActionResult Register([FromBody]object payload)
    {
        try
        {
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            _validateRegister.InitializeParameters(hash);
            _validateRegister.run();

            if(_validateRegister.HasErrors) {
                return UnprocessableEntity(_validateRegister.Payload);
            } else {
                return Ok(_userService.Register(hash["username"].ToString(), hash["password"].ToString(), hash["role"].ToString()));
            }
        }
        catch(Exception e)
        {
            Dictionary<string, string> msg = new Dictionary<string, string>();
            msg["message"] = "Something went wrong";

            return StatusCode(StatusCodes.Status500InternalServerError, msg);
        }
    }    
}