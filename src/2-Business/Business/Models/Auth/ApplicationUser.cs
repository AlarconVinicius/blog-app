using Business.Models.Blog;
using Business.Models.Blog.Recipe;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models.Auth;

public class ApplicationUser : IdentityUser
{
    [Column(Order = 2)]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 50 caracteres")]
    public string Name { get; set; } = string.Empty;

    [Column(Order = 3)]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O sobrenome deve ter entre 3 e 50 caracteres")]
    public string LastName { get; set; } = string.Empty;

    [Column(Order = 4)]
    [StringLength(150)]
    public string FullName { get; private set; } = string.Empty;

    public ICollection<BlogEntity>? Blogs { get; set; }

    public ICollection<RecipePost>? RecipePosts { get; set; }

    public ApplicationUser()
    {

    }
    public ApplicationUser(string name, string lastName)
    {
        Name = name;
        LastName = lastName;
        JoinName();
    }
    public void JoinName()
    {
        FullName = Name + " " + LastName;
    }
}
