using final_api.Models;

namespace final_api.Repositories;

public interface IAuthService
{
    User CreateUser(User user);

    string SignIn(SignInRequest request); 

}