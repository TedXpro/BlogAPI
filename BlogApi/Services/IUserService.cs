
using BLOGAPI.Models;
public interface IUserService{
    public Task Register(User user);
    public Task<(String, String)> Login(Account account);
    public Task<List<User>> GetUsers();
    public Task<User> GetUser(string id);
    public Task UpdateUser(string id, User user);
    public Task DeleteUser(string id);
}