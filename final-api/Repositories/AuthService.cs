using final_api.Migrations;
using final_api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using bcrypt = BCrypt.Net.BCrypt;

namespace final_api.Repositories;

public class AuthService : IAuthService
{
    private static PostsDbContext _context;
    
    private static IConfiguration _config;

    public AuthService(PostsDbContext context, IConfiguration config) {
        _context = context;
        _config = config;
    }

    public User CreateUser(User user)
    {
        var passwordHash = bcrypt.HashPassword(user.Password);
        user.Password = passwordHash;

        user.CreatedDate = DateTime.Now.ToString("MM/dd/yyyy");

        _context.Add(user);
        _context.SaveChanges();
        return user;
    }

    public string SignIn(SignInRequest request)
    {
        var user = _context.Users.SingleOrDefault(x => x.Username == request.Username);
        var verified = false;

        if (user != null)
        {
            verified = bcrypt.Verify(request.Password, user.Password);
        }

        if (user == null || !verified)
        {
            return String.Empty;
        }

        return BuildToken(user);
    }

    private string BuildToken(User user)
    {
        var secret = _config.GetValue<String>("TokenSecret");
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));

        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new Claim[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
        new Claim("UserId", user.UserId.ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username ?? ""),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? "")
        };

        // Create token
        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCredentials);

        // Encode token
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }

    // SIGN OUT METHOD? --OR-- is this done on the frontend to forget the token?
    // https://medium.com/devgorilla/how-to-log-out-when-using-jwt-a8c7823e8a6
}