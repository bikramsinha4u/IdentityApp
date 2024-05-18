using IdentityApp.DTOs.Account;
using IdentityApp.Models;
using IdentityApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(JwtService jwtService, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this._jwtService = jwtService;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        [Authorize]
        [HttpGet("refresh-user-token")]
        public async Task<ActionResult<UserDto>> RefreshUserToken()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email)?.Value);

             return CreateApplicationUserDto(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var task = _userManager.FindByNameAsync(model.UserName);
            var user = task.Result;

            if (user == null)
                return Unauthorized("Invalid Username or Password.");

            if (user.EmailConfirmed == false)
                return Unauthorized("Please confirm your email.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid Username or Password.");

            return Ok(CreateApplicationUserDto(user));
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.Email))
                return BadRequest("This email address is already registerd, try different email.");

            var userToAdd = new User
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower(),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(userToAdd, model.Password);

            if(!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new JsonResult(new { title = "Account Created", message = "Your account has been created, please confrim your email address" }));
        }

        #region Private Helper
        private UserDto CreateApplicationUserDto(User user)
        {
            return new UserDto { FirstName = user.FirstName, LastName = user.LastName, Jwt = _jwtService.CreateJwt(user)};
        }

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower())
;        }
        #endregion
    }
}
