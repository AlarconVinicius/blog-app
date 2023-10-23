using Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUserEntity>
{
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUserEntity>().ToTable("AspNetUsers");

        base.OnModelCreating(builder);
    }
}
