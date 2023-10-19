using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiAuth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiAuth.Controller;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
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

        var userIdentity = await _userManager.FindByEmailAsync(registerUser.Email);
        await _userManager.AddToRoleAsync(userIdentity!, RoleEnum.User.ToString());    
        await _signInManager.SignInAsync(user, false);
        
        return Ok(await GenerateJwt(registerUser.Email)); 
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserRequest loginUser) 
    {
        if(!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if(result.Succeeded) return Ok(await GenerateJwt(loginUser.Email));

        return BadRequest("Usuário ou senha inválidoos."); 
    }

    private async Task<string> GenerateJwt(string email)
    { 
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return null!;
        }
        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(await _userManager.GetClaimsAsync(user!));
        var userRoles = await _userManager.GetRolesAsync(user!);
        foreach (var role in userRoles)
        {
            identityClaims.AddClaim(new Claim(ClaimTypes.Role, role));
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        var tokenDescription = new SecurityTokenDescriptor 
        {
            Subject = identityClaims,
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescription));
    }
}