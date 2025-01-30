using BLOGAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using MongoDB.Driver;
using BCrypt.Net;
using System.Security.Claims;

public class UserService : IUserService{

    private readonly IMongoCollection<User> _users;
    private readonly JwtService _jwtService;

    public UserService(IConfiguration configuration, JwtService jwtService){
            var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
            _users = database.GetCollection<User>(configuration["MongoDbSettings:UserCollectionName"]);
        _jwtService = jwtService;
    }

    public async Task Register(User user){
        // check if email is not empty
        if (user.Email == null || user.Email == ""){
            throw Error.ErrEmailEmpty;
        }

        // check if email is valid
        try{
            MailAddress m = new MailAddress(user.Email);
        }
        catch (FormatException){
            throw Error.ErrEmailInvalid;
        }

        // check if password is valid
        var err = ValidationService.ValidatePassword(user.Password);
        if (err != Error.noError){
            throw err;
        }     

        // check if user already exists
        if (_users.Find(u => u.Email == user.Email).FirstOrDefault() != null){
            throw Error.ErrUserExists;
        }

        // check if no one is in the db
        if (_users.Find(u => true).FirstOrDefault() == null){
            user.Role = "admin";
        }
        else{
            user.Role = "user";
        }
        // hash password
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        // make sure verified is false
        user.Verified = false;

        // insert user into database
        _users.InsertOne(user);

        // send verification email
        // unimplemented

        return;

    }

    public async Task<(String, String)> Login(Account account){
        if (account.Email == null || account.Email == ""){
            throw Error.ErrEmailEmpty;
        }
        if (account.Password == null || account.Password == ""){
            throw Error.ErrPasswordEmpty;
        }

        User user = _users.Find(u => u.Email == account.Email).FirstOrDefault();
        if (user == null){
            throw Error.ErrUserNotFound;
        }

        if (!BCrypt.Net.BCrypt.Verify(account.Password, user.Password)){
            throw Error.ErrIncorrectEmailPassword;
        }

        
        string accessToken = _jwtService.GenerateToken(user, 15);
        string refreshToken = _jwtService.GenerateToken(user, 7 * 24 * 60);

        return (accessToken, refreshToken);
    }

    public async Task<List<User>> GetUsers(){
        return await _users.Find(u => true).ToListAsync();
    }

    public async Task<User> GetUser(string id){
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task PromoteUser(string id){
        await _users.UpdateOneAsync(u => u.Id == id, Builders<User>.Update.Set(u => u.Role, "admin"));
    }

    public async Task DemoteUser(string id){
        await _users.UpdateOneAsync(u => u.Id == id, Builders<User>.Update.Set(u => u.Role, "user"));
    }

    public (string accessToken, string refreshToken) RefreshToken(string refreshToken) {
        var principal = _jwtService.ValidateToken(refreshToken);
        if (principal == null) throw Error.ErrInvalidToken;

        var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) throw Error.ErrInvalidToken;

        var user = _users.Find(u => u.Id == userId).FirstOrDefault();
        if (user == null) throw Error.ErrUserNotFound;

        // Generate new tokens
        string newAccessToken = _jwtService.GenerateToken(user, 15);
        string newRefreshToken = _jwtService.GenerateToken(user, 7 * 24 * 60);

        return (newAccessToken, newRefreshToken);
    }
}