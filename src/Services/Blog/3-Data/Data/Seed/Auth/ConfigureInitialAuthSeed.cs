using Business.Models.Auth;
using Data.Configuration;
using Data.Seed.Auth;
using Microsoft.AspNetCore.Identity;

namespace Data.Auth.Seed;

public class ConfigureInitialAuthSeed
{

    private readonly ApplicationDbContext _contextIdentity;
    private readonly UserManager<ApplicationUser> _userManager;

    public ConfigureInitialAuthSeed(ApplicationDbContext contextIdentity, UserManager<ApplicationUser> userManager)
    {
        _contextIdentity = contextIdentity;
        _userManager = userManager;
    }

    public void StartConfig()
    {
        new CreateInitialRolesSeed(_contextIdentity).Create();
        new CreateInitialAdminSeed(_contextIdentity, _userManager!).Create();
    }
}
