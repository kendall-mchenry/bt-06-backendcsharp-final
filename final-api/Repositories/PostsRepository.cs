using System.IdentityModel.Tokens.Jwt;
using System.Text;
using final_api.Migrations;
using final_api.Models;
using Microsoft.IdentityModel.Tokens;

namespace final_api.Repositories;

public class PostsRepository : IPostsRepository 
{
    private readonly PostsDbContext _context;

    public PostsRepository(PostsDbContext context) {
        _context = context;
    }

    // POST / NEW post
    public Post CreatePost(Post newPost)
    {
        // Do I need to add anything in here that links the User data to the post? OR does this happen in the front end by accessing the signed in user in local storage?

        // activeUser = HOW TO FIND SIGNED IN USER?;
        // createPost.User = activeUser;
        // createPost.UserId = activeUser.UserId;

        // var tokenHandler = new JwtSecurityTokenHandler();
        //     var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //     tokenHandler.ValidateToken(token, new TokenValidationParameters
        //     {
        //         ValidateIssuerSigningKey = true,
        //         IssuerSigningKey = new SymmetricSecurityKey(key),
        //         ValidateIssuer = false,
        //         ValidateAudience = false,
        //         // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
        //         ClockSkew = TimeSpan.Zero
        //     }, out SecurityToken validatedToken);

        //     var jwtToken = (JwtSecurityToken)validatedToken;
        //     var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

        
        newPost.PostedDate = DateTime.Now.ToString("MM/dd/yyyy, hh:mm:ss tt");

        _context.Posts.Add(newPost);
        _context.SaveChanges();
        return newPost;

    }

    // DELETE / delete post by id
    public void DeletePostById(int postId)
    {
        var post = _context.Posts.Find(postId);

        if (post != null) {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

    }

    // PUT / edit post by id
    public Post? EditPost(Post editPost)
    {
        var originalPost = _context.Posts.Find(editPost.PostId);

        if (originalPost != null) {
            originalPost.Title = editPost.Title;
            originalPost.Content = editPost.Content;
            _context.SaveChanges();
        }

        return originalPost;
    }

    // GET / ALL posts
    public IEnumerable<Post> GetAllPosts()
    {
        return _context.Posts.ToList();
    }

    // GET / one post by post id
    public Post? GetPostByPostId(int postId)
    {
        return _context.Posts.SingleOrDefault(p => p.PostId == postId);
    }


    // GET / ALL posts by user id
    public List<Post>? GetPostsByUserId(int userId)
    {
        // How do I use LINQ here to get all posts by user ID & add them to a list of posts?

        var postList = _context.Posts.Where(u => u.UserId == userId).ToList();

        return postList;
    }    
    
}