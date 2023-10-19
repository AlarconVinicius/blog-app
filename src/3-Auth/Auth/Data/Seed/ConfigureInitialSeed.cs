using Microsoft.AspNetCore.Identity;

namespace Auth.Data.Seed;

public class ConfigureInitialSeed
{

    private readonly ApplicationDbContext _contextIdentity;
    private readonly UserManager<IdentityUser> _userManager;

    public ConfigureInitialSeed(ApplicationDbContext contextIdentity, UserManager<IdentityUser> userManager)
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
