using Business.Interfaces.Services;
using Business.Interfaces.Services.Auth;
using Business.Models.Auth;
using Business.Models.Auth.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Services.Auth;

public class AuthService : MainService, IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppSettings _appSettings;

    public AuthService(
                       SignInManager<ApplicationUser> signInManager,
                       UserManager<ApplicationUser> userManager, 
                       IOptions<AppSettings> appSettings,
                       INotifier notifier) : base(notifier)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    public async Task<LoginUserResponse> RegisterUserAsync(RegisterUserRequest registerUser)
    {
        var user = new ApplicationUser
        {
            Name = registerUser.Name,
            LastName = registerUser.LastName,
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true,
        };
        user.JoinName();

        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (!result.Succeeded)
        {
            foreach (var errors in result.Errors)
            {
                Notify(errors.Description);
            }
            return null!;
        }

        var userIdentity = await _userManager.FindByEmailAsync(registerUser.Email);
        await _userManager.AddToRoleAsync(userIdentity!, RoleEnum.User.ToString());
        await _userManager.AddClaimAsync(userIdentity!, new Claim("Permission", PermissionEnum.Reader.ToString()));
        await _signInManager.SignInAsync(user, false);

        return await GenerateJwt(registerUser.Email);
    }

    public async Task<LoginUserResponse> LoginAsync(LoginUserRequest loginUser)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        if (result.Succeeded)
            return await GenerateJwt(loginUser.Email);

        Notify("Usuário ou senha inválidos.");
        return null!;
    }

    private async Task<LoginUserResponse> GenerateJwt(string email)
    {
        var userDb = await _userManager.FindByEmailAsync(email);
        if (userDb == null)
        {
            return null!;
        }
        var claims = (await _userManager.GetClaimsAsync(userDb)).ToList();
        var userRoles = await _userManager.GetRolesAsync(userDb);
        AddStandardClaims(claims, userDb);
        AddUserRolesClaims(claims, userRoles);

        var token = GenerateToken(claims);

        var response = CreateResponse(token, userDb, claims);

        return response;
    }

    private void AddStandardClaims(List<Claim> claims, IdentityUser user)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
    }
    private void AddUserRolesClaims(List<Claim> claims, IList<string> userRoles)
    {
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }
    }
    private SecurityToken GenerateToken(List<Claim> claims)
    {
        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

    }
    private LoginUserResponse CreateResponse(SecurityToken token, IdentityUser user, List<Claim> claims)
    {
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginUserResponse
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email!,
                Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
            }
        };
    }
    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - DateTimeOffset.UnixEpoch).TotalSeconds);
}