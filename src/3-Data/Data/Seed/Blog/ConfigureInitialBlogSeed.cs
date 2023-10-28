using Data.Configuration;
using Data.Seed.Auth;

namespace Data.Auth.Seed;

public class ConfigureInitialBlogSeed
{

    private readonly ApplicationDbContext _contextIdentity;

    public ConfigureInitialBlogSeed(ApplicationDbContext contextIdentity)
    {
        _contextIdentity = contextIdentity;
    }

    public void StartConfig()
    {
        new CreateInitialBlogsSeed(_contextIdentity).Create();
        new CreateInitialCategoriesSeed(_contextIdentity).Create();
    }
}
