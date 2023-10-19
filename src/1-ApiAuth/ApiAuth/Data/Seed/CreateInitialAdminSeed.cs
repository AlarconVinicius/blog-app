using System.Security.Claims;
using ApiAuth.Data;
using ApiAuth.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiAuth;

public class CreateInitialAdminSeed
{
    private readonly ApplicationDbContext _contextIdentity;
    private readonly UserManager<IdentityUser> _userManager;

    public CreateInitialAdminSeed(ApplicationDbContext contextIdentity, UserManager<IdentityUser> userManager)
    {
        _contextIdentity = contextIdentity;
        _userManager = userManager;
    }

    public void Create()
    {
        Guid id = Guid.Parse("BBDC94BA-D192-409B-A9BF-BF5977B30425");
        var phoneNumber = "(99) 99999-9999";
        var email = "blog@admin.com";
        var password = "Admin@123";

        var user = new IdentityUser
        {
            Id = id.ToString(),
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            PhoneNumber = phoneNumber,
            PhoneNumberConfirmed = true,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper()
        };
        

        var userExists = _userManager.FindByEmailAsync(user.Email).Result;

        if (userExists is null)
        {
            string permissions = PermissionEnum.Reader.ToString() + "," + PermissionEnum.Writer.ToString();
            _userManager.CreateAsync(user, password).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, RoleEnum.Admin.ToString()).GetAwaiter().GetResult();
            _userManager.AddClaimAsync(user, new Claim("Permission", permissions)).GetAwaiter().GetResult();
        }
        else
        {
            var roles = _userManager.GetRolesAsync(userExists).GetAwaiter().GetResult();
            if (roles == null || roles.Count == 0)
            {
                _userManager.AddToRoleAsync(userExists, RoleEnum.Admin.ToString()).GetAwaiter().GetResult();
            }
        }
        _contextIdentity.SaveChanges();
    }
}