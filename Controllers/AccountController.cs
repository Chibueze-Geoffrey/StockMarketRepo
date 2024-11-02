using Api.DTOs.Account;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Api.Interfaces;
using Api.DTOs;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signIn;

        public AccountController(UserManager<AppUser> userManager,
            ITokenService tokenService,SignInManager<AppUser>signIn)
        {
            _tokenService = tokenService;
            _signIn = signIn;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid) {
                return BadRequest(ModelState); 
            }

            var users = await _userManager.Users
                .FirstOrDefaultAsync(x=>x.UserName == loginDto.UserName.ToLower());

            if (users == null) { 
                return Unauthorized("Invalid User");
            }
            var results = await _signIn.CheckPasswordSignInAsync(users, loginDto.Password,false);
            if (!results.Succeeded)
            {
                return Unauthorized("Invalid User:  UserName not found or Incorrect password");
            }
            return Ok(
                new NewUSerDto
                {
                    UserName = users.UserName,
                    Email = users.Email,
                    Token = _tokenService.CreateToken(users)
                });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new NewUSerDto
                        {
                            UserName = appUser.UserName,
                            Email = appUser.Email,
                            Token = _tokenService.CreateToken(appUser)
                        });
                    }
                    else
                    {
                        return StatusCode(500, new { message = "Failed to add user to role", errors = roleResult.Errors });
                    }
                }
                else
                {
                    return StatusCode(500, new { message = "User creation failed", errors = createdUser.Errors });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while registering the user.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
