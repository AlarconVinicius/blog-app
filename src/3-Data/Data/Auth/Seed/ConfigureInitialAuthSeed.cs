using Business.Auth.Models;
using Data.Configuration;
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
