using final_api.Repositories;
using final_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace final_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;

    private readonly IAuthService _authService;

    public AuthController(ILogger<AuthController> logger, IAuthService service, IUserRepository userRepository) {
        _logger = logger;
        _authService = service;
    }

    // POST / create a new user
    [HttpPost]
    [Route("signup")]
    public ActionResult CreateNewUser(User user)
    {
        if (user == null || !ModelState.IsValid) {
            return BadRequest();
        }

        _authService.CreateUser(user);
        return NoContent();
    }

    // POST / using response to sign in
    [HttpPost]
    [Route("signin")]
    public ActionResult<string> SignIn(SignInRequest request)
    {
        // Since we're passing in the model as a parameter instead of strings, which method of validating the request data is correct/better?
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password) || request == null || !ModelState.IsValid)
        {
            return BadRequest();
        }

        var token = _authService.SignIn(request);

        if (string.IsNullOrWhiteSpace(token) || token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    
}