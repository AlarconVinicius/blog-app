using Business.Helpers.Auth;
using Business.Interfaces.Repositories.Blog;
using Business.Interfaces.Services.Blog;
using Business.Mappings.Blog;
using Business.Models.Auth;
using Business.Models.Blog.Dtos;
using Business.Models.Blog.Recipe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Business.Services.Blog;

public class UserService : MainService, IUserService
{
    
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpAccessor;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpAccessor, IUserRepository userRepository)
    {
        _userManager = userManager;
        _httpAccessor = httpAccessor;
        _userRepository = userRepository;
    }

    public async Task<UserViewDto> GetAuthenticatedUser()
    {
        var userId = AuthHelper.GetUserId(_httpAccessor).ToString();
        var userDb = await _userManager.FindByIdAsync(userId);
        if(userDb is null){
            AddProcessingError("Falha ao buscar usuário: Usuário não encontrado.");
            return null!;
        }
        var response = userDb.ToDto();
        return response;
    }

    public async Task UpdateAuthenticatedUser(UserUpdDto user)
    {
        var userId = AuthHelper.GetUserId(_httpAccessor).ToString();
        var userDb = await _userManager.FindByIdAsync(userId);

        if(userDb is null){
            AddProcessingError("Falha ao buscar usuário: Usuário não encontrado.");
            return;
        }
        
        userDb.Name = user.Name;
        userDb.LastName = user.LastName;
        userDb.JoinName();
        userDb.UserName = user.Email;
        userDb.NormalizedUserName = user.Email.ToUpper();
        userDb.Email = user.Email;
        userDb.NormalizedEmail = user.Email.ToUpper();
        userDb.EmailConfirmed = true;
        userDb.PhoneNumber = user.PhoneNumber;
        userDb.PhoneNumberConfirmed = true;
        await _userManager.UpdateAsync(userDb);
    }

    public async Task UpdatePassword(UserPasswordDto userPassword) {
        if(userPassword.NewPassword != userPassword.ConfirmNewPassword){
            AddProcessingError("Falha ao atualizar a senha: As senhas não coincidem.");
            return;
        }
        var userId = AuthHelper.GetUserId(_httpAccessor).ToString();
        var userDb = await _userManager.FindByIdAsync(userId);

        if(userDb is null){
            AddProcessingError("Falha ao atualizar a senha: Usuário não encontrado.");
            return;
        }

        await _userManager.ChangePasswordAsync(userDb, userPassword.OldPassword, userPassword.NewPassword);
    }

    public async Task FavoriteRecipe(Guid recipeId, bool favorite)
    {
        var userId = AuthHelper.GetUserId(_httpAccessor);
        var userRecipe = new UserFavoriteRecipe(recipeId, userId.ToString());

        var existingFavorite = await _userRepository.GetFavoriteRecipeByUserAndRecipeId(userRecipe);

        if (existingFavorite != null && !favorite)
        {
            await _userRepository.UnfavoriteRecipe(existingFavorite);
        }
        else if (existingFavorite == null && favorite)
        {
            await _userRepository.FavoriteRecipe(userRecipe);
        }
    }

    public async Task<IEnumerable<RecipePostViewDto>> GetFavoriteRecipesByUserId()
    {
        var userId = AuthHelper.GetUserId(_httpAccessor);
        return (await _userRepository.GetFavoriteRecipesByUserId(userId)).Select(x => x.ToDto());
    }

}