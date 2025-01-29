using BLOGAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using MongoDB.Driver;
using BCrypt.Net;

public class UserService : IUserService{

    private readonly IMongoCollection<User> _users;

    public UserService(IConfiguration configuration){
        var client = new MongoClient(configuration.GetConnectionString("BlogDb"));
        var database = client.GetDatabase("BlogDb");
        _users = database.GetCollection<User>("Users");
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
        if (user.Password == null || user.Password == ""){
            throw Error.ErrPasswordEmpty;
        }

        int upper = 0, lower = 0, digit = 0, special = 0;
        for (int i = 0; i < user.Password.Length; i++){
            if (user.Password[i] >= 'A' && user.Password[i] <= 'Z'){
                upper++;
            }
            else if (user.Password[i] >= 'a' && user.Password[i] <= 'z'){
                lower++;
            }
            else if (user.Password[i] >= '0' && user.Password[i] <= '9'){
                digit++;
            }
            else{
                special++;
            }
        }

        if (upper == 0 || lower == 0 || special == 0 || digit == 0){
            throw Error.ErrPasswordInvalid;
        }

        // check if user already exists
        if (_users.Find(u => u.Email == user.Email).FirstOrDefault() != null){
            throw Error.ErrUserExists;
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

        // unimplemented
        return ("access_token", "refresh");
    }

    public async Task<List<User>> GetUsers(){
        return await _users.Find(u => true).ToListAsync();
    }

    public async Task<User> GetUser(string id){
        return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateUser(string id, User user){
        _users.ReplaceOne(u => u.Id == id, user);
    }

    public async Task DeleteUser(string id){
        _users.DeleteOne(u => u.Id == id);
    }
}