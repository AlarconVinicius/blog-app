using Api.Configuration;
using Api.IoC;
using Business.Models.Auth;
using Data.Auth.Seed;
using Data.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfig(builder.Configuration);
builder.Services.AddApiConfig();
builder.Services.ConfigureCustomServices();
builder.Services.ConfigureCustomAuthServices();
builder.Services.ConfigureCustomBlogServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseApiConfig(app.Environment);

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
