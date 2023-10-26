using Business.Models.Auth;
using Data.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Data.Auth.Seed;

public class CreateInitialAdminSeed
{
    private readonly ApplicationDbContext _contextIdentity;
    private readonly UserManager<ApplicationUser> _userManager;

    public CreateInitialAdminSeed(ApplicationDbContext contextIdentity, UserManager<ApplicationUser> userManager)
    {
        _contextIdentity = contextIdentity;
        _userManager = userManager;
    }

    public void Create()
    {
        Guid id = Guid.Parse("BBDC94BA-D192-409B-A9BF-BF5977B30425");
        var phoneNumber = "(99) 99999-9999";
        var name = "Admin";
        var lastName = "User";
        var email = "blog@admin.com";
        var password = "Admin@123";

        var user = new ApplicationUser
        {
            Id = id.ToString(),
            UserName = email,
            Email = email,
            Name = name,
            LastName = lastName,
            EmailConfirmed = true,
            PhoneNumber = phoneNumber,
            PhoneNumberConfirmed = true,
            NormalizedEmail = email.ToUpper(),
            NormalizedUserName = email.ToUpper()
        };
        user.JoinName();


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