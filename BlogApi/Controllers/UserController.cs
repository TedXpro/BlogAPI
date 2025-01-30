using BLOGAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BLOGAPI.Controllers{
    [Authorize]
    [ApiController]
    [Route("[Controller]")]
    public class UserController : ControllerBase{
        private readonly IUserService _userService;

        public UserController(IUserService us){
            _userService = us;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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

        [Authorize(Roles = "admin")]
        [HttpGet("/users")]
        public async Task<ActionResult> GetUsers(){
            try{
                return Ok(await _userService.GetUsers());
            }
            catch(Exception e){
                return StatusCode(500, "An error occurred");
            }
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPut("/user/{id}")]
        public async Task<ActionResult> PromoteUser(string id, User user){
            try{
                await _userService.PromoteUser(id);
                return Ok("User promoted successfully.");
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

        [Authorize(Roles = "admin")]
        [HttpPost("/user/{id}")]
        public async Task<ActionResult> DemoteUser(string id){
            try{
                await _userService.DemoteUser(id);
                return Ok("User demoted successfully.");
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

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] string refreshToken) {
            try {
                var tokens = _userService.RefreshToken(refreshToken);
                return Ok(new { accessToken = tokens.accessToken, refreshToken = tokens.refreshToken });
            }
            catch (Exception ex) {
                return Unauthorized(ex.Message);
            }
        }
    }
}