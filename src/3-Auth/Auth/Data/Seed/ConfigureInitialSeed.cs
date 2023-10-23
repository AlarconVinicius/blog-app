using Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Data.Seed;

public class ConfigureInitialSeed
{

    private readonly ApplicationDbContext _contextIdentity;
    private readonly UserManager<ApplicationUserEntity> _userManager;

    public ConfigureInitialSeed(ApplicationDbContext contextIdentity, UserManager<ApplicationUserEntity> userManager)
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
