using BLOGAPI.Models;

public class UserService : IUserService{
    public async Task<User> Register(User user){
        return await Task.FromResult(user);
    }

    public async Task<User> Login(Account account){
        return await Task.FromResult(new User());
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