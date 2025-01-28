
using BLOGAPI.Models;
public interface IUserService{
    public Task<User> Register(User user);
    public Task<User> Login(Account account);
    public Task<List<User>> GetUsers();
    public Task<User> GetUser(int id);
    public Task<User> UpdateUser(int id, User user);
    public Task<User> DeleteUser(int id);
}