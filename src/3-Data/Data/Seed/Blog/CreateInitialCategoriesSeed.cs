using Business.Models.Blog;
using Data.Configuration;

namespace Data.Seed.Auth;

public class CreateInitialCategoriesSeed
{
    private readonly ApplicationDbContext _context;

    public CreateInitialCategoriesSeed(ApplicationDbContext context)
    {
        _context = context;
    }
    public void Create()
    {
        var blogId = Guid.Parse("2a2ff613-6f3b-4dd8-9fd6-a2f824b67b62");
        List<string> categories = new List<string>
        {
            "Café da Manhã",
            "Almoço",
            "Jantar",
            "Sobremesa",
            "Lanche",
            "Bebida",
            "Aperitivo",
            "Sopa",
            "Salada",
            "Massa",
            "Frutos do Mar",
            "Vegetariano",
            "Vegano",
            "Sem Lactose",
            "Sem Glúten",
            "Rápido e Fácil",
            "Feriado",
            "Crianças",
            "Outros"
        };

        foreach (string category in categories)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Name == category);

            if (existingCategory == null)
            {
                var newCategory = new Category(category, blogId);
                _context.Categories.Add(newCategory);
            }
        }
        _context.SaveChanges();
    }
}
