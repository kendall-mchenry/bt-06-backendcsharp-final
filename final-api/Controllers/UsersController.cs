using final_api.Repositories;
using final_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace final_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;

    private readonly IUserRepository _userRepository;

    public UsersController(ILogger<UsersController> logger, IUserRepository repository) {
        _logger = logger;
        _userRepository = repository;
    }

    // GET / ALL users
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAllUsers()
    {
        return Ok(_userRepository.GetAllUsers());
    }

    // GET / one user by user id
    [HttpGet]
    [Route("{userId:int}")]
    // If the userID is in the route, does that pose a security risk in anyway?
    public ActionResult<User> GetUserById(int userId)
    {
        var user = _userRepository.GetUserById(userId);

        if (user == null) {
            return NotFound();
        }

        return Ok(user);
    }

    // GET / to get user details from the token
    [HttpGet]
    [Route("current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetCurrentUser()
    {
        // Would either of these links be more secure?
        // https://jasonwatmore.com/post/2021/12/14/net-6-jwt-authentication-tutorial-with-example-api
        // https://www.blakepell.com/blog/read-jwt-token-claims-in-aspnet-core
        // https://stackoverflow.com/questions/67310543/how-use-a-jwt-token-to-retrieve-current-user-data-in-net-core-api

        if (HttpContext.User == null) {
            return Unauthorized();
        }
        
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        
        var userId = Int32.Parse(userIdClaim.Value);

        var user = _userRepository.GetUserById(userId);

        if (user == null) {
            return Unauthorized();
        }

        return Ok(user);
    }

    // SHOULD I MAKE IT SO A USER CAN UPDATE THEIR PASSWORD?
    // PUT / edit user by user id
    [HttpPut]
    [Route("current/edit")] // OR combo of these two?
    // [Route("{userId:int}")] // is the user Id needed for the client?
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> EditUser(User editUser)
    {
        // Does this need additional authentication so ONLY the signed in user can edit their details? -OR- will this only be accessible by the user that's signed in anyway? 

        // THIS WORKS BUT IDK IF IT'S RIGHT?!

        if (HttpContext.User == null) {
            return Unauthorized("Unable to find user, returns null");
        }
        
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        
        var userId = Int32.Parse(userIdClaim.Value);

        if (!ModelState.IsValid || editUser == null) {
            return BadRequest();
        }

        if (userId == editUser.UserId) {
            return Ok(_userRepository.EditUser(editUser));
        } else {
            return Unauthorized("Not current user, can't edit");
        }       
    }


    // FOR TESTING PURPOSE ONLY (as of right now)
    // DELETE / user by user id
    [HttpDelete]
    [Route("{userId:int}")]
    public ActionResult DeleteUser(int userId)
    {
        _userRepository.DeleteUserById(userId);
        return NoContent();
    }
}