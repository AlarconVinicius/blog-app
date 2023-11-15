using Api.IoC;
using Business.Models.Auth;
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

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp");

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
new ConfigureInitialBlogSeed(dbContext).StartConfig();

app.Run();
