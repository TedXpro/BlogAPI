using BLOGAPI.Models;

public class UserService : IUserService{
    public async Task Register(User user){
        await Task.FromResult(new User());
        // check if email is not empty
        // if (user.Email == null || user.Email == ""){
        //     throw new Exception("Email is required");
        // }
    }

    public async Task<(String, String)> Login(Account account){
        return ("access_token", "refresh");
    }

    public async Task<List<User>> GetUsers(){
        return await Task.FromResult(new List<User>());
    }

    public async Task<User> GetUser(int id){
        return await Task.FromResult(new User());
    }

    public async Task<User> UpdateUser(int id, User user){
        return await Task.FromResult(user);
    }

    public async Task<User> DeleteUser(int id){
        return await Task.FromResult(new User());
    }
}