using BLOGAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BLOGAPI.Controllers{
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase{
        private readonly IUserService _userService;

        public UserController(IUserService us){
            _userService = us;
        }

        [HttpPost("/register")]
        public async Task<ActionResult> Register(User user){
            try {
                await _userService.Register(user);
                return Ok("User registered successfully. Verification Email Sent.");
            }
            catch(Exception e){
                if (e == Error.ErrEmailEmpty){
                    return BadRequest(e.Message);
                }
                else if (e == Error.ErrEmailInvalid){
                    return BadRequest(e.Message);
                }
                else if (e == Error.ErrPasswordEmpty){
                    return BadRequest(e.Message);
                }
                else if (e == Error.ErrPasswordInvalid){
                    return BadRequest(e.Message);
                }
                else if (e == Error.ErrUserExists){
                    return BadRequest(e.Message);
                }
                else{
                    return StatusCode(500, "An error occurred");
                }
            }
        }

        [HttpPost("/login")]
        public async Task<ActionResult<(string, string)>> Login(Account account){
            try{
                return Ok( await _userService.Login(account));
            }
            catch(Exception e){
                if (e == Error.ErrEmailEmpty || e == Error.ErrPasswordEmpty){
                    return BadRequest(e.Message);
                }
                else if (e == Error.ErrIncorrectEmailPassword){
                    return BadRequest(e.Message);
                }
                else if (e == Error.ErrUserNotFound){
                    return BadRequest(e.Message);
                }
                else{
                    return StatusCode(500, "An error occurred");
                }
            }
            
        }

        [HttpGet("/users")]
        public async Task<ActionResult> GetUsers(){
            try{
                return Ok(await _userService.GetUsers());
            }
            catch(Exception e){
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpGet("/user/{id}")]
        public async Task<ActionResult> GetUser(string id){
            try{
                return Ok(await _userService.GetUser(id));
            }
            catch(Exception e){
                if (e == Error.ErrUserNotFound){
                    return BadRequest(e.Message);
                }
                else{
                    return StatusCode(500, "An error occurred");
                }
            }
        }

        [HttpPut("/user/{id}")]
        public async Task<ActionResult> UpdateUser(string id, User user){
            try {
                await _userService.UpdateUser(id, user);
                return Ok("user updated successfully.");
            }
            catch(Exception e){
                if (e == Error.ErrUserNotFound){
                    return BadRequest(e.Message);
                }
                else{
                    return StatusCode(500, "An error occurred");
                }
            }
        }

        [HttpDelete("/user/{id}")]
        public async Task<ActionResult> DeleteUser(string id){
            try {
                await _userService.DeleteUser(id);
                return Ok("user deleted successfully.");
            }
            catch(Exception e){
                if (e == Error.ErrUserNotFound){
                    return BadRequest(e.Message);
                }
                else{
                    return StatusCode(500, "An error occurred");
                }
            }
        }
    }

}