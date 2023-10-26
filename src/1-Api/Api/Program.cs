using Api.IoC;
using Business.Auth.Models;
using Data.Auth.Seed;
using Data.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDbContextJwtServices(builder.Configuration);
builder.Services.ConfigureCustomServices();
builder.Services.ConfigureCustomAuthServices();
builder.Services.ConfigureCustomBlogServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
if (dbContext.Database.GetPendingMigrations().Any())
{
    dbContext.Database.Migrate();
}
var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

new ConfigureInitialAuthSeed(dbContext, userManager!).StartConfig();

app.Run();
