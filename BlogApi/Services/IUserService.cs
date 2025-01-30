
using BLOGAPI.Models;
public interface IUserService{
    public Task Register(User user);
    public Task<(String, String)> Login(Account account);
    public Task<List<User>> GetUsers();
    public Task<User> GetUser(string id);
    public Task PromoteUser(string id);
    public Task DemoteUser(string id);
    public (string accessToken, string refreshToken) RefreshToken(string refreshToken);
}