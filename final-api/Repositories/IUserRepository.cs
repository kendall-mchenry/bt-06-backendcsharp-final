using final_api.Models;

namespace final_api.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();

    User? GetUserById(int userId);

    User? EditUser(User editUser);

    // Only adding this to clean up my user data
    void DeleteUserById(int userId);

    // Add anything here about getting the posts for a user?
}