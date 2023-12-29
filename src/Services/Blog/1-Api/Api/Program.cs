using Api.Configuration;
using Api.Configuration.Swagger;
using Asp.Versioning.ApiExplorer;
using Business.Models.Auth;
using Data.Auth.Seed;
using Data.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.ResolveDependencies();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApiConfig(app.Environment);
app.UseSwaggerConfig(apiVersionDescriptionProvider);

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
if (dbContext.Database.GetPendingMigrations().Any())
{
    dbContext.Database.Migrate();
}
var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

new ConfigureInitialAuthSeed(dbContext, userManager!).StartConfig();
new ConfigureInitialBlogSeed(dbContext).StartConfig();

app.Run();
