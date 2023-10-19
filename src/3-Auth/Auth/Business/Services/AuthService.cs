using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Business.Interfaces;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Utils.Configuration.Business;

namespace Auth.Business.Services;

public class AuthService : MainService, IAuthService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;

    public AuthService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    public async Task<string> RegisterUserAsync(RegisterUserRequest registerUser)
    {
        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if(!result.Succeeded) 
        {
            foreach (var errors in result.Errors)
            {
                AddProcessingError(errors.Description);
            }
            return null!;
        }

        var userIdentity = await _userManager.FindByEmailAsync(registerUser.Email);
        await _userManager.AddToRoleAsync(userIdentity!, RoleEnum.User.ToString());    
        await _userManager.AddClaimAsync(userIdentity!, new Claim("Permission", PermissionEnum.Reader.ToString()));
        await _signInManager.SignInAsync(user, false);
        
        return await GenerateJwt(registerUser.Email);
    }

    public async Task<string> LoginAsync(LoginUserRequest loginUser)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if(result.Succeeded) 
            return await GenerateJwt(loginUser.Email);

        AddProcessingError("Usuário ou senha inválidos.");
        return null!;
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