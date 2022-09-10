using bcrypt = BCrypt.Net.BCrypt;
using final_api.Migrations;
using final_api.Models;
using Microsoft.EntityFrameworkCore;

namespace final_api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly PostsDbContext _context;

    public UserRepository(PostsDbContext context)
    {
        _context = context;
    }

    // PUT / edit user by user id
    public User? EditUser(User editUser)
    {
        var originalUser = _context.Users.Find(editUser.UserId);

        if (originalUser != null) {
            originalUser.Username = editUser.Username;
            
            // Do we want users to be able to edit their password? If so, HOW?
            // This is working, BUT it's also re-hashing the pw each update. Does that matter? Performance?
            // originalUser.Password = editUser.Password;
            // var passwordHash = bcrypt.HashPassword(originalUser.Password);
            // originalUser.Password = passwordHash;
            
            // Just don't include the password option?
            
            originalUser.FirstName = editUser.FirstName;
            originalUser.LastName = editUser.LastName;
            originalUser.State = editUser.State;
            originalUser.PhotoUrl = editUser.PhotoUrl;
            _context.SaveChanges();
        }

        return originalUser;
    }


    // GET / ALL users
    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    // GET / one user by user id
    public User? GetUserById(int userId)
    {
        return _context.Users.SingleOrDefault(u => u.UserId == userId);
    }

    // FOR TESTING PURPOSE ONLY (as of right now)
    // DELETE / one user by use id
    public void DeleteUserById(int userId)
    {
        var user = _context.Users.Find(userId);

        if (user != null) {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }

    // PRETTY SURE I DON'T KNOW WHAT THIS IS DOING SO IDK WHY I NEED IT
    // Referencing: https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager
    // GET / user and related posts
    // public List<User> GetUserPosts(User user)
    // {
    //     var userPosts = _context.Users.Include(user => user.Posts).ToList();

    //     return userPosts;
    // }
}