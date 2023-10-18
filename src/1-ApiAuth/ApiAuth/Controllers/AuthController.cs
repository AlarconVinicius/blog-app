using ApiAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApiAuth.Controller;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Register(RegisterUserRequest registerUser) 
    {
        if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if(!result.Succeeded) return BadRequest(result.Errors);

        await _signInManager.SignInAsync(user, false);
        
        return Ok(); 
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserRequest loginUser) 
    {
        if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if(result.Succeeded) return Ok();

        return BadRequest("Usuário ou senha inválidoos."); 
    }
}
