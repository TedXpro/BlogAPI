using BLOGAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLOGAPI.Controllers{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase{
        private readonly IUserService _userService;

        [HttpPost("/register")]
        public async Task<ActionResult> Register(User user){
            return await Ok(_userService.Register(user));
        }

        [HttpPost("/login")]
        public async Task<ActionResult> Login(Account account){
            return await Ok(_userService.Login(account));
        }

        [HttpGet("/users")]
        public async Task<ActionResult> GetUsers(){
            return await Ok(_userService.GetUsers());
        }

        [HttpGet("/user/{id}")]
        public async Task<ActionResult> GetUser(int id){
            return await Ok(_userService.GetUser(id));
        }

        [HttpPut("/user/{id}")]
        public async Task<ActionResult> UpdateUser(int id, User user){
            return await Ok(_userService.UpdateUser(id, user));
        }

        [HttpDelete("/user/{id}")]
        public async Task<ActionResult> DeleteUser(int id){
            return await Ok(_userService.DeleteUser(id));
        }
    }

}