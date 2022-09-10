using final_api.Models;
using final_api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace final_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly ILogger<PostsController> _logger;

    private readonly IPostsRepository _postRepository;

    public PostsController(ILogger<PostsController> logger, IPostsRepository repository)
    {
        _logger = logger;
        _postRepository = repository;
    }

    // POST / create new post
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Post> CreateNewPost(Post createPost)
    {
        if (HttpContext.User == null) {
            return Unauthorized();
        }
        
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        
        var userId = Int32.Parse(userIdClaim.Value);

        if (userId == null) {
            return Unauthorized();
        }

        createPost.UserId = userId;

        if (!ModelState.IsValid || createPost == null) {
            return BadRequest();
        }

        var newPost = _postRepository.CreatePost(createPost);
        return Created(nameof(GetPostById), newPost);

    }
    
    // GET one post by post id
    [HttpGet]
    [Route("{postId:int}")]
    public ActionResult<Post> GetPostById(int postId)
    {
        var post = _postRepository.GetPostByPostId(postId);

        if (post == null) {
            return NotFound();
        }

        return Ok(post);
    }

    // GET ALL posts 
    [HttpGet]
    public ActionResult<IEnumerable<Post>> GetAllPosts() 
    {
        return Ok(_postRepository.GetAllPosts());
    }

    // GET ALL posts by user id (for others to view the user page)
    [HttpGet]
    [Route("user/{userId:int}")]
    public ActionResult<IEnumerable<Post>> GetPostsByUserId(int userId)
    {
        var userPosts = _postRepository.GetPostsByUserId(userId);

        if (userPosts == null) {
            return NotFound();
        }
        
        return Ok(userPosts);
    }

    // GET / ALL current user posts
    [HttpGet]
    [Route("user/current")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<IEnumerable<Post>> GetCurrentUserPosts(int userId)
    {
        if (HttpContext.User == null) {
            return Unauthorized();
        }
        
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        
        userId = Int32.Parse(userIdClaim.Value);

        if (userId == null) {
            return Unauthorized();
        }

        var userPosts = _postRepository.GetPostsByUserId(userId);

        if (userPosts == null) {
            return NotFound();
        }
        
        return Ok(userPosts);
    }

    // PUT / edit post by post id
    [HttpPut]
    [Route("edit/{postId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Post> EditPost(Post editPost)
    {
        if (HttpContext.User == null) {
            return Unauthorized();
        }
        
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        
        var userId = Int32.Parse(userIdClaim.Value);
        
        if (!ModelState.IsValid || editPost == null) {
            return BadRequest();
        }

        if (userId == editPost.UserId) {
            return Ok(_postRepository.EditPost(editPost));
        } else {
            return Unauthorized();
        }

        // return Ok(_postRepository.EditPost(editPost));
    }

    // DELETE / post by post id
    [HttpDelete]
    [Route("{postId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult DeletePost(int postId)
    {
        if (HttpContext.User == null) {
            return Unauthorized();
        }
        
        var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
        
        var userId = Int32.Parse(userIdClaim.Value);

        var deletePost = _postRepository.GetPostByPostId(postId);

        if (userId == deletePost.UserId) {
            _postRepository.DeletePostById(postId);
            return NoContent();
        } else {
            return Unauthorized();
        }
        
        // _postRepository.DeletePostById(postId);
        // return NoContent();
    }


}