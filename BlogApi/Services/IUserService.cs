
using BLOGAPI.Models;
public interface IUserService{
    public Task Register(User user);
    public Task<(String, String)> Login(Account account);
    public Task<List<User>> GetUsers();
    public Task<User> GetUser(int id);
    public Task<User> UpdateUser(int id, User user);
    public Task<User> DeleteUser(int id);
}