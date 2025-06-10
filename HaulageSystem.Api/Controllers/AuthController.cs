using HaulageSystem.Application.Configuration;
using HaulageSystem.Application.Helpers;
using HaulageSystem.Application.Models.Identity;
using HaulageSystem.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace HaulageSystem.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class AuthController : BaseApiController
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    [HttpGet("check-auth")]
    public async Task<ActionResult<AuthenticationStateDto>> CheckAuth()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
            
                return Ok(new AuthenticationStateDto
                { 
                    IsAuthenticated = true,
                    Roles = AuthHelpers.FormatRoles(roles.ToList()),
                    Username=user.UserName
                });
            }
        }
        return Ok(new AuthenticationStateDto(){ IsAuthenticated = false});
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _signInManager.PasswordSignInAsync(command.Username, command.Password, true, false);

        if (result.Succeeded)
        {
            return Ok();
        }

        return Unauthorized();
    }
    
    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "Logout successful" });
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = new ApplicationUser
        {
            UserName = command.Username,
            Email = command.Email,
            FirstName = command.FirstName,
            LastName = command.LastName,
            NormalizedEmail = command.Email.ToUpper(),
            NormalizedUserName = command.Username.ToUpper(),
            EmailConfirmed = true,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (result.Succeeded)
        {
            return Ok(new { message = "User registered successfully" });
        }

        return BadRequest(result.Errors);
    }
}

public class LoginCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class AuthenticationStateDto
{
    public string  Username { get; set; }
    public List<UserRoles> Roles { get; set; }
    public bool IsAuthenticated { get; set; }
}

public class RegisterCommand
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}